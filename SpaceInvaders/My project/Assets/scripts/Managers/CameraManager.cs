using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    public static CameraManager Instance { get; private set; }

    public Vector3 leftEdge { get; private set; }
    public Vector3 rightEdge { get; private set; }
    public Vector3 topEdge { get; private set; }
    public Vector3 bottomEdge { get; private set; }
    public float viewportRadius { get; private set; }

    private void Awake() {
        Instance = this;

        // set world bounds
        leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
        topEdge = Camera.main.ViewportToWorldPoint(Vector3.up);
        bottomEdge = Camera.main.ViewportToWorldPoint(Vector3.down);

        // set radius
        viewportRadius = HypotenuseLength(Camera.main.orthographicSize, Camera.main.orthographicSize);
    }

    float HypotenuseLength(float sideALength, float sideBLength) {
        return Mathf.Sqrt(sideALength * sideALength + sideBLength * sideBLength);
    }
}
