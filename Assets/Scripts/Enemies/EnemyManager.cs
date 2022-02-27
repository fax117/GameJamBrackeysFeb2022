using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Transform[] _spawners;
    public GameObject[] enemyPrefab;
    [SerializeField] private ParticleSystem _spawnEffect;

    public void SpawnEnemies()
    {
        foreach (Transform spawner in _spawners)
        {
            Instantiate(_spawnEffect, spawner.transform.position, Quaternion.identity);
            Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Length)], spawner.transform.position, Quaternion.identity);
        }
    }

}
