using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPotionSpawner : MonoBehaviour
{
    [SerializeField] private GameObject potionPrefab;

    [SerializeField] private float minimumSpawnTime;
    [SerializeField] private float maximumSpawnTime;
    [SerializeField] private float timeUntilSpawn;

    [SerializeField] private Transform playerTransform; // Assign in inspector or find in code
    [SerializeField] private float spawnRadius = 5f;    // How far from player potions can spawn

    private void Awake()
    {
        SetTimeUntilSpawn();
    }

    void Update()
    {
        timeUntilSpawn -= Time.deltaTime;

        if (timeUntilSpawn <= 0)
        {
            Vector3 spawnPosition = GetRandomPositionAroundPlayer();
            Instantiate(potionPrefab, spawnPosition, Quaternion.identity);
            SetTimeUntilSpawn();
        }
    }

    private void SetTimeUntilSpawn()
    {
        timeUntilSpawn = Random.Range(minimumSpawnTime, maximumSpawnTime);
    }

    private Vector3 GetRandomPositionAroundPlayer()
    {
        Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPosition = playerTransform.position + new Vector3(randomCircle.x, 0, randomCircle.y);
        return spawnPosition;
    }
}
