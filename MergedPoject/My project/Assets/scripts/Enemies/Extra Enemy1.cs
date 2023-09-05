using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class ExtraEnemy1 : MonoBehaviour
{
    public int direction = 0;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float intervalBetweenBomb = 1f;
    private Vector3 endPosition = Vector3.zero;
    public Projectile bomb;


    private void Start()
    {

        InvokeRepeating("GenerateBomb", 0, intervalBetweenBomb);
    }


    private void Update()
    {
        Vector3 dir = endPosition - this.transform.position;
        dir = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90f;
        angle = angle < 0 ? angle + 360 : angle;
        this.transform.eulerAngles = new Vector3(0, 0, angle);
        this.transform.RotateAround(Vector3.zero, Vector3.forward, speed * Time.deltaTime);


    }
    
    private void GenerateBomb()
    {
        Instantiate(bomb, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ball") ||
            collision.gameObject.layer == LayerMask.NameToLayer("PlayerProjectile"))
        {
            //Debug.Log("hit");
            ScoreManager.Instance.AddToScore(100);
            this.GetComponentInParent<SpawnerA>().currentMonsterAmount -= 1;
            Destroy(this.gameObject);
            return;
        }

    }

}
