using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public static Player Instance { get; private set; }

    [SerializeField] public float G = 2000f;
    [SerializeField] public float maxG = 2200f;
    [SerializeField] public float minG = 1800f;

    [SerializeField] private float incrementG = 100f;

    [SerializeField] private Ball ballPrefab;
    [SerializeField] private Transform ballParent;
    private Ball currentBall;

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
        }
    }
    private void GameInput_OnDecrease(object sender, System.EventArgs e) {
        if (G > minG) {
            G -= incrementG;
        }
    }
}
