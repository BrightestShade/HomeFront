using UnityEngine;

public class TrippleShotSpawner : MonoBehaviour
{
    public GameObject tripleShotPrefab;   // Assign the prefab in the Inspector
    public Transform playerTransform;     // Assign the player in Inspector
    public float spawnInterval = 15f;
    public float spawnRadius = 6f;

    private void Start()
    {
        if (playerTransform == null)
        {
            Debug.LogWarning("Player Transform not assigned in the Inspector.");
        }

        InvokeRepeating(nameof(SpawnTripleShot), 5f, spawnInterval);
    }

    void SpawnTripleShot()
    {
        if (tripleShotPrefab == null || playerTransform == null)
        {
            Debug.LogWarning("Cannot spawn Triple Shot. Missing prefab or player reference.");
            return;
        }

        Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPosition = playerTransform.position + new Vector3(randomOffset.x, randomOffset.y, 0f);

        Instantiate(tripleShotPrefab, spawnPosition, Quaternion.identity);
    }
}
