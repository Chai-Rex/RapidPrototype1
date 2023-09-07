using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBall : MonoBehaviour {

    [SerializeField] private Rigidbody2D rigidbody2d;
    [SerializeField] public float G = 2300f;

    private void Awake() {
        if (rigidbody2d == null) {
            rigidbody2d = GetComponent<Rigidbody2D>();
        }
    }

    private void Start() {
        MainMenuGravityManager.attractees.Add(rigidbody2d);
    }



    private void OnDestroy() {
        MainMenuGravityManager.attractees.Remove(rigidbody2d);
        Destroy(this.gameObject);
    }


    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) {

            rigidbody2d.velocity = new Vector2(0, 0);
            rigidbody2d.AddForce(Vector2.up * G);

        }

    }
}
