using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// <summary>
/// This class is used to pool all the missiles
/// </summary>
public class MissilePooling : MonoBehaviour
{
    public static MissilePooling MissilePoolInstance;

    public List<GameObject> pooledMissiles;
    public List<GameObject> pooledEnemyMissiles;

    public GameObject missileObjectToPool;
    public GameObject enemyMissileObjectToPool;

    public int poolAmount;

    private void Awake()
    {
        MissilePoolInstance = this;
    }

    private void Start()
    {
        pooledMissiles = new List<GameObject>();
        for (var i = 0; i < poolAmount; i++)
        {
            var temp1 = Instantiate(missileObjectToPool);
            var temp2 = Instantiate(enemyMissileObjectToPool);
            temp1.SetActive(false);
            temp2.SetActive(false);
            pooledMissiles.Add(temp1);
            pooledEnemyMissiles.Add(temp2);
        }
    }

    public GameObject GetPooledPlayerMissile()
    {
        for (var i = 0; i < poolAmount; i++)
        {
            if (!pooledMissiles[i].activeInHierarchy) return pooledMissiles[i];
        }

        return null;
    }

    public GameObject GetPooledEnemyMissile()
    {
        for (var i = 0; i < poolAmount; i++)
        {
            if (!pooledEnemyMissiles[i].activeInHierarchy) return pooledEnemyMissiles[i];
        }

        return null;
    }

    // <summary>
    /// Disables all the missiles
    /// </summary>
    public void DisableAllMissiles()
    {
        foreach (var enemy in pooledMissiles.Where(missile => missile.activeInHierarchy))
            enemy.SetActive(false);
        foreach (var enemy in pooledEnemyMissiles.Where(missile => missile.activeInHierarchy))
            enemy.SetActive(false);
    }
}