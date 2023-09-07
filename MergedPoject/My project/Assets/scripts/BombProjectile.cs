using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProjectile : MonoBehaviour
{

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
    [SerializeField] private CircleCollider2D collider2d;
    [SerializeField] private int damage=3;
    [SerializeField] private float radiusExplosion = 0.4f;
    [SerializeField] private GameObject explosionEffect;
    private bool isExploding = false;
    [SerializeField] private float explodeTime=1f;

    private void Start()
    {
        SoundManager.Instance.SoundInvaderShoot(this.transform.position);
        rigidbody2d.AddForce(new Vector2(
            Dome.Instance.transform.position.x - this.transform.position.x,
            Dome.Instance.transform.position.y - this.transform.position.y
            ).normalized * g);
    }

    private void Update()
    {
        if (this.transform.position.y > CameraManager.Instance.topRightCorner.y + 1 ||
            this.transform.position.y < CameraManager.Instance.bottomLeftCorner.y - 1 ||
            this.transform.position.x > CameraManager.Instance.topRightCorner.x + 1 ||
            this.transform.position.x < CameraManager.Instance.bottomLeftCorner.x - 1)
        {


            Destroy(this.gameObject);
        }
        if (isExploding)
        {
            explodeTime -= Time.deltaTime;
            if (explodeTime < 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {

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

        if (collision.gameObject.layer == LayerMask.NameToLayer("Planet"))
        {
            if (this.gameObject.layer == LayerMask.NameToLayer("PlayerProjectile"))
            {
                // bounce back
                SoundManager.Instance.SoundProjectileBounce(this.transform.position);
                rigidbody2d.velocity = new Vector2(0, 0);
                rigidbody2d.AddForce(new Vector2(
                    this.transform.position.x - collision.transform.position.x,
                    this.transform.position.y - collision.transform.position.y
                    ).normalized * g);
            }
            else
            {
                SoundManager.Instance.SoundProjectileDamageHP(this.transform.position);
                Dome.Instance.LowerHeathBy(damage);
                Destroy(this.gameObject);
            }
            return;
        }



        if (collision.gameObject.layer == LayerMask.NameToLayer("Invader") || 
            collision.gameObject.layer == LayerMask.NameToLayer("Bomber"))
        {
            rigidbody2d.velocity = new Vector2(0, 0);

            collider2d.radius = radiusExplosion;
            Instantiate(explosionEffect, this.transform.position, Quaternion.identity,  null);
            spriteRenderer.sprite = null;
            isExploding = true;
            
        }

    }

}
