using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberInvaderParent : MonoBehaviour {
    [SerializeField] private int direction = 1;
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private float rotationSpeedBombing = 2f;
    [SerializeField] private float baseTargetHeight = 14f;
    [SerializeField] private float amountOfDegreesToRotate = 360f;

    [SerializeField] private float waitingToContinueTime = 10f;

    [SerializeField] private BomberInvader bomberScript;


    private float currentTimeWaiting = 0f;
    private float degreesSurvayed = 0f;

    private enum State {

        MovingIntoFrame,
        Moving,
        MovingOutOfFrame,
        waitingToContinue,
        Bombing
    }
    private State state;

    private void Start() {
        state = State.MovingIntoFrame;
        bomberScript.transform.localPosition = new Vector3(0, CameraManager.Instance.viewportRadius + 1, 0);
        this.transform.Rotate(0f, 0f, Random.Range(0f, 360f));
        int randomDirection = Random.Range(0, 2);
        switch (randomDirection) {
            case 0: direction = -1; break;
            case 1: direction = 1; break;
        }
    }

    private void Update() {

        switch (state) {

            case State.MovingIntoFrame:


                bomberScript.transform.localPosition -= new Vector3(0, movementSpeed * Time.deltaTime, 0);
                if (bomberScript.transform.localPosition.y < baseTargetHeight) {
                    bomberScript.transform.localPosition = new Vector3(0, baseTargetHeight, 0);
                    state = State.Moving;
                }


                break;
            case State.Moving:

                float rotationAmountThisFrame = direction * rotationSpeed * Time.deltaTime;
                degreesSurvayed += rotationAmountThisFrame;
                if (degreesSurvayed > amountOfDegreesToRotate) {
                    state = State.MovingOutOfFrame;
                    degreesSurvayed = 0f;
                    return;
                }
                this.transform.Rotate(0, 0, rotationAmountThisFrame);


                break;
            case State.MovingOutOfFrame:

                bomberScript.transform.localPosition += new Vector3(0, movementSpeed * Time.deltaTime, 0);
                if (bomberScript.transform.localPosition.y > CameraManager.Instance.viewportRadius + 1) {
                    bomberScript.transform.localPosition = new Vector3(0, CameraManager.Instance.viewportRadius + 1, 0);
                    bomberScript.gameObject.SetActive(false);
                    state = State.waitingToContinue;
                }

                break;
            case State.waitingToContinue:
                currentTimeWaiting += Time.deltaTime;
                if (currentTimeWaiting > waitingToContinueTime) {
                    currentTimeWaiting = 0f;
                    bomberScript.gameObject.SetActive(true);
                    this.transform.Rotate(0f, 0f, Random.Range(0f, 360f));
                    state = State.MovingIntoFrame;
                }

                break;
            case State.Bombing:

                float rotationAmountThisFrameBombing = direction * rotationSpeedBombing * Time.deltaTime;
                this.transform.Rotate(0, 0, rotationAmountThisFrameBombing);

                break;
        }

    }

    public void SetBombing() {
        if (state == State.Bombing) { bomberScript.DeployBomb(); return; }
        if (!(state == State.Moving)) { return; }
        state = State.Bombing;
    }

    public void EndBombing() {
        state = State.Moving;
    }

    public void OffsetTargetHeight(float lowerAmount) {
        baseTargetHeight -= lowerAmount;
    }

    public void Explode() {
        Destroy(this.gameObject);
    }
}
