using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Dome : MonoBehaviour {

    public static Dome Instance { get; private set; }

    public event EventHandler OnLivesChange;

    [SerializeField] private int maxHealth = 300;

    [SerializeField] private Rigidbody2D rigidbody2d;

    private int remainingHealth = 0;

    private void Awake() {
        Instance = this;

        if (rigidbody2d == null) {
            rigidbody2d = GetComponent<Rigidbody2D>();
        }
    }

    private void Start() {
        GravityManager.attractors.Add(rigidbody2d);
        remainingHealth = maxHealth;
        OnLivesChange?.Invoke(this, EventArgs.Empty);
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

    public void RaiseHealthBy(int healing) {
        if (remainingHealth + healing > maxHealth)
            remainingHealth = maxHealth;
        else
            remainingHealth += healing;

        OnLivesChange?.Invoke(this, EventArgs.Empty);
    }

    public int GetLives() {
        return remainingHealth;
    }
    public float GetNormalizedLives()
    {
        float normalized = (float)remainingHealth / maxHealth;
        return normalized;
    }

}
