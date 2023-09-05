using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LanderInvader : MonoBehaviour {

    public System.Action killed;
    public static event EventHandler onLanderKilled;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ball") ||
            collision.gameObject.layer == LayerMask.NameToLayer("PlayerProjectile")) {
            //Debug.Log("hit");
            ScoreManager.Instance.AddToScore(100);
            ScoreManager.Instance.IncrementInvadersDestroyed();
            this.killed.Invoke();
            onLanderKilled?.Invoke(this, EventArgs.Empty);
            this.gameObject.SetActive(false);
            return;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Planet") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Player")) {
            Dome.Instance.LowerHeathBy(50);
            this.killed.Invoke();
            onLanderKilled?.Invoke(this, EventArgs.Empty);
            this.gameObject.SetActive(false);
        }

    }
}
