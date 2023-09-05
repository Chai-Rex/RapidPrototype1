using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.CullingGroup;

public class SurvayerInvader : MonoBehaviour {

    [SerializeField] private GameObject Drop;
    [SerializeField] private Transform DropParent;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ball") ||
            collision.gameObject.layer == LayerMask.NameToLayer("PlayerProjectile")) {

            Instantiate(Drop, this.transform.position, Quaternion.identity, DropParent);

            ScoreManager.Instance.IncrementInvadersDestroyed();
            ScoreManager.Instance.IncrementSpecialInvadersDestroyed();
            ScoreManager.Instance.AddToScore(3000);

            this.gameObject.SetActive(false);
        }
    }
}
