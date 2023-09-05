using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ExtraEnemy2 : MonoBehaviour
{

    public int direction = 0;
    [SerializeField] float speed = 3f;
    private int numPos = 0;
    private float des;
    public GameObject bomb;
    private Vector3 endPosition = Vector3.zero;
    [SerializeField] private float intervalBetweenBomb = 0.2f;

    //Zigezag Pos
    
    static Vector3 pos11,pos12,pos13,pos14,pos21,pos22,pos23,pos24,pos31,pos32,pos33,pos34,pos41,pos42,pos43,pos44 = new Vector3();
    public Vector3[] pos1={pos11, pos12, pos13, pos14, pos21, pos22, pos23, pos24, pos31, pos32, pos33, pos34, pos41, pos42, pos43, pos44 };
    public Vector3[] pos2={pos21, pos22, pos23, pos24, pos31, pos32, pos33, pos34, pos41, pos42, pos43, pos44, pos11, pos12, pos13, pos14 };
    

    private void Start()
    {

    }


    private void Update()
    {
        Vector3 dir = endPosition - this.transform.position;
        dir = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90f;
        angle = angle < 0 ? angle + 360 : angle;
        this.transform.eulerAngles = new Vector3(0, 0, angle);



        switch (direction)
        {
            case 0:
                {
                    des = Vector3.Distance(this.transform.position, pos1[numPos]);
                    transform.position = Vector3.MoveTowards(this.transform.position, pos1[numPos], Time.deltaTime * speed);
                    
                    if (des < 0.1f && numPos < pos1.Length - 1)
                    {
                        GenerateBomb(pos1[numPos]);
                        numPos++;
                    }

                    break;
                }
            case 1:
                {
                    des = Vector3.Distance(this.transform.position, pos2[numPos]);
                    transform.position = Vector3.MoveTowards(this.transform.position, pos2[numPos], Time.deltaTime * speed);
                    if (des < 0.1f && numPos < pos2.Length - 1)
                    {
                        GenerateBomb(pos2[numPos]);
                        numPos++;
                    }

                    break;
                }
        }
        


                }
    private void GenerateBomb(Vector3 pos)
    {
        float j = 0f;
        for (int i = 0; i < 5; i++)
        {
            j += intervalBetweenBomb;
            StartCoroutine(DelayGenerateBomb(j, pos));
        }
    }
    IEnumerator DelayGenerateBomb(float i, Vector3 pos)
    {
        yield return new WaitForSeconds(i);
        Instantiate(bomb, pos, Quaternion.identity);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ball") ||
            collision.gameObject.layer == LayerMask.NameToLayer("PlayerProjectile"))
        {
            //Debug.Log("hit");
            ScoreManager.Instance.AddToScore(100);
            this.GetComponentInParent<SpawnerB>().currentMonsterAmount -= 1;
            Destroy(this.gameObject);
            return;
        }


    }
}
