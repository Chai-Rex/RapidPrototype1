using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LanderInvader : MonoBehaviour {
    public SpriteRenderer spriteRenderer;

    public System.Action killed;

    public static event EventHandler onLanderKilled;

    public void SetSprite(Sprite sprite) {
        spriteRenderer.sprite = sprite;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Laser")) {
            //Debug.Log("hit");
            ScoreManager.Instance.AddToScore(100);
            this.killed.Invoke();
            onLanderKilled?.Invoke(this, EventArgs.Empty);
            this.gameObject.SetActive(false);
        }
    }

}
