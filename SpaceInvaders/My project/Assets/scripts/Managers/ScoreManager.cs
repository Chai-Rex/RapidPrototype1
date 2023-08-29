using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.CullingGroup;

public class ScoreManager : MonoBehaviour {

    public static ScoreManager Instance { get; private set; }

    private int currentScore = 0;

    public event EventHandler OnScoreChange;


    private void Awake() {
        Instance = this;
    }

    public void AddToScore(int scorePointsOnKill) {
        currentScore += scorePointsOnKill;
        OnScoreChange?.Invoke(this, EventArgs.Empty);
    }

    public int GetScore() {
        return currentScore;
    }

}
