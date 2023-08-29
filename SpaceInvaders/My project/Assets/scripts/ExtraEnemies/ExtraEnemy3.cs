using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraEnemy3 : MonoBehaviour
{
    public int direction = 0;
    private float des;
    private float speed = 3f;
    private Vector3 bombPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        des = Vector3.Distance(this.transform.position, GameObject.Find("SpawnerC").GetComponent<SpawnerC>().Pos[direction].transform.position);
        transform.position = Vector3.MoveTowards(this.transform.position, GameObject.Find("SpawnerC").GetComponent<SpawnerC>().Pos[direction].transform.position, Time.deltaTime * speed);
        //GenerateBomb
        switch (direction)
        {
            case 0:
                {
                    bombPos = GetComponent<SpawnerC>().Pos[5].transform.position;
                    break;
                }
            case 1:
                {
                    bombPos = GetComponent<SpawnerC>().Pos[6].transform.position;
                    break;
                }
            case 2:
                {
                    bombPos = GetComponent<SpawnerC>().Pos[5].transform.position;
                    break;
                }
            case 3:
                {
                    bombPos = GetComponent<SpawnerC>().Pos[4].transform.position;
                    break;
                }
        }
    }

    private void GenerateBomb(Vector3 pos)
    {
        
    }


}
