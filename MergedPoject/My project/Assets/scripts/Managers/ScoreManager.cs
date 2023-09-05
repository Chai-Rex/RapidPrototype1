using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.CullingGroup;

public class ScoreManager : MonoBehaviour {

    public static ScoreManager Instance { get; private set; }

    public int currentScore { get; private set; }

    public int projectilesBounced { get; private set; }
    public int invadersDestroyed { get; private set; }
    public int specialInvadersDestroyed { get; private set; }
    public int moonBounces { get; private set; }

    public event EventHandler OnScoreChange;


    private void Awake() {
        Instance = this;
    }

    public void AddToScore(int scorePointsOnKill) {
        currentScore += scorePointsOnKill;
        OnScoreChange?.Invoke(this, EventArgs.Empty);
    }
    public void IncrementProjectilesBounced() {
        projectilesBounced++;
    }
    public void IncrementInvadersDestroyed() {
        invadersDestroyed++;
    }
    public void IncrementSpecialInvadersDestroyed() {
        specialInvadersDestroyed++;
    }
    public void IncrementMoonBounces() {
        moonBounces++;
    }

}
