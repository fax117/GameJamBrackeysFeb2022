using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Transform[] _spawners;
    public GameObject enemyPrefab;
    private int gargoyles;
    private int armors;
    public int totalEnemies;

    public int CountEnemies()
    {
        gargoyles = GameObject.FindGameObjectsWithTag("Gargoyle").Length;
        armors = GameObject.FindGameObjectsWithTag("Armor").Length;

        return gargoyles + armors;
    }

    public void SpawnEnemies()
    {
        foreach (Transform spawner in _spawners)
        {
            Instantiate(enemyPrefab, spawner.transform.position, Quaternion.identity);
        }
    }

}
