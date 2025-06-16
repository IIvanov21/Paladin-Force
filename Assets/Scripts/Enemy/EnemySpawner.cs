using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyStats stats;
    public Transform spawnPoint;
    public LevelSettings levelSettings;

    void Start()
    {
        InvokeRepeating("SpawnEnemy",levelSettings.initialDelay ,levelSettings.enemySpawnRate);
    }

    void SpawnEnemy()
    {
        Instantiate(stats.enemyObject, spawnPoint.position, Quaternion.identity);
    }
}
