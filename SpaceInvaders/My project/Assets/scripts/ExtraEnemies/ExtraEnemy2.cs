using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ExtraEnemy2 : MonoBehaviour
{

    public int direction = 0;
    private float speed = 3f;
    private int numPos = 0;
    private float des;
    
    public GameObject bomb;

    public System.Action killed;

    private Vector3 leftEdge;
    private Vector3 rightEdge;
    private Vector3 topEdge;

    //Zigezag Pos

    static Vector3 lPos1, lPos2, lPos3, lPos4, lPos5, rPos1, rPos2, rPos3, rPos4, rPos5 = new Vector3();
    private Vector3[] leftPos = { lPos1,lPos2,lPos3,lPos4,lPos5 };
    private Vector3[] rightPos = { rPos1, rPos2, rPos3, rPos4, rPos5 };
    

    private void Start()
    {
        leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
        topEdge = Camera.main.ViewportToWorldPoint(Vector3.up);

        for (int i = 0; i < 5; i++)
        {
            leftPos[i] = GameObject.Find("SpawnerB").GetComponent<SpawnerB>().Pos[i].transform.position;
        }
        for (int i=0;i<5;i++)
        {
            rightPos[i]= GameObject.Find("SpawnerB").GetComponent<SpawnerB>().Pos[i+5].transform.position;
        }

    }


    private void Update()
    {
        for (int i = 0; i < 5; i++)
        {
            leftPos[i] = GameObject.Find("SpawnerB").GetComponent<SpawnerB>().Pos[i].transform.position;
            leftPos[i].y -= topEdge.y- GameObject.Find("SpawnerB").GetComponent<SpawnerB>().generatePos.y;
        }
        for (int i = 0; i < 5; i++)
        {
            rightPos[i] = GameObject.Find("SpawnerB").GetComponent<SpawnerB>().Pos[i + 5].transform.position;
            rightPos[i].y-= topEdge.y - GameObject.Find("SpawnerB").GetComponent<SpawnerB>().generatePos.y;
        }

        switch (direction)
        {
            case 0:
                {
                    //left to right movement
                    des = Vector3.Distance(this.transform.position, leftPos[numPos]);
                    transform.position = Vector3.MoveTowards(this.transform.position, leftPos[numPos], Time.deltaTime * speed);
                    if (des < 0.1f && numPos < leftPos.Length - 1)
                    {
                        //stop
                        
                        GenerateBomb(leftPos[numPos]);
                        numPos++;
                    }
                    break;
                }
            case 1:
                {
                    //right to left movement
                    des = Vector3.Distance(this.transform.position, rightPos[numPos]);
                    transform.position = Vector3.MoveTowards(this.transform.position, rightPos[numPos], Time.deltaTime * speed);
                    if (des < 0.1f && numPos < rightPos.Length - 1)
                    {
                        //stop
                        
                        GenerateBomb(rightPos[numPos]);
                        numPos++;
                    }
                    break;
                }
        }

        if (this.direction == 0 && transform.position.x > rightEdge.x)
        {
            Destroy(this.gameObject);
        }
        if (this.direction == 1 && transform.position.x < leftEdge.x)
        {
            Destroy(this.gameObject);
        }
        if (transform.position.y < 10.0f)
        {
            Destroy(this.gameObject);
        }


    }

    private void GenerateBomb(Vector3 pos)
    {
        float j = 0f;
        for (int i=0;i<5;i++)
        {
            j += 0.2f;
            StartCoroutine(DelayGenerateBomb(j,pos));
        }  
    }
    IEnumerator DelayGenerateBomb(float i,Vector3 pos)
    {
        yield return new WaitForSeconds(i);
        Instantiate(bomb, pos , Quaternion.identity);
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
