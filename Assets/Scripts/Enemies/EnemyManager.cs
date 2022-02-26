using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Transform[] _spawners;
    public GameObject enemyPrefab;

    public void SpawnEnemies()
    {
        Instantiate(enemyPrefab, _spawners[0].transform.position, Quaternion.identity);
    }

}
