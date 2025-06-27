using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LZSpawner LZSpawner;
    [SerializeField] private GameObject playerPrefab; 
    [SerializeField] private BZSpawner BZSpawner;
    [SerializeField] private WaveCountdown waveCountdown;

    private GameObject playerInstance;


    private void Start()
    {
        Debug.Log("Game Started!");
        StartGame();
    }

    public void StartGame()
    {
        waveCountdown.StartWaveCountdown();
        LZSpawner.StartFirstWave();
        BZSpawner.StartFirstWave();
    }
}
