using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    public static CameraManager Instance { get; private set; }

    public Vector3 bottomLeftCorner { get; private set; }
    public Vector3 bottomRightCorner { get; private set; }
    public Vector3 topLeftCorner { get; private set; }
    public Vector3 topRightCorner { get; private set; }
    public float viewportRadius { get; private set; }

    private void Awake() {
        Instance = this;

        // set world bounds
        bottomLeftCorner = Camera.main.ViewportToWorldPoint(Vector3.zero);
        bottomRightCorner = Camera.main.ViewportToWorldPoint(Vector3.right);
        topLeftCorner = Camera.main.ViewportToWorldPoint(Vector3.up);
        topRightCorner = Camera.main.ViewportToWorldPoint(Vector3.one);

        // set radius
        viewportRadius = HypotenuseLength(Camera.main.orthographicSize, Camera.main.orthographicSize);
    }

    float HypotenuseLength(float sideALength, float sideBLength) {
        return Mathf.Sqrt(sideALength * sideALength + sideBLength * sideBLength);
    }
}
