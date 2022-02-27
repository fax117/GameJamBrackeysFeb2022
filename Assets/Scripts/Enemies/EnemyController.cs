using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private float attackRange;
    [SerializeField] private GameObject player;
    private EnemyHealth enemyHealth;
    private ShootingController shootingController;
    private Animator _characterAnimator;

    private void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        _characterAnimator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        shootingController = GetComponent<ShootingController>();
    }

    private void Update()
    {
       
        if (Vector2.Distance(transform.position, player.transform.position) <= attackRange)
        {
            //Attack
            _characterAnimator.SetTrigger("Attack");
            shootingController.canShoot = true;
            shootingController.Shoot();
        }
        else
        {
            shootingController.canShoot = false;
        }

        if(enemyHealth.CurrentHP <= 0)
        {
            enemyHealth.Death();
        }
    }

}
