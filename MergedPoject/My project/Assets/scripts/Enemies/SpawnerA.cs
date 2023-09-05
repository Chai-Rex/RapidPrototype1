using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class SpawnerA : MonoBehaviour
{

    private Vector3 generatePosLeft= new Vector3(-14.92601f, 0.0f, 0.0f);
    private Vector3 generatePosRight = new Vector3(14.92601f, 0.0f, 0.0f);
    public GameObject Object;
    [SerializeField] private float currentTimeWaiting = 0f;
    [SerializeField] private float intervalBetweenEnemy1 = 10.0f;
    [SerializeField] private int monsterMaxAmount = 0;
    public int currentMonsterAmount = 0;

    void Start()
    {
        InvokeRepeating("spawnEnemy1", currentTimeWaiting, intervalBetweenEnemy1);

    }

    public void spawnEnemy1()
    {
        if (currentMonsterAmount<monsterMaxAmount)
        {
            System.Random random = new System.Random();
            int randomNumber = random.Next(1, 3);
            switch (randomNumber % 2)
            {
                case 0://left side
                    {
                        GameObject monster = Instantiate(Object, generatePosLeft, Quaternion.identity);
                        monster.transform.parent = this.transform;
                        currentMonsterAmount += 1;
                        monster.GetComponent<ExtraEnemy1>().direction = 0;
                        break;
                    }
                case 1://right side
                    {
                        GameObject monster = Instantiate(Object, generatePosRight, Quaternion.identity);
                        monster.transform.parent = this.transform;
                        currentMonsterAmount += 1;
                        monster.GetComponent<ExtraEnemy1>().direction = 1;
                        break;
                    }

            }
        }
        

    }

    // Update is called once per frame
    void Update()
    {


    }
}
