using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroSequenceManager : MonoBehaviour {


    [SerializeField] GameObject sequenceBall;

    [SerializeField] private float cameraStartSize = 50f;
    [SerializeField] private float cameraEndSize = 16f;
    [SerializeField] private float cameraResizeTime = 5f;

    private float timeElapsed = 0;
    private Camera mainCamera;


    private void Start() {
        mainCamera = Camera.main;
        mainCamera.orthographicSize = cameraStartSize;
    }


    private void Update() {

        if (!GameStateManager.Instance.IsGameIntroSequence()) { return; }

        if (timeElapsed < cameraResizeTime) {
            mainCamera.orthographicSize = Mathf.Lerp(cameraStartSize, cameraEndSize, timeElapsed / cameraResizeTime);

            timeElapsed += Time.deltaTime;
            return;
        }

        CameraManager.Instance.updateCameraBounds();
        GameStateManager.Instance.EndOfIntroSequence();
    }


}


