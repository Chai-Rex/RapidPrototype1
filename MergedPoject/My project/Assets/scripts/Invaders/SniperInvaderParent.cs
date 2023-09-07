using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperInvaderParent : MonoBehaviour {


    [SerializeField] private float baseTargetHeight = 20f;
    [SerializeField] private float movementSpeed = 8f;
    [SerializeField] private float waitingToContinueTime = 5f;
    [SerializeField] private float stationaryTime = 10f;

    [SerializeField] private SniperInvader sniperScript;


    private float currentTimeWaiting = 0f;


    private enum State {

        MovingIntoFrame,
        Stationary,
        MovingOutOfFrame,
        waitingToContinue,
        Sniping
    }
    private State state;

    private void Awake() {
        state = State.MovingIntoFrame;
        sniperScript.transform.localPosition = new Vector3(0, CameraManager.Instance.viewportRadius + 2, 0);

    }

    private void Update() {

        switch (state) {

            case State.MovingIntoFrame:


                sniperScript.transform.localPosition -= new Vector3(0, movementSpeed * Time.deltaTime, 0);
                if (sniperScript.transform.localPosition.y < baseTargetHeight) {
                    sniperScript.transform.localPosition = new Vector3(0, baseTargetHeight, 0);
                    state = State.Stationary;
                }


                break;
            case State.Stationary:

                currentTimeWaiting += Time.deltaTime;
                if (currentTimeWaiting > stationaryTime) {

                    currentTimeWaiting = 0f;
                    state = State.MovingOutOfFrame;
                }


                break;
            case State.MovingOutOfFrame:

                sniperScript.transform.localPosition += new Vector3(0, movementSpeed * Time.deltaTime, 0);
                if (sniperScript.transform.localPosition.y > CameraManager.Instance.viewportRadius + 1) {
                    sniperScript.transform.localPosition = new Vector3(0, CameraManager.Instance.viewportRadius + 1, 0);
                    sniperScript.gameObject.SetActive(false);

                    state = State.waitingToContinue;
                }

                break;
            case State.waitingToContinue:
                currentTimeWaiting += Time.deltaTime;
                if (currentTimeWaiting > waitingToContinueTime) {

                    currentTimeWaiting = 0f;
                    sniperScript.gameObject.SetActive(true);
                    state = State.MovingIntoFrame;
                }

                break;
            case State.Sniping:


                break;
        }

    }

    public void SetSniping() {
        if (state == State.Sniping) { sniperScript.FireShot(); return; }
        if (!(state == State.Stationary)) { return; }
        state = State.Sniping;
    }

    public void EndSniping() {
        state = State.Stationary;
    }


    public void Explode() {
        Destroy(this.gameObject);
    }


    public void SetRotation(float degree) {
        this.transform.Rotate(0f, 0f, degree);
    }

}
