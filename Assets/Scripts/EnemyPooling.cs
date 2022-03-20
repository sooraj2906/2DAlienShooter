using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// <summary>
/// This class is used to pool all the enemies
/// </summary>
public class EnemyPooling : MonoBehaviour
{
    public static EnemyPooling EnemyPoolInstance;

    public List<GameObject> pooledEnemies;

    [SerializeField] private GameObject gameObjectToPool;

    [SerializeField] private int poolAmount;

    private void Awake()
    {
        EnemyPoolInstance = this;
    }

    private void Start()
    {
        pooledEnemies = new List<GameObject>();
        for (var i = 0; i < poolAmount; i++)
        {
            var temp = Instantiate(gameObjectToPool);
            temp.SetActive(false);
            pooledEnemies.Add(temp);
        }
    }

    public GameObject GetPooledObject()
    {
        for (var i = 0; i < poolAmount; i++)
        {
            if (!pooledEnemies[i].activeInHierarchy) return pooledEnemies[i];
        }

        return null;
    }

    // <summary>
    /// Disables all the enemies
    /// </summary>
    public void DisableAllEnemies()
    {
        foreach (var enemy in pooledEnemies.Where(enemy => enemy.activeInHierarchy))
            enemy.SetActive(false);
    }
}