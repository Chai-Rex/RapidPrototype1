using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;

public class Player : MonoBehaviour {

    public static Player Instance { get; private set; }

    public event EventHandler OnGChanged;

    [SerializeField] public float G = 2000f;
    [SerializeField] public float maxG = 2200f;
    [SerializeField] public float minG = 1800f;

    [SerializeField] private float incrementG = 100f;

    [SerializeField] private float damageImmuneTime = 1f;

    [SerializeField] private Ball ballPrefab;
    [SerializeField] private Transform ballParent;

    private Ball currentBall;

    public bool isDamageImmune = false;
    private float currentImmuneTimer = 0f;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is more than one Player instance");
        }
        Instance = this;
    }

    private void Start() {
        // set events
        GameInput.Instance.OnAction += GameInput_OnAction; ;
        GameInput.Instance.OnIncrease += GameInput_OnIncrease;
        GameInput.Instance.OnDecrease += GameInput_OnDecrease;

    }
    private void Update() {
        HandleImmunity();
    }

    private void HandleImmunity() {
        if (isDamageImmune) {
            currentImmuneTimer += Time.deltaTime;
            if (currentImmuneTimer > damageImmuneTime) {
                currentImmuneTimer = 0f;
                isDamageImmune = false;
            }
        }
    }


    private void GameInput_OnAction(object sender, System.EventArgs e) {
        // game must be started
        if (!GameStateManager.Instance.IsGamePlaying()) { return; }
        // only 1 laser at a time
        if (!currentBall) {
            currentBall = Instantiate(ballPrefab, this.transform.position, Quaternion.identity, ballParent);
        }
    }
    private void GameInput_OnIncrease(object sender, System.EventArgs e) {
        if (G < maxG) {
            G += incrementG;
            OnGChanged?.Invoke(this, EventArgs.Empty);
        }
    }
    private void GameInput_OnDecrease(object sender, System.EventArgs e) {
        if (G > minG) {
            G -= incrementG;
            OnGChanged?.Invoke(this, EventArgs.Empty);
        }
    }
    public float GetNormalizedPower() {
        return (G - minG) / (maxG - minG);
    }

}
