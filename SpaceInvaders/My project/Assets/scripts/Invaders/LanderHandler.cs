using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Rendering.CameraUI;
using static UnityEngine.Rendering.DebugUI.Table;

public class LanderHandler : MonoBehaviour {
    [Header("Grid Settings")]
    [SerializeField] private int rows = 5;
    [SerializeField] private int columns = 11;
    [SerializeField] private float degreesBetweenInvaders = 5f;
    [SerializeField] private float heightPadding = 2f;

    [Header("Movement Settings")]
    [SerializeField] private float startingDegree = 0f;
    [SerializeField] private float startingDirection = 1f;
    [SerializeField] private float dropAmount = 1f;
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private float incrementSpeedBy = 0.25f;
    [SerializeField] private AnimationCurve moveSpeed;

    [Header("Projectile Settings")]
    [SerializeField] private float missileRate = 1f;

    [Header("Spawn Movement")]
    [SerializeField] private float moveIntoFrameSpeed = 1f;
    [SerializeField] private float moveIntoFrameAmount = 10f;

    [Header("Prefab")]
    [SerializeField] private GameObject BasicLanderInvader;
    [SerializeField] private GameObject SpecialLanderInvader;
    [SerializeField] private Projectile missile;
    [SerializeField] private Transform projectileHolder;


    private float currentMoveIntoFrameAmount = 0f;
    private bool isMovingDown = false;
    private float increaseBaseSpeedPercent = 1;
    // counting 
    public int amountKilled { get; private set; }
    private int totalAmountInvaders => rows * columns;
    private int amoutAlive => totalAmountInvaders - amountKilled;

    private float percentKilled => (float)amountKilled / (float)totalAmountInvaders;

    private GameObject[,] InvaderGrid;
    private GameObject[] RadiusParents;


    private void Awake() {
        InvaderGrid = new GameObject[rows, columns];
        RadiusParents = new GameObject[columns];
    }

    private void Start() {
        increaseBaseSpeedPercent -= incrementSpeedBy;
        StartLanders();
        //LanderInvader.onLanderKilled += Invader_OnLanderKilled;
        InvokeRepeating(nameof(FireMissle), missileRate, missileRate);
    }

    private void StartLanders() {
        currentMoveIntoFrameAmount = moveIntoFrameAmount;
        amountKilled = 0;
        increaseBaseSpeedPercent += incrementSpeedBy;
        this.transform.eulerAngles = new Vector3(0, 0, 0);


        // populate grid with invaders

        for (int col = 0; col < this.columns; col++) {

            RadiusParents[col] = Instantiate(new GameObject("RadiusParentLander"), new Vector3(0, 0, 0), Quaternion.identity, this.transform);

            for (int row = 0; row < rows; row++) {
                if (row == rows / 2) {
                    InvaderGrid[row, col] = Instantiate(SpecialLanderInvader, new Vector3(0, 0, 0), Quaternion.identity, RadiusParents[col].transform);
                    // action
                    SpecialLanderInvader currentInvader = InvaderGrid[row, col].GetComponent<SpecialLanderInvader>();
                    if (!currentInvader) { Debug.LogError("missing SpecialLanderInvader script"); }
                    currentInvader.killed += InvaderKilled;
                } else {
                    InvaderGrid[row, col] = Instantiate(BasicLanderInvader, new Vector3(0, 0, 0), Quaternion.identity, RadiusParents[col].transform);
                    // action
                    LanderInvader currentInvader = InvaderGrid[row, col].GetComponent<LanderInvader>();
                    if (!currentInvader) { Debug.LogError("missing LanderInvader script"); }
                    currentInvader.killed += InvaderKilled;
                }


                InvaderGrid[row, col].transform.localPosition += new Vector3(0, CameraManager.Instance.viewportRadius + row * heightPadding, 0);

            }

            RadiusParents[col].transform.Rotate(0, 0, degreesBetweenInvaders * col + 45 + 45 - (columns * degreesBetweenInvaders) / 2 );
        }

        this.transform.eulerAngles = new Vector3(0, 0, startingDegree - 90);
    }

