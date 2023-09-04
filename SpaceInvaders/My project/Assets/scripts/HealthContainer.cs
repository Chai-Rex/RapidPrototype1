using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthContainer : MonoBehaviour {

    [SerializeField] private Rigidbody2D rigidbody2d;
    [SerializeField] private int healing = 100;

    private void Awake() {
        if (rigidbody2d == null) {
            rigidbody2d = GetComponent<Rigidbody2D>();
        }
    }

    private void Start() {
        GravityManager.attractees.Add(rigidbody2d);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) {

            Dome.Instance.RaiseHealthBy(healing);

            GravityManager.attractees.Remove(rigidbody2d);
            Destroy(this.gameObject);
        }
    }
}
