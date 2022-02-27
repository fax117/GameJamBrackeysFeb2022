using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [SerializeField] private ColliderTrigger colliderTrigger;
    [SerializeField] private EnemyManager enemyManager;

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
        colliderTrigger.OnPlayerEnterTrigger += ColliderTrigger_OnPlayerEnterTrigger;
        enemyManager = GetComponent<EnemyManager>();
    }

    private void ColliderTrigger_OnPlayerEnterTrigger(object sender, System.EventArgs e)
    {
        Debug.Log("Event Started");
        if(state == State.Idle)
        {
            StartBattle();
            colliderTrigger.OnPlayerEnterTrigger -= ColliderTrigger_OnPlayerEnterTrigger;
        }
            
    }

    private void StartBattle()
    {
        Debug.Log("Spawningn enemies");
        enemyManager.SpawnEnemies();
        state = State.InBattle;
    }
}
