using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Dome : MonoBehaviour {

    public static Dome Instance { get; private set; }

    public event EventHandler OnLivesChange;

    [SerializeField] private int remainingHealth = 200;

    [SerializeField] private Rigidbody2D rigidbody2d;

    private void Awake() {
        Instance = this;

        if (rigidbody2d == null) {
            rigidbody2d = GetComponent<Rigidbody2D>();
        }
    }

    private void Start() {
       GravityManager.attractors.Add(rigidbody2d);
    }

    private void OnDestroy() {
        GravityManager.attractors.Remove(rigidbody2d);
    }

    public void LowerHeathBy(int damage) {
        remainingHealth -= damage;
        OnLivesChange?.Invoke(this, EventArgs.Empty);

        if (remainingHealth <= 0) {
            GameStateManager.Instance.EndGame();
        }
    }

    public int GetLives() {
        return remainingHealth;
    }

}
