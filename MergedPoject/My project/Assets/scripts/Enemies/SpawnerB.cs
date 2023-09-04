using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnerB : MonoBehaviour
{


    public Vector3 generatePosLeft = new Vector3(-14.92601f, 0.0f, 0.0f);
    public Vector3 generatePosRight = new Vector3(14.92601f, 0.0f, 0.0f);
    public GameObject Object;
    public GameObject[] Pos;

    [SerializeField] private float intervalBetweenEnemy2 = 30.0f;
    void Start()
    {
        InvokeRepeating("spawnEnemy2", 0f, intervalBetweenEnemy2);

    }

    public void spawnEnemy2()
    {
        System.Random random = new System.Random();
        int randomNumber = random.Next(1, 100);
        switch (randomNumber % 2)
        {
            case 0://left side
                {
                    GameObject monster = Instantiate(Object, generatePosLeft, Quaternion.identity);
                    monster.GetComponent<ExtraEnemy2>().direction = 0;
                    break;
                }
            case 1://right side
                {
                    GameObject monster = Instantiate(Object, generatePosRight, Quaternion.identity);
                    monster.GetComponent<ExtraEnemy2>().direction = 1;
                    break;
                }

        }

    }

    // Update is called once per frame
    void Update()
    {


    }
}
