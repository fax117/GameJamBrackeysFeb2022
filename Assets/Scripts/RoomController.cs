using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [SerializeField] private ColliderTrigger colliderTrigger;
    [SerializeField] private int numWaves;
    [SerializeField] private GameObject wallEnter;
    [SerializeField] private GameObject wallExit;

    private float searchCountdown = 1f;

    private EnemyManager enemyManager;

    private State state;

    private enum State
    {
        Idle,
        InBattle,
    }

    private void Awake()
    {
        state = State.Idle;
    }

    private void Start()
    {
        enemyManager = GetComponent<EnemyManager>();
        colliderTrigger.OnPlayerEnterTrigger += ColliderTrigger_OnPlayerEnterTrigger;
        wallEnter.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!gameObject.CompareTag("FinalRoom"))
        {
            if (!EnemyIsAlive())
            {
                state = State.Idle;
                wallExit.gameObject.SetActive(false);
            }
        }
        else
        {
            if (!EnemyIsAlive())
            {
                state = State.Idle;
                //Go to Game Over Screen
            }
        }
    }

    private void ColliderTrigger_OnPlayerEnterTrigger(object sender, System.EventArgs e)
    {
        Debug.Log("Entered Trigger");

        if (state == State.Idle)
        {
            StartBattle();
            wallEnter.gameObject.SetActive(true);
            wallExit.gameObject.SetActive(true);
            colliderTrigger.OnPlayerEnterTrigger -= ColliderTrigger_OnPlayerEnterTrigger;
        }
    }

    private void StartBattle()
    {
        enemyManager.SpawnEnemies();
        state = State.InBattle;
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;

        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Gargoyle") == null && GameObject.FindGameObjectWithTag("Armor") == null)
            {
                return false;
            }
        }

        return true;
    }

    void endGame()
    {
        
    }

}
