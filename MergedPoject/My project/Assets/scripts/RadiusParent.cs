using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RadiusParent : MonoBehaviour {
    public static RadiusParent Instance { get; private set; }

    [SerializeField] private float playerRotationSpeed = 80f;

    [SerializeField] private float lerpDuration = 0.25f;
    private float elapsedTime = 0f;

    private bool isMoveLeft = false;
    private bool isMoveRight = false;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is more than one Player instance");
        }
        Instance = this;
    }

    private void Start() {
        // set events
        GameInput.Instance.OnMoveLeftStarted += GameInput_OnMoveLeftStarted;
        GameInput.Instance.OnMoveLeftCanceled += GameInput_OnMoveLeftCanceled;
        GameInput.Instance.OnMoveRightStarted += GameInput_OnMoveRightStarted;
        GameInput.Instance.OnMoveRightCanceled += GameInput_OnMoveRightCanceled;

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

        if ((isMoveLeft && isMoveRight) || (!isMoveLeft && !isMoveRight)) {
            if (elapsedTime <= 0) {
                elapsedTime = 0;
            } else {
                elapsedTime -= Time.deltaTime;
            }
            return; 
        }

        if (elapsedTime >= lerpDuration) { 
            elapsedTime = lerpDuration; 
        } else {
            elapsedTime += Time.deltaTime;
        }

        if (isMoveLeft) {
            // move left
            transform.Rotate(0, 0, Mathf.Lerp(0, 1, elapsedTime / lerpDuration) * playerRotationSpeed * Time.deltaTime);
        }

        if (isMoveRight) {
            // move right
            transform.Rotate(0, 0, -Mathf.Lerp(0, 1, elapsedTime / lerpDuration) * playerRotationSpeed * Time.deltaTime);
        }
    }

}