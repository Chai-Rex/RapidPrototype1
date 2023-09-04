using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerAHandler : MonoBehaviour
{

    [SerializeField] private float waitingToStartTimer = 5f;
    [SerializeField] private GameObject SpawnerA;
    private float currentTimeWaiting = 0f;

    private enum State
    {
        WaitingToStart,
        StopSpawning
    }
    private State state;


    private void Start()
    {
        state = State.WaitingToStart;
        SpawnerA.gameObject.SetActive(false);
    }

    private void Update()
    {



        switch (state)
        {
            case State.WaitingToStart:
                currentTimeWaiting += Time.deltaTime;
                if (currentTimeWaiting > waitingToStartTimer)
                {
                    currentTimeWaiting = 0f;
                    SpawnerA.gameObject.SetActive(true);
                }
                break;

            case State.StopSpawning: 
                state = State.WaitingToStart;
                break;
        }

    }


}
