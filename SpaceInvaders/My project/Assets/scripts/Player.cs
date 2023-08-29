using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public static Player Instance { get; private set; }

    [SerializeField] private float speed = 5.0f;
    [SerializeField] private Projectile laserPrefab;
    [SerializeField] private Transform projectileParent;

    [SerializeField] private float edgePadding = 1f;

    private Projectile currentLaser;

    private Vector3 leftEdge;
    private Vector3 rightEdge;

    private bool isMoveLeft = false;
    private bool isMoveRight = false;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is more than one Player instance");
        }
        Instance = this;
    }

    private void Start() {
        // set world bounds
        leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        // set events
        GameInput.Instance.OnMoveLeftStarted += GameInput_OnMoveLeftStarted;
        GameInput.Instance.OnMoveLeftCanceled += GameInput_OnMoveLeftCanceled;
        GameInput.Instance.OnMoveRightStarted += GameInput_OnMoveRightStarted;
        GameInput.Instance.OnMoveRightCanceled += GameInput_OnMoveRightCanceled;
        GameInput.Instance.OnAction += GameInput_OnAction; ;

    }

    private void GameInput_OnMoveLeftStarted(object sender, System.EventArgs e) {
        isMoveLeft = true;
    }
    private void GameInput_OnMoveLeftCanceled(object sender, System.EventArgs e) {
        isMoveLeft = false;
    }

    private void GameInput_OnMoveRightStarted(object sender, System.EventArgs e) {
        isMoveRight = true;
    }
    private void GameInput_OnMoveRightCanceled(object sender, System.EventArgs e) {
        isMoveRight = false;
    }

    private void Update() {
        if(isMoveLeft && isMoveRight) { return; }

        if (isMoveLeft) {
            // move left
            if (this.transform.position.x >= leftEdge.x + edgePadding) {
                this.transform.position += Vector3.left * speed * Time.deltaTime;
            }
        }

        if (isMoveRight) {
            // move right
            if (this.transform.position.x <= rightEdge.x - edgePadding) {
                this.transform.position += Vector3.right * speed * Time.deltaTime;
            }
        }
    }

    private void GameInput_OnAction(object sender, System.EventArgs e) {
        // game must be started
        if (!GameStateManager.Instance.IsGamePlaying()) { return; }
        // only 1 laser at a time
        if (!currentLaser) {
            currentLaser = Instantiate(laserPrefab, this.transform.position, Quaternion.identity, projectileParent);
        }
    }
}