    private void DestroyLanders() {

        for (int col = 0; col < this.columns; col++) {
            Destroy(RadiusParents[col]);
            for (int row = 0; row < rows; row++) {
                Destroy(InvaderGrid[row, col]);
            }
        }

    }

    private void Update() {

        if (GameStateManager.Instance.IsGameWaitingToStart()) { return; }

        // move below viewport

        if (currentMoveIntoFrameAmount >= 0) {
            float moveAmountThisFrame = moveIntoFrameSpeed * Time.deltaTime;
            currentMoveIntoFrameAmount -= moveAmountThisFrame;
            foreach (GameObject invader in InvaderGrid) {
                invader.transform.localPosition -= new Vector3(0, moveAmountThisFrame, 0);
            }

            if (currentMoveIntoFrameAmount < 0) {
                foreach (GameObject invader in InvaderGrid) {
                    invader.transform.localPosition -= new Vector3(0, currentMoveIntoFrameAmount, 0);
                }
            }
            return;
        }

        if (GameStateManager.Instance.IsGameCountdownToStart()) { GameStateManager.Instance.EndCountdownToStart(); }

        //move down
        if (isMovingDown) {


            if (dropAmount >= 0 ) {
                float moveAmountThisFrame = moveSpeed.Evaluate(percentKilled) * increaseBaseSpeedPercent * Time.deltaTime;
                dropAmount -= moveAmountThisFrame;
                foreach (GameObject invader in InvaderGrid) {
                    invader.transform.localPosition -= new Vector3(0, moveAmountThisFrame, 0);
                }
                return;
            }

            if (dropAmount < 0) {
                foreach (GameObject invader in InvaderGrid) {
                    invader.transform.localPosition -= new Vector3(0, dropAmount, 0);
                }
            }

            isMovingDown = false;
            dropAmount = 1f;
        }


        //this.transform.position += direction * moveSpeed.Evaluate(percentKilled) * IncreaseBaseSpeedPercent * Time.deltaTime;

        // move left or right
        foreach (GameObject invader in InvaderGrid) {
            // if invader is dead, skip and continue to next invader
            if (!invader.gameObject.activeInHierarchy) {
                continue;
            }

            if (startingDirection < 0 && invader.transform.parent.transform.localEulerAngles.z < 50) {
                // hit right most limits
                // flip direction and move down
                startingDirection *= -1f;
                isMovingDown = true;
                //Debug.Log("RIGHT edge hit");
                return;
            } else if (startingDirection > 0 && invader.transform.parent.transform.localEulerAngles.z > 130) {
                // hit left most limits
                // flip direction and move down
                startingDirection *= -1f;
                isMovingDown = true;
                //Debug.Log("LEFT edge hit");
                return;
            }
        }
        //Debug.Log(invader.transform.parent.transform.localEulerAngles.z);
        //Debug.Log(startingDirection * moveSpeed.Evaluate(percentKilled) * increaseBaseSpeedPercent * rotationSpeed * Time.deltaTime);
        float rotationAmountThisFrame = moveSpeed.Evaluate(percentKilled) * increaseBaseSpeedPercent * rotationSpeed * Time.deltaTime;
        foreach (GameObject parent in RadiusParents) {
            parent.transform.Rotate(0, 0, startingDirection * rotationAmountThisFrame);
        }

    }

    private void FireMissle() {
        foreach (GameObject invader in InvaderGrid) {
            if (!invader.gameObject.activeInHierarchy) {
                continue;
            }

            if (Random.value < (1.0f / (float)amoutAlive)) {
                Instantiate(missile, invader.transform.position, Quaternion.identity, projectileHolder);
                break;
            }
        }
    }

    private void InvaderKilled() {
        amountKilled++;
        if (amountKilled >= totalAmountInvaders) {
            ScoreManager.Instance.AddToScore(1000);

            DestroyLanders();
            StartLanders();
        }
    }

}
