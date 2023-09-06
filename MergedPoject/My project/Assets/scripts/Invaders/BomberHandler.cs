using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberHandler : MonoBehaviour {

    public static BomberHandler Instance { get;  private set; }

    [SerializeField] private int handlerID = 0;

    [Header("Waiting to Start")]
    [SerializeField] private float WaitToStartTime = 15f;

    [Header("Spawn")]
    [SerializeField] private int maxBombers = 3;
    [SerializeField] private float offsetHeightBy = 1f;
    [SerializeField] private int AmountOfBombs = 20;
    [SerializeField] private GameObject bomberPrefab;

    [Header("Waiting to Spawn")]
    [SerializeField] private float timeBetweenSpawns = 30f;


    private float currentTimeBetweenStates = 0f;
    private int selectedBomber = -1;
    private int bombsDeployed = 0;

    private GameObject[] BomberList;
    private BomberInvaderParent[] BomberInvaderScripts;


    private enum State {
        WaitingToStart,
        Spawn,
        WaitingToSpawn,
        Shoot
    }
    private State state;
    private State previousState;

    private void Start() {
        Instance = this;
        state = State.WaitingToStart;
        BomberList = new GameObject[maxBombers];
        BomberInvaderScripts = new BomberInvaderParent[maxBombers];

        ProjectileManager.Instance.OnProjectileTick += ProjectileManager_OnProjectileTick;
    }

    private void Update() {

        switch (state) {
            case State.WaitingToStart:

                currentTimeBetweenStates += Time.deltaTime;
                if (currentTimeBetweenStates > WaitToStartTime) {
                    currentTimeBetweenStates = 0f;
                    state = State.Spawn;
                }

                break;
            case State.Spawn:

                SpawnBomber();
                state = State.WaitingToSpawn;

                break;
            case State.WaitingToSpawn:

                currentTimeBetweenStates += Time.deltaTime;
                if (currentTimeBetweenStates > timeBetweenSpawns) {
                    currentTimeBetweenStates = 0f;
                    state = State.Spawn;
                }

                break;
            case State.Shoot:

                break;
        }

    }

    private void SpawnBomber() {
        for (int i = 0; i < maxBombers; i++) {
            if (BomberList[i]) { continue; }
            BomberList[i] = Instantiate(bomberPrefab, Vector3.zero, Quaternion.identity, this.transform);
            BomberInvaderScripts[i] = BomberList[i].GetComponent<BomberInvaderParent>();
            BomberInvaderScripts[i].OffsetTargetHeight((float)i * offsetHeightBy);
            break;
        }
    }

    private void ProjectileManager_OnProjectileTick(object sender, System.EventArgs e) {
        if (ProjectileManager.Instance.selectedHandler != handlerID) { return; }

        if (selectedBomber == -1) {
            for (int i = maxBombers - 1; i >= 0; i--) {
                if (!BomberInvaderScripts[i]) { continue; }
                previousState = state;
                state = State.Shoot;
                selectedBomber = i; 
                break;
            }
            if (selectedBomber == -1) {
                ProjectileManager.Instance.SelectNewHandler();
                return;
            }
        }



        if (!BomberList[selectedBomber] || 
            bombsDeployed >= AmountOfBombs) {

            BomberInvaderScripts[selectedBomber].EndBombing();
            state = previousState;

            bombsDeployed = 0;
            selectedBomber = -1;
            ProjectileManager.Instance.SelectNewHandler();
            return;
        }

        BomberInvaderScripts[selectedBomber].SetBombing();
        bombsDeployed++;
    }


}
