using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvayorHandler : MonoBehaviour {


    [SerializeField] private int direction = 1;
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private float targetHieight = 15f;
    [SerializeField] private float amountOfDegreesToSurvay = 180f;

    [SerializeField] private float waitingToStartTimer = 10f;

    [SerializeField] private GameObject Survayor;


    private float currentTimeWaiting = 0f;
    private float degreesSurvayed = 0f;

    private enum State {
        WaitingToStart,
        MovingIntoFrame,
        Survaying,
        MovingOutOfFrame
    }
    private State state;


    private void Start() {
        state = State.WaitingToStart;
        Survayor.transform.localPosition = new Vector3(0, CameraManager.Instance.viewportRadius + 1, 0);
        Survayor.gameObject.SetActive(false);
    }

    private void Update() {



        switch (state) {
            case State.WaitingToStart:
                currentTimeWaiting += Time.deltaTime;
                if (currentTimeWaiting > waitingToStartTimer) {
                    currentTimeWaiting = 0f;
                    Survayor.gameObject.SetActive(true);
                    state = State.MovingIntoFrame;
                }

                break;
            case State.MovingIntoFrame:

                Survayor.transform.localPosition -= new Vector3(0, movementSpeed * Time.deltaTime, 0);
                if (Survayor.transform.localPosition.y < targetHieight) {
                    Survayor.transform.localPosition = new Vector3(0, targetHieight, 0);
                    state = State.Survaying;
                }


                break;
            case State.Survaying:

                float rotationAmountThisFrame = direction * rotationSpeed * Time.deltaTime;
                degreesSurvayed += rotationAmountThisFrame;
                if (degreesSurvayed > amountOfDegreesToSurvay) {
                    state = State.MovingOutOfFrame;
                    degreesSurvayed = 0f;
                    return;
                }
                this.transform.Rotate(0, 0, rotationAmountThisFrame);


                break;
            case State.MovingOutOfFrame:

                Survayor.transform.localPosition += new Vector3(0, movementSpeed * Time.deltaTime, 0);
                if (Survayor.transform.localPosition.y > CameraManager.Instance.viewportRadius + 1) {
                    Survayor.transform.localPosition = new Vector3(0, CameraManager.Instance.viewportRadius + 1, 0);
                    Survayor.gameObject.SetActive(false);
                    state = State.WaitingToStart;
                }

                break;
        }

    }


}
