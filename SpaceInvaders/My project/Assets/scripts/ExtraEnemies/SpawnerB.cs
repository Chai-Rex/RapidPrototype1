using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnerB : MonoBehaviour
{

    private float edgePadding = 1f;
    private float heightLimit = 2.0f;

    private Vector3 leftEdge;
    private Vector3 rightEdge;
    private Vector3 topEdge;
    //private float yBottom;
    private float yTop;

    public Vector3 generatePos;
    public GameObject Object;
    public GameObject[] Pos;

    private float enemy2Frequency = 30.0f;
    private List<float> invaderYPos = new List<float>();
    void Start()
    {
        // set world bounds
        leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
        topEdge = Camera.main.ViewportToWorldPoint(Vector3.up);
        yTop = topEdge.y;

        InvokeRepeating("spawnEnemy2", 10, enemy2Frequency);

    }

    public void spawnEnemy2()
    {
        System.Random random = new System.Random();
        int randomNumber = random.Next(1, 100);
        switch (randomNumber % 2)
        {
            case 0://left side
                {
                    generatePos = new Vector3(leftEdge.x - edgePadding, yTop- heightLimit, 0.0f);//y
                    GameObject monster = Instantiate(Object, generatePos, Quaternion.identity);
                    monster.GetComponent<ExtraEnemy2>().direction = 0;
                    break;
                }
            case 1://right side
                {
                    generatePos = new Vector3(rightEdge.x + edgePadding, yTop- heightLimit, 0.0f);//y
                    GameObject monster = Instantiate(Object, generatePos, Quaternion.identity);
                    monster.GetComponent<ExtraEnemy2>().direction = 1;
                    break;
                }

        }

    }

    // Update is called once per frame
    void Update()
    {
        invaderYPos.Clear();
        GameObject LanderHandler = GameObject.Find("LanderHandler");
        foreach (Transform child in LanderHandler.transform)
        {
            invaderYPos.Add(child.position.y);
        }
        yTop = invaderYPos.Min();
       


    }
}
