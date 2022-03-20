using System.Collections;
using UnityEngine;

// <summary>
/// This class manages the spawning on enemies
/// </summary>
public class EnemyManager : MonoBehaviour
{
    [SerializeField] private Transform enemySpawn;
    [SerializeField] private int enemySpawnRate = 2;
    private bool _canSpawn = true;


    private void Start()
    {
        GameManager.PInstance.onGameStart += GameStart;
    }


    private void Update()
    {
        if (_canSpawn)
        {
            StartCoroutine(SpawnEnemy());
        }
    }

    private void GameStart()
    {
        EnemyPooling.EnemyPoolInstance.DisableAllEnemies();
        MissilePooling.MissilePoolInstance.DisableAllMissiles();
    }

    // <summary>
    /// Checks if an enemy can be spawned and spawns them
    /// </summary>
    private IEnumerator SpawnEnemy()
    {
        _canSpawn = false;
        var enemyGo = EnemyPooling.EnemyPoolInstance.GetPooledObject();
        if (enemyGo != null)
        {
            var position = enemySpawn.transform.position;
            enemyGo.transform.position = new Vector3(position.x,
                position.y + Random.Range(-Camera.main.orthographicSize, Camera.main.orthographicSize));
            enemyGo.transform.rotation = Quaternion.identity;
            enemyGo.SetActive(true);
        }

        yield return new WaitForSeconds(1f / enemySpawnRate);
        _canSpawn = true;
    }

    private void OnDestroy()
    {
        GameManager.PInstance.onGameStart -= GameStart;
    }
}