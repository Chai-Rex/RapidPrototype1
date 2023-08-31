using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.CullingGroup;

public class SurvayerInvader : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ball") ||
            collision.gameObject.layer == LayerMask.NameToLayer("PlayerProjectile")) {

            ScoreManager.Instance.AddToScore(3000);
            this.gameObject.SetActive(false);
        }
    }
}
