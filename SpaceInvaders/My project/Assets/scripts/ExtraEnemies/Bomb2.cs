using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb2 : MonoBehaviour
{
    private Vector3 leftEdge;
    private Vector3 rightEdge;
    private Vector3 topEdge;
    private Vector3 bottomEdge;

    public System.Action killed;

    public static event EventHandler onLanderKilled;


    // Start is called before the first frame update
    void Start()
    {
        leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
        topEdge = Camera.main.ViewportToScreenPoint(Vector3.up);
        bottomEdge=Camera.main.ViewportToWorldPoint(Vector3.down);  
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.x < leftEdge.x || transform.position.x> rightEdge.x || transform.position.y<bottomEdge.y)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Laser") || (collision.gameObject.layer == LayerMask.NameToLayer("Player")))
        {
            //Debug.Log("hit");
            ScoreManager.Instance.AddToScore(100);
            onLanderKilled?.Invoke(this, EventArgs.Empty);
            Destroy(this.gameObject);
        }
    }

}
