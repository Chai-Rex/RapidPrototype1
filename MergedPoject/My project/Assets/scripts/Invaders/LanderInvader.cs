using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LanderInvader : MonoBehaviour {

    public System.Action killed;
    public System.Action bounce;


    [SerializeField] private int damageToPlanet = 50;
    [SerializeField] private int pointsAwarded = 100;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) {

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

            ScoreManager.Instance.AddToScore(pointsAwarded);
            SoundManager.Instance.SoundInvaderExplosion(this.transform.position);
            DestroySelf();
            return;
        }

    }

    private void DestroySelf() {
        ScoreManager.Instance.IncrementInvadersDestroyed();
        this.killed.Invoke();
        this.gameObject.SetActive(false);
    }
}
