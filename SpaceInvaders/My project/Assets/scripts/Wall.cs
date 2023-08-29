using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {


    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float edgePadding = 1f;

    private Vector3 leftEdge;
    private Vector3 rightEdge;


    private void Start() {
        // set world bounds
        leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
    }

    private void Update() {
        if (Input.GetKey(KeyCode.LeftArrow)) {
            // move left
            if (this.transform.position.x >= leftEdge.x + edgePadding) {
                this.transform.position += Vector3.left * speed * Time.deltaTime;
            }

        } else if (Input.GetKey(KeyCode.RightArrow)) {
            // move right
            if (this.transform.position.x <= rightEdge.x - edgePadding) {
                this.transform.position += Vector3.right * speed * Time.deltaTime;
            }

        }

    }
}
