using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class ExtraEnemy1 : MonoBehaviour
{
    public int direction = 0;
    private float speed = 3f;
    private float bombFrequency = 2f;

    private Vector3 movedirection;
    public GameObject bomb;

    public System.Action killed;

    private Vector3 leftEdge;
    private Vector3 rightEdge;



    private void Start()
    {
        leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        DecideMovingDirection();
        InvokeRepeating("GenerateBomb", 0, bombFrequency);
    }


    private void Update()
    {
        transform.position += movedirection * speed * Time.deltaTime;
        if (this.direction == 0 && transform.position.x > rightEdge.x) 
        {
            Destroy(this.gameObject);
        }
        if (this.direction == 1 && transform.position.x < leftEdge.x)
        {
            Destroy(this.gameObject);
        }
        if (transform.position.y<10.0f)
        {
            Destroy(this.gameObject);
        }


    }
    private void DecideMovingDirection()
    {
        switch (direction)
        {
            case 0:
                {
                    //left to right movement
                    movedirection = new Vector3(1, 0, 0);
                    break;
                }
            case 1:
                {
                    //right to left movement
                    movedirection = new Vector3(-1, 0, 0);
                    break;
                }
        }
    }
    private void GenerateBomb()
    {
        Instantiate(bomb, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            //Debug.Log("hit");
            ScoreManager.Instance.AddToScore(100);
            Destroy(this.gameObject);

        }
    }

}
