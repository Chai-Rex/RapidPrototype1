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

}
