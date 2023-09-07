using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyProjectile : MonoBehaviour {

    [SerializeField] private Rigidbody2D rigidbody2d;
    [SerializeField] private TrailRenderer trailRenderer;

    [SerializeField] private float g = 200f;
    [SerializeField] private float gIncreaseOnCollision = 400f;

    [SerializeField] private int damageToPlanet = 20;

    [SerializeField] private Color playerColor;
    [SerializeField] private Color invaderColor;

    [SerializeField] private GameObject lightningEffect;

    private void Start() {
        SoundManager.Instance.SoundInvaderShoot(this.transform.position);
        rigidbody2d.AddForce(new Vector2(
            Dome.Instance.transform.position.x - this.transform.position.x,
            Dome.Instance.transform.position.y - this.transform.position.y
            ).normalized * g);
    }

    private void Update() {
        if (this.transform.position.y > CameraManager.Instance.topRightCorner.y + 1 ||
            this.transform.position.y < CameraManager.Instance.bottomLeftCorner.y - 1 ||
            this.transform.position.x > CameraManager.Instance.topRightCorner.x + 1 ||
            this.transform.position.x < CameraManager.Instance.bottomLeftCorner.x - 1) {

            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Ball")) {

            SoundManager.Instance.SoundProjectileBounce(this.transform.position);
            ScoreManager.Instance.IncrementProjectilesBounced();

            trailRenderer.enabled = true;

            rigidbody2d.velocity = new Vector2(0, 0);
            rigidbody2d.AddForce(new Vector2(
                this.transform.position.x - collision.transform.position.x,
                this.transform.position.y - collision.transform.position.y
                ).normalized * gIncreaseOnCollision);

            this.gameObject.layer = LayerMask.NameToLayer("PlayerProjectile");
            return;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Planet")) {
            if (this.gameObject.layer == LayerMask.NameToLayer("PlayerProjectile")) {
                // bounce back
                SoundManager.Instance.SoundProjectileBounce(this.transform.position);
                rigidbody2d.velocity = new Vector2(0, 0);
                rigidbody2d.AddForce(new Vector2(
                    this.transform.position.x - collision.transform.position.x,
                    this.transform.position.y - collision.transform.position.y
                    ).normalized * gIncreaseOnCollision);
            } else {
                Instantiate(lightningEffect, this.transform.position, Quaternion.identity, null);
                SoundManager.Instance.SoundProjectileDamageHP(this.transform.position);
                Dome.Instance.LowerHeathBy(damageToPlanet);
                Destroy(this.gameObject);
            }
            return;
        }

    }
}
