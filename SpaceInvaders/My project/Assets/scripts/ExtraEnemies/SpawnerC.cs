using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerC : MonoBehaviour
{
    private float enemy3Frequency = 15.0f;
    private float edgePadding = 3f;
    public GameObject Object;
    public GameObject[] Pos;
    public Vector3 generatePos;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawnEnemy3", 10, enemy3Frequency);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void spawnEnemy3()
    {
        System.Random random = new System.Random();
        int randomNumber = random.Next(1, 100);
        switch (randomNumber % 4)
        {
            case 0://EnemyPosL1
                {
                    generatePos = new Vector3(Pos[0].transform.position.x-edgePadding, Pos[0].transform.position.y, 0.0f);//y
                    GameObject monster = Instantiate(Object, generatePos, Quaternion.identity);
                    monster.GetComponent<ExtraEnemy3>().direction = 0;
                    break;
                }
            case 1://EnemyPosL2
                {
                    generatePos = new Vector3(Pos[1].transform.position.x - edgePadding, Pos[1].transform.position.y, 0.0f);//y
                    GameObject monster = Instantiate(Object, generatePos, Quaternion.identity);
                    monster.GetComponent<ExtraEnemy3>().direction = 1;
                    break;
                }
            case 2://EnemyPosR1
                {
                    generatePos = new Vector3(Pos[2].transform.position.x + edgePadding, Pos[2].transform.position.y, 0.0f);//y
                    GameObject monster = Instantiate(Object, generatePos, Quaternion.identity);
                    monster.GetComponent<ExtraEnemy3>().direction = 2;
                    break;
                 }
            case 3://EnemyPosR2
                 {
                    generatePos = new Vector3(Pos[3].transform.position.x + edgePadding, Pos[3].transform.position.y, 0.0f);//y
                    GameObject monster = Instantiate(Object, generatePos, Quaternion.identity);
                    monster.GetComponent<ExtraEnemy3>().direction = 3;
                    break;
                 }

        }

    }
   


}
