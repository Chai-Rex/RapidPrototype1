using System;
using UnityEngine;

public class SpecialLanderInvader : MonoBehaviour {

    [SerializeField] private GameObject shieldPrefab;

    [SerializeField] private CircleCollider2D shieldCollider2D;

    public System.Action killed;
    public System.Action bounce;



    [SerializeField] private int damageToPlanet = 50;
    [SerializeField] private int pointsAwarded = 200;

    private bool isShieldActive = true;


    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ball")) {

            if (isShieldActive) {
                DeactiveShield();
                return;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision) {


        if ( collision.gameObject.layer == LayerMask.NameToLayer("Player")) {
            if (Player.Instance.isDamageImmune) { return; }

            Player.Instance.isDamageImmune = true;

            Dome.Instance.LowerHeathBy(damageToPlanet / 2);
            SoundManager.Instance.SoundInvaderDamageHP(this.transform.position);
            this.bounce.Invoke();
            return;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Planet")) {

            Dome.Instance.LowerHeathBy(damageToPlanet);
            SoundManager.Instance.SoundInvaderDamageHP(this.transform.position);
            DestroySelf();
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ball") || 
            collision.gameObject.layer == LayerMask.NameToLayer("PlayerProjectile")) {

            if (isShieldActive) {
                DeactiveShield();
            } else {
                ScoreManager.Instance.AddToScore(pointsAwarded);
                SoundManager.Instance.SoundInvaderExplosion(this.transform.position);
                DestroySelf();
            }

        }

    }

    private void DeactiveShield() {
        shieldPrefab.gameObject.SetActive(false);
        shieldCollider2D.enabled = false;
        isShieldActive = false;
    }

    private void DestroySelf() {
        ScoreManager.Instance.IncrementInvadersDestroyed();
        ScoreManager.Instance.IncrementSpecialInvadersDestroyed();
        this.killed.Invoke();
        this.gameObject.SetActive(false);
    }
}
