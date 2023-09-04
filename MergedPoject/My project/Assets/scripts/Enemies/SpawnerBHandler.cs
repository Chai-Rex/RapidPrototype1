using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBHandler : MonoBehaviour
{

    [SerializeField] private float waitingToStartTimer = 5f;
    [SerializeField] private GameObject SpawnerB;
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
        SpawnerB.gameObject.SetActive(false);
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
                    SpawnerB.gameObject.SetActive(true);
                }
                break;

            case State.StopSpawning: 
                state = State.WaitingToStart;
                break;
        }

    }


}
