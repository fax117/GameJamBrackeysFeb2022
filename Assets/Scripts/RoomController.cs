using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [SerializeField] private ColliderTrigger colliderTrigger;
    [SerializeField] private int numWaves;
    [SerializeField] private GameObject wallEnter;
    [SerializeField] private GameObject wallExit;
    [SerializeField] private BoxCollider2D triggerEnd;

    [SerializeField] private GameObject youWinUI;

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
        //youWinUI = GameObject.FindGameObjectWithTag("YouWinMenu");
        colliderTrigger.OnPlayerEnterTrigger += ColliderTrigger_OnPlayerEnterTrigger;
        wallEnter.gameObject.SetActive(false);
    }

    private void Update()
    { 
        if (!EnemyIsAlive())
        {
            state = State.Idle;
            wallExit.gameObject.SetActive(false);
            triggerEnd.gameObject.SetActive(true);
        }  
    }

    private void ColliderTrigger_OnPlayerEnterTrigger(object sender, System.EventArgs e)
    {
        if (state == State.Idle)
        {
            StartBattle();
            triggerEnd.gameObject.SetActive(false);
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

    public void EndGame()
    {
        youWinUI.SetActive(true);
    }

}
