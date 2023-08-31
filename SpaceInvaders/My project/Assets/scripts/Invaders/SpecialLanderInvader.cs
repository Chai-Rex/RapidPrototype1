using System;
using UnityEngine;

public class SpecialLanderInvader : MonoBehaviour {

    [SerializeField] private GameObject shieldPrefab;

    [SerializeField] private CircleCollider2D shieldCollider2D;

    public System.Action killed;

    public static event EventHandler onLanderKilled;


    private bool isShieldActive = true;


    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ball") ||
            collision.gameObject.layer == LayerMask.NameToLayer("PlayerProjectile")) {

            if (isShieldActive) {
                shieldPrefab.gameObject.SetActive(false);
                shieldCollider2D.radius = 0.4f;
                shieldCollider2D.isTrigger = true;
                isShieldActive = false;
                return;
            }
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Planet") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Player")) {
            Dome.Instance.LowerHeathBy(50);
            DestroySelf();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ball") ||
            collision.gameObject.layer == LayerMask.NameToLayer("PlayerProjectile")) {
            //Debug.Log("hit");
            ScoreManager.Instance.AddToScore(100);
            DestroySelf();
            return;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Planet") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Player")) {
            Dome.Instance.LowerHeathBy(50);
            DestroySelf();
        }

    }

    private void DestroySelf() {
        this.killed.Invoke();
        onLanderKilled?.Invoke(this, EventArgs.Empty);
        this.gameObject.SetActive(false);
    }
}
