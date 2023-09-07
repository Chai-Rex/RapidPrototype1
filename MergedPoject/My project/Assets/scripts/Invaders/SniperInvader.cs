using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperInvader : MonoBehaviour {


    [SerializeField] private SniperInvaderParent myParent;
    [SerializeField] private Transform dropTransform;
    [SerializeField] private Transform projectileHolder;


    [SerializeField] private GameObject EnergyBlast;


    [SerializeField] private int pointsAwarded = 500;
    public void FireShot() {
        Instantiate(EnergyBlast, dropTransform.position, Quaternion.identity, projectileHolder);
    }


    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ball")) {

            ScoreManager.Instance.AddToScore(pointsAwarded);
            SoundManager.Instance.SoundInvaderExplosion(this.transform.position);
            DestroySelf();
            return;
        }

    }


    private void DestroySelf() {
        ScoreManager.Instance.IncrementInvadersDestroyed();
        myParent.Explode();
    }



}
