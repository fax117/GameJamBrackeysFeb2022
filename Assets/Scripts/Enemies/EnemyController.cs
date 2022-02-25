using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private float attackRange;
    [SerializeField] private GameObject player;

    private void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        print(Vector2.Distance(transform.position, player.transform.position));

        if (Vector2.Distance(transform.position, player.transform.position) <= attackRange && Vector2.Distance(transform.position, player.transform.position) > attackRange*-1)
        {
            //Attack 
            print("I'm Shooting");
        }
    }

}
