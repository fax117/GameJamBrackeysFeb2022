using System;
using System.Collections;
using UnityEditor.UIElements;
using UnityEngine;

namespace DefaultNamespace
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        [SerializeField] private float damage = 1f;

        private GameObject _player;
        private PlayerHealth _playerHealth;

        private EnemyHealth _enemyHealth;
        private TransformationController _transformationController;
        private DamageController _damageController;

        private GameObject _gargoyle;
        private GameObject _armorKnight;


        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _playerHealth = _player.GetComponent <PlayerHealth>();
            _transformationController = _player.GetComponent<TransformationController>();
            _damageController = _player.GetComponent<DamageController>();
        }

      
        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }

        private void FixedUpdate()
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }

        private void IdentifyTarget(GameObject target)
        {
            //If A bullet hits an enemy
            if (target.CompareTag("Armor") || target.CompareTag("Gargoyle"))
            {
                _enemyHealth = target.GetComponent<EnemyHealth>();

                //If Player is in Werewolf mode
                if (_transformationController.IsWolfModeActive == true)
                {
                    // If Player is in werewolf, heals the damage depending on the type of enemy he hit
                    if(target.CompareTag("Armor")) 
                        _playerHealth.AccumulateHealth(_damageController._armorDmgWerewolf);
                    else
                        _playerHealth.AccumulateHealth(_damageController._baseDmgWerewolf);

                    _enemyHealth.DealDamage(_damageController._baseDmgWerewolf, _damageController._armorDmgWerewolf);
                }
                // If player is in normal human mode
                else
                { 
                    _enemyHealth.DealDamage(_damageController._baseDmgHuman, _damageController._armorDmgHuman);
                }
            } 

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
         
            GameObject collisionGO = collision.gameObject;

            if(!collisionGO.CompareTag("Moonlight") && !collisionGO.CompareTag("Trigger"))
            {
                IdentifyTarget(collisionGO);
                Destroy(gameObject);
            }

        }

    }
}