using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu()]
public class AudioClipRefSO : ScriptableObject {
    public AudioClip[] invaderBallBounce;
    public AudioClip[] playerBallBounce;
    public AudioClip[] planetBallBounce;

    public AudioClip[] projectileBounce;

    public AudioClip[] projectileDamageHP;
    public AudioClip[] invaderDamageHP;
    public AudioClip[] gainHP;

    public AudioClip[] projectileExplosion;
    public AudioClip[] invaderExplosion;

    public AudioClip[] invaderShoot;
    public AudioClip[] playerShoot;

    public AudioClip[] gameOver;

}
