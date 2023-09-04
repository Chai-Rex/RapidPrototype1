using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BallIndicatorUI : MonoBehaviour {

    public static BallIndicatorUI Instance { get; private set; }

    [SerializeField] private TMP_Text distanceText;
    [SerializeField] private GameObject UI;
    [SerializeField] private float OutOfBoundsOffset = 0.5f;

    public static GameObject Ball;


    private void Awake() {
        Instance = this;
    }

    private void Update() {

        if (!Ball) { UI.gameObject.SetActive(false); return; }

        if (Ball.transform.position.y > CameraManager.Instance.topRightCorner.y + OutOfBoundsOffset ||
            Ball.transform.position.y < CameraManager.Instance.bottomLeftCorner.y - OutOfBoundsOffset ||
            Ball.transform.position.x > CameraManager.Instance.topRightCorner.x + OutOfBoundsOffset ||
            Ball.transform.position.x < CameraManager.Instance.bottomLeftCorner.x - OutOfBoundsOffset) {

            // face towards ball
            Vector3 direction = Ball.transform.position - this.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            this.transform.rotation = rotation;

            // 
            UI.gameObject.SetActive(true);
            distanceText.text = (int)Vector3.Distance(this.transform.position, Ball.transform.position) + 1 + "u";
            
        } else {
            UI.gameObject.SetActive(false);
        }


    }


}
