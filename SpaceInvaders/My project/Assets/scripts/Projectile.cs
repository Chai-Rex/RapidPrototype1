using UnityEngine;
using System;

public class Projectile : MonoBehaviour {

    [SerializeField] private Rigidbody2D rigidbody2d;
    [SerializeField] private float g = 1f;

    private void Start() {
        rigidbody2d.AddForce(new Vector2(
            Dome.Instance.transform.position.x - this.transform.position.x,
            Dome.Instance.transform.position.y - this.transform.position.y
            ).normalized * g);
    }

    private void Update() {
        if (this.transform.position.y > CameraManager.Instance.topEdge.y + 1 ||
            this.transform.position.y < CameraManager.Instance.bottomEdge.y - 1 ||
            this.transform.position.x > CameraManager.Instance.rightEdge.x + 1 ||
            this.transform.position.x < CameraManager.Instance.leftEdge.x - 1) {


            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) {

            rigidbody2d.velocity = new Vector2(0, 0);
            rigidbody2d.AddForce(new Vector2(
                this.transform.position.x - Player.Instance.transform.position.x,
                this.transform.position.y - Player.Instance.transform.position.y
                ).normalized * g);

            this.gameObject.layer = LayerMask.NameToLayer("PlayerProjectile");
            return;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Planet")) {
            Dome.Instance.LowerHeathBy(1);
            Destroy(this.gameObject);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Invader")) {
            Destroy(this.gameObject);
        }

    }

}
