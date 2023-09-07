using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPlanet : MonoBehaviour {

    [SerializeField] private Rigidbody2D rigidbody2d;

    private void Awake() {

        if (rigidbody2d == null) {
            rigidbody2d = GetComponent<Rigidbody2D>();
        }
    }

    private void Start() {
        MainMenuGravityManager.attractors.Add(rigidbody2d);

    }

    private void OnDestroy() {
        MainMenuGravityManager.attractors.Remove(rigidbody2d);
    }
}
