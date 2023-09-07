using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperHandler : MonoBehaviour {

    public static SniperHandler Instance { get; private set; }

    [SerializeField] private int handlerID = 0;

    [Header("Waiting to Start")]
    [SerializeField] private float WaitToStartTime = 15f;

    [Header("Spawn")]
    [SerializeField] private int maxSnipers = 4;
    [SerializeField] private int amountOfSpawnAttemptsTotal = 10;
    [SerializeField] private GameObject sniperPrefab;

    [Header("Waiting to Spawn")]
    [SerializeField] private float timeBetweenSpawns = 30f;

    [Header("Shooting")]
    [SerializeField] private int amountOfShotAttemptsTotal = 6;
    [SerializeField] private int AmountOfShots = 1;


    private float currentTimeBetweenStates = 0f;
    private int selectedSniper = -1;
    private int shotsFired = 0;
    private int amountOfShotsAttempted = 0;

    private GameObject[] SniperList;
    private SniperInvaderParent[] SniperInvaderScripts;


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
        SniperList = new GameObject[maxSnipers];
        SniperInvaderScripts = new SniperInvaderParent[maxSnipers];

        ProjectileManager.Instance.OnProjectileTick += ProjectileManager_OnProjectileTick;
    }

    private void Update() {
        if (!GameStateManager.Instance.IsGamePlaying()) { return; }

        switch (state) {
            case State.WaitingToStart:

                currentTimeBetweenStates += Time.deltaTime;
                if (currentTimeBetweenStates > WaitToStartTime) {
                    currentTimeBetweenStates = 0f;
                    state = State.Spawn;
                }

                break;
            case State.Spawn:

                SpawnSniper();
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

    private void SpawnSniper() {
        bool hasEmpty = false;
        for (int i = 0; i < maxSnipers; i++) {
            if (!SniperList[i]) { hasEmpty = true; break; }

        }
        if (!hasEmpty) { return; }

        for (int attemps = amountOfSpawnAttemptsTotal; attemps > 0; attemps++) {
            int selectRandomSniper = Random.Range(0, maxSnipers);
            if (SniperList[selectRandomSniper]) { continue; }
            SniperList[selectRandomSniper] = Instantiate(sniperPrefab, Vector3.zero, Quaternion.identity, this.transform);
            SniperInvaderScripts[selectRandomSniper] = SniperList[selectRandomSniper].GetComponent<SniperInvaderParent>();
            SniperInvaderScripts[selectRandomSniper].SetRotation((selectRandomSniper + 1) * 90 - 45);
            break;
        }
    }

    private void ProjectileManager_OnProjectileTick(object sender, System.EventArgs e) {
        if (ProjectileManager.Instance.selectedHandler != handlerID) { return; }

        if (selectedSniper == -1) {
            if (amountOfShotsAttempted > amountOfShotAttemptsTotal) { 
                amountOfShotsAttempted = 0;  
                ProjectileManager.Instance.SelectNewHandler(); 
                return; 
            }

            int selectRandomSniper = Random.Range(0, maxSnipers);

            if (!SniperInvaderScripts[selectRandomSniper]) { amountOfShotsAttempted++; return; }

            amountOfShotsAttempted = 0;
            previousState = state;
            state = State.Shoot;
            selectedSniper = selectRandomSniper;
        }


        if (!SniperList[selectedSniper] ||
            shotsFired >= AmountOfShots) {

            SniperInvaderScripts[selectedSniper].EndSniping();
            state = previousState;

            shotsFired = 0;
            selectedSniper = -1;
            ProjectileManager.Instance.SelectNewHandler();
            return;
        }

        SniperInvaderScripts[selectedSniper].SetSniping();
        shotsFired++;
    }


}
