using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Random = UnityEngine.Random;

public class ProjectileManager : MonoBehaviour {

    public static ProjectileManager Instance { get; private set; }

    public event EventHandler OnProjectileTick;

    [SerializeField] private float tickrate = 0.25f;
    [SerializeField] private int numberOfQuandrants = 4;
    public int selectedQuadrant { get; private set; }

    private float tickTimer = 0f;

    private void Awake() {
        Instance = this;
    }
    private void Start() {
        SelectNewQuadrant();
    }

    private void Update() {
        if (!GameStateManager.Instance.IsGamePlaying()) { return; }

        tickTimer += Time.deltaTime;
        if (tickTimer > tickrate) {
            tickTimer = 0f;
            OnProjectileTick?.Invoke(this, EventArgs.Empty);
        }

    }


    public void SelectNewQuadrant() {
        selectedQuadrant = Random.Range(0, numberOfQuandrants);

    }
}
