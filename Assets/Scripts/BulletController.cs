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
            if(_transformationController.IsWolfModeActive == true)
            {
                if (target.tag == "Armor") _playerHealth.AccumulateHealth(_damageController._armorDmgWerewolf);
                else if (target.tag == "Gargoyle") _playerHealth.AccumulateHealth(_damageController._baseDmgWerewolf);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
           //this may change a little when enemies are added. Otherwise enemy's bullets will not hit player;

            if(collision.gameObject.tag != "Moonlight" && collision.gameObject.tag != "Player")
            {
                IdentifyTarget(collision.gameObject);
                Destroy(gameObject);
            }
           
        }

    }
}