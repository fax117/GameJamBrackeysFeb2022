using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

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
        _playerHealth = _player.GetComponent<PlayerHealth>();
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
        // If a bullet hits a player
        if (target.CompareTag("Player"))
        {
            _playerHealth.DealDamage(_damageController._enemyDamageToPlayer);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //This may change a little when enemies are added. Otherwise enemy's bullets will not hit player;
        GameObject collisionGO = collision.gameObject;

        if (!collisionGO.CompareTag("Moonlight") && !collisionGO.CompareTag("Trigger"))
        {
            IdentifyTarget(collisionGO);
            Destroy(gameObject);
        }

    }
}
