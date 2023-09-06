using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Random = UnityEngine.Random;

public class ProjectileManager : MonoBehaviour {

    public static ProjectileManager Instance { get; private set; }

    public event EventHandler OnProjectileTick;

    [SerializeField] private float tickrate = 0.25f;
    [SerializeField] private int numberOfLanderHandlers = 4;
    [SerializeField] private int numberOfBomberHandlers = 1;

    [Header("Must add up to max")]
    [SerializeField] private int max = 100;
    [SerializeField] private int landerProbability = 60;
    [SerializeField] private int bomberProbability = 40;

    public int selectedHandler { get; private set; }

    private float tickTimer = 0f;

    private void Awake() {
        Instance = this;
    }
    private void Start() {
        SelectNewHandler();
    }

    private void Update() {
        if (!GameStateManager.Instance.IsGamePlaying()) { return; }

        tickTimer += Time.deltaTime;
        if (tickTimer > tickrate) {
            tickTimer = 0f;
            OnProjectileTick?.Invoke(this, EventArgs.Empty);
        }

    }


    public void SelectNewHandler() {
        int selection = Random.Range(0, max);

        if (selection <= landerProbability) {
            selectedHandler = Random.Range(0, numberOfLanderHandlers);
            return;
        }
        if (selection > landerProbability && selection <= bomberProbability + landerProbability) {
            selectedHandler = numberOfLanderHandlers + numberOfBomberHandlers - 1;
            return;
        }


    }

}
