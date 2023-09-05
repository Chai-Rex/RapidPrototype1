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
    [SerializeField] private float currentTimeWaiting = 0f;
    [SerializeField] private int monsterMaxAmount = 0;

    public int currentMonsterAmount = 0;
    static Vector3 pos11, pos12, pos13, pos14, pos21, pos22, pos23, pos24, pos31, pos32, pos33, pos34, pos41, pos42, pos43, pos44 = new Vector3();
    private Vector3[] pos1 = { pos11, pos12, pos13, pos14, pos21, pos22, pos23, pos24, pos31, pos32, pos33, pos34, pos41, pos42, pos43, pos44 };
    private Vector3[] pos2 = { pos21, pos22, pos23, pos24, pos31, pos32, pos33, pos34, pos41, pos42, pos43, pos44, pos11, pos12, pos13, pos14 };

    void Start()
    {
        for(int i = 0; i < 12; i++)
        {
            pos1[i] = Pos[i].transform.position;
            pos2[i] = Pos[i + 4].transform.position;
        }
        for (int i = 12; i < 16; i++)
        {
            pos1[i] = Pos[i].transform.position;
            pos2[i] = Pos[i - 12].transform.position;
        }
        InvokeRepeating("spawnEnemy2", currentTimeWaiting, intervalBetweenEnemy2);

    }

    public void spawnEnemy2()
    {
        if (currentMonsterAmount < monsterMaxAmount)
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
                        ExtraEnemy2 extra_enemy2 = monster.GetComponent<ExtraEnemy2>();
                        extra_enemy2.direction = 0;
                        extra_enemy2.pos1 = pos1;
                        extra_enemy2.pos2 = pos2;
                        break;
                    }
                case 1://right side
                    {
                        GameObject monster = Instantiate(Object, generatePosRight, Quaternion.identity);
                        monster.transform.parent = this.transform;
                        currentMonsterAmount += 1;
                        ExtraEnemy2 extra_enemy2 = monster.GetComponent<ExtraEnemy2>();
                        extra_enemy2.direction = 1;
                        extra_enemy2.pos1 = pos1;
                        extra_enemy2.pos2 = pos2;
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
