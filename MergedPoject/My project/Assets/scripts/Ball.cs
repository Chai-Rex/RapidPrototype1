using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour {


    [SerializeField] private Rigidbody2D rigidbody2d;

    //private Vector3 lastVelocity;

    private float triggerTimer = 0f;
    private float triggerTime = 0.1f;
    private bool isTriggerArmed = false;
    private void Awake() {
        if (rigidbody2d == null) {
            rigidbody2d = GetComponent<Rigidbody2D>();
        }
    }

    private void Start() {
        GravityManager.attractees.Add(rigidbody2d);
        BallIndicatorUI.Ball = this.gameObject;
        rigidbody2d.velocity = new Vector2(0, 0);
        rigidbody2d.AddForce(new Vector2(
            this.transform.position.x - Dome.Instance.transform.position.x,
            this.transform.position.y - Dome.Instance.transform.position.y
            ).normalized * Player.Instance.G);
    }

    private void Update() {

        //lastVelocity = rigidbody2d.velocity;
        if (!isTriggerArmed) {
            triggerTimer += Time.deltaTime;
            if (triggerTimer >= triggerTime) {
                isTriggerArmed = true;
            }
        }
    }

    private void FixedUpdate() {
        if (this.transform.position.y > CameraManager.Instance.topRightCorner.y + 1 ||
            this.transform.position.y < CameraManager.Instance.bottomLeftCorner.y - 1 ||
            this.transform.position.x > CameraManager.Instance.topRightCorner.x + 1 ||
            this.transform.position.x < CameraManager.Instance.bottomLeftCorner.x - 1) {


            rigidbody2d.AddForce(new Vector2(
                Dome.Instance.transform.position.x - this.transform.position.x,
                Dome.Instance.transform.position.y - this.transform.position.y
                ).normalized * 25);
        }
    }

    private void OnDestroy() {
        GravityManager.attractees.Remove(rigidbody2d);
        BallIndicatorUI.Ball = null;
        Destroy(this.gameObject);
    }


    private void OnCollisionEnter2D(Collision2D collision) {

        if (!isTriggerArmed) { return; }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) {

            isTriggerArmed = false;

            ScoreManager.Instance.IncrementMoonBounces();

            triggerTimer = 0f;
            rigidbody2d.velocity = new Vector2(0, 0);
            rigidbody2d.AddForce(new Vector2(
                this.transform.position.x - Player.Instance.transform.position.x,
                this.transform.position.y - Player.Instance.transform.position.y
                ).normalized * Player.Instance.G);

        }

        //if (collision.gameObject.layer == LayerMask.NameToLayer("Planet")) {
        //    //GravityManager.attractees.Remove(rigidbody2d);
        //    //Destroy(this.gameObject);
        //    var speed = lastVelocity.magnitude;
        //    var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

        //    rigidbody2d.velocity = direction * Mathf.Max(speed, 0f);

        //    return;
        //}

        //if (collision.gameObject.layer == LayerMask.NameToLayer("Invader")) {
        //    var speed = lastVelocity.magnitude;
        //    var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

        //    rigidbody2d.velocity = direction * Mathf.Max(speed, 0f);



        //    return;
        //}

        //if (!triggerArmed) { return; }

        //if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) {

        //    triggerArmed = false;
        //    triggerTimer = 0f;
        //    rigidbody2d.velocity = new Vector2(0, 0);
        //    rigidbody2d.AddForce(new Vector2(
        //        this.transform.position.x - Player.Instance.transform.position.x,
        //        this.transform.position.y - Player.Instance.transform.position.y
        //        ).normalized * g);
        //    Debug.Log("trigger");

        //}
    }






}
