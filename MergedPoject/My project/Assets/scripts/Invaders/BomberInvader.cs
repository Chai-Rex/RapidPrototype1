using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class BomberInvader : MonoBehaviour {

    [SerializeField] private BomberInvaderParent myParent;
    [SerializeField] private Transform dropTransform;
    [SerializeField] private Transform projectileHolder;


    [SerializeField] private GameObject Bomb;


    [SerializeField] private int pointsAwarded = 500;
    public void DeployBomb() {
        Instantiate(Bomb, dropTransform.position, Quaternion.identity, projectileHolder);
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
