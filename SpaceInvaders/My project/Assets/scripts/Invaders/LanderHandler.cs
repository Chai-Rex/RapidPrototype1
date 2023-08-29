using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Rendering.CameraUI;
using static UnityEngine.Rendering.DebugUI.Table;

public class LanderHandler : MonoBehaviour {

    [SerializeField] private int rows = 5;
    [SerializeField] private int columns = 11;
    [SerializeField] private float spacingBetweenInvaders = 2.0f;
    [SerializeField] private float edgePadding = 1f;

    [SerializeField] private GameObject LanderInvader1;

    [SerializeField] private float dropAmount = 1f;
    [SerializeField] private AnimationCurve moveSpeed;

    private bool isMovingDown = false;
    private float targetHeight;
    private Vector3 direction = Vector2.right;

    private Vector3 leftEdge;
    private Vector3 rightEdge;
    private Vector3 topEdge;

    private float timeElapsed = 0;
    private float lerpDuration = 3;
    private float startValue = 0;
    private float endValue = 10;
    private float valueToLerp;

    public int amountKilled { get; private set; }
    private int totalAmountInvaders => rows * columns;
    private float percentKilled => (float)amountKilled / (float)totalAmountInvaders;

    private float IncreaseBaseSpeedPercent = 0.5f;

    private GameObject[,] InvaderGrid;

    private void Awake() {
        InvaderGrid = new GameObject[rows, columns];
    }

    private void Start() {
        StartLanders();
    }

    private void StartLanders() {

        amountKilled = 0;
        IncreaseBaseSpeedPercent += 0.5f;
        timeElapsed = 0;

        // find center
        float width = spacingBetweenInvaders * (float)(columns - 1);
        float height = spacingBetweenInvaders * (float)(rows - 1);
        Vector2 centering = new Vector2(-width / 2, -height / 2);


        // populate grid with invaders
        for (int row = 0; row < rows; row++) {
            // row center
            Vector3 rowPosition = new Vector3(centering.x, centering.y + (row * spacingBetweenInvaders), 0.0f);
            for (int col = 0; col < this.columns; col++) {

                InvaderGrid[row, col] = Instantiate(LanderInvader1, this.transform.position, Quaternion.identity, this.transform);
                // column center
                Vector3 position = rowPosition;
                position.x += col * spacingBetweenInvaders;
                InvaderGrid[row, col].transform.localPosition = position;
                // action
                LanderInvader currentInvader = InvaderGrid[row, col].GetComponent<LanderInvader>();
                if (!currentInvader) { Debug.LogError("missing invader script"); }
                currentInvader.killed += InvaderKilled;

            }
        }

        // set world bounds
        leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
        topEdge = Camera.main.ViewportToWorldPoint(Vector3.up);

        // move above viewport
        startValue = topEdge.y + (height / 2) + edgePadding;
        endValue = topEdge.y - (height / 2) - edgePadding;
        this.transform.position = new Vector3(0, startValue, 0);
        targetHeight = endValue;
    }

    private void DestroyLanders() {
        for (int row = 0; row < rows; row++) {
            for (int col = 0; col < this.columns; col++) {
                Destroy(InvaderGrid[row, col]);
            }
        }
    }

    private void Update() {

        if (GameStateManager.Instance.IsGameWaitingToStart()) { return; }

        // move below viewport
        if (timeElapsed < lerpDuration) {
            valueToLerp = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
            this.transform.position = new Vector3(0, valueToLerp, 0);
            timeElapsed += Time.deltaTime;
            return;
        }

        // wait to start
        if (GameStateManager.Instance.IsGameCountdownToStart()) { return; }

        // move down
        if (isMovingDown) {
            if (this.transform.position.y <= targetHeight) {
                isMovingDown = false;
                this.transform.position = new Vector3(this.transform.position.x, targetHeight, 0);
            } else {
                this.transform.position += Vector3.down * moveSpeed.Evaluate(percentKilled) * IncreaseBaseSpeedPercent * Time.deltaTime;
                return;
            }
        }


        this.transform.position += direction * moveSpeed.Evaluate(percentKilled) * IncreaseBaseSpeedPercent * Time.deltaTime;

        // move left or right
        foreach (GameObject invader in InvaderGrid) {
            // if invader is dead, skip and continue to next invader
            if (!invader.gameObject.activeInHierarchy) {
                continue;
            }

            if (direction == Vector3.right && invader.transform.position.x > rightEdge.x - edgePadding) {
                // hit right most limits
                // flip direction and move down
                direction *= -1f;
                isMovingDown = true;
                targetHeight -= dropAmount;
                //Debug.Log("RIGHT edge hit");
            } else if (direction == Vector3.left && invader.transform.position.x < leftEdge.x + edgePadding) {
                // hit left most limits
                // flip direction and move down
                direction *= -1f;
                isMovingDown = true;
                targetHeight -= dropAmount;
                //Debug.Log("LEFT edge hit");

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
