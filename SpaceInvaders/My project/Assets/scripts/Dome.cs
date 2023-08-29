using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Dome : MonoBehaviour {

    public static Dome Instance { get; private set; }

    public event EventHandler OnLivesChange;

    [SerializeField] private int remainingLives = 3;

    private void Awake() {
        Instance = this;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        remainingLives--;
        OnLivesChange?.Invoke(this, EventArgs.Empty);

        if (remainingLives <= 0) {
            GameStateManager.Instance.EndGame();
        }
    }

    public int GetLives() {
        return remainingLives;
    }

}
