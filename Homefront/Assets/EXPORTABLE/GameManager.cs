using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private WaveCountdown waveCountdown;

    private void Start()
    {
        Debug.Log("Game Started!");
        StartGame();
    }

    public void StartGame()
    {
        waveCountdown.StartWaveCountdown();
        enemySpawner.StartFirstWave();
    }
}
