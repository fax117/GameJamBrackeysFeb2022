using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private float attackRange;
    [SerializeField] private GameObject player;
    private ShootingController shootingController;
    private Animator _characterAnimator;

    private void Awake()
    {
        _characterAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        shootingController = GetComponent<ShootingController>();
        
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) <= attackRange)
        {
            //Attack 
            shootingController.canShoot = true;
            shootingController.Shoot();
            _characterAnimator.SetBool("IsAttacking", true);
        }
        else
        {
            shootingController.canShoot = false;
        }
    }

}
