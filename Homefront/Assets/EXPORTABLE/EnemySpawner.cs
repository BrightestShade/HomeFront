using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval = 2f;

    // Spawn area
    [SerializeField] private float minX = -10f;
    [SerializeField] private float maxX = 10f;
    [SerializeField] private float minY = -5f;
    [SerializeField] private float maxY = 5f;

    [SerializeField] private Transform player; // Reference to the player
    [SerializeField] private float minSpawnDistance = 3f; // Minimum distance from player

    private int currentWave = 0;
    private int minEnemies = 1;
    private int maxEnemies = 7;

    private void Start()
    {
        WaveCountdown.OnWaveStart += StartNewWave;
    }

    private void OnDestroy()
    {
        WaveCountdown.OnWaveStart -= StartNewWave;
    }

    public void StartFirstWave()
    {
        Debug.Log("Starting first wave...");
        currentWave = 1;
        StartCoroutine(SpawnWave(2));
    }

    private void StartNewWave()
    {
        currentWave++;

        int enemyCount;
        if (currentWave <= 2)
        {
            enemyCount = 2;
        }
        else
        {
            enemyCount = Random.Range(minEnemies, maxEnemies + currentWave);
        }

        Debug.Log($"Starting Wave {currentWave}, Spawning {enemyCount} enemies...");
        StartCoroutine(SpawnWave(enemyCount));
    }

    private IEnumerator SpawnWave(int enemyCount)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 spawnPosition;

            // Ensure enemies don't spawn too close to the player
            do
            {
                float spawnX = Random.Range(minX, maxX);
                float spawnY = Random.Range(minY, maxY);
                spawnPosition = new Vector3(spawnX, spawnY, 0);
            }
            while (Vector3.Distance(spawnPosition, player.position) < minSpawnDistance);

            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
