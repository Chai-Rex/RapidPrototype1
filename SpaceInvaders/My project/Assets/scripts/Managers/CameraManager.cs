using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    public static CameraManager Instance { get; private set; }

    public Vector3 leftEdge { get; private set; }
    public Vector3 rightEdge { get; private set; }
    public Vector3 topEdge { get; private set; }
    public Vector3 bottomEdge { get; private set; }

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        // set world bounds
        leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
        topEdge = Camera.main.ViewportToWorldPoint(Vector3.up);
        bottomEdge = Camera.main.ViewportToWorldPoint(Vector3.down);
    }


}
