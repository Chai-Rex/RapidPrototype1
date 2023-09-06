using UnityEngine;
using System;

public class Projectile : MonoBehaviour {

    [SerializeField] private Rigidbody2D rigidbody2d;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private float g = 1f;
    [SerializeField] private Color playerColor;
    [SerializeField] private Color invaderColor;
    [SerializeField] private Material playerTrail;
    [SerializeField] private Material invaderTrail;
    [SerializeField] private GameObject InvaderHitEffect = null;
    [SerializeField] private GameObject planetHitEffect = null;
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

            rigidbody2d.velocity = new Vector2(0, 0);
            rigidbody2d.AddForce(new Vector2(
                this.transform.position.x - collision.transform.position.x,
                this.transform.position.y - collision.transform.position.y
                ).normalized * g);

            spriteRenderer.color = playerColor;
            trailRenderer.material = playerTrail;
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
                    ).normalized * g);
            } else {
                if (planetHitEffect != null) {
                    Instantiate(planetHitEffect, transform.position, transform.rotation, null);
                }
                SoundManager.Instance.SoundProjectileDamageHP(this.transform.position);
                Dome.Instance.LowerHeathBy(1);
                Destroy(this.gameObject);
            }
            return;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Bomber")) {
            SoundManager.Instance.SoundProjectileBounce(this.transform.position);

            rigidbody2d.velocity = new Vector2(0, 0);
            rigidbody2d.AddForce(new Vector2(
                Dome.Instance.transform.position.x - this.transform.position.x,
                Dome.Instance.transform.position.y - this.transform.position.y 
                ).normalized * g);

            spriteRenderer.color = invaderColor;
            trailRenderer.material = invaderTrail;
            this.gameObject.layer = LayerMask.NameToLayer("InvaderProjectile");
        }


        if (collision.gameObject.layer == LayerMask.NameToLayer("Invader")) {
            if (InvaderHitEffect != null) {
                Instantiate(InvaderHitEffect, transform.position, transform.rotation, null);
            }
            Destroy(this.gameObject);
        }

    }

}
