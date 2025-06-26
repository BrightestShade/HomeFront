using System.Collections;
using UnityEngine;
using TMPro;

public class WaveCountdown : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float waveCooldown = 20f;
    private float remainingTime;
    private bool countdownActive = false;

    public delegate void WaveStartHandler();
    public static event WaveStartHandler OnWaveStart;

    private void Start()
    {
        remainingTime = waveCooldown;
    }

    private void Update()
    {
        if (countdownActive)
        {
            remainingTime -= Time.deltaTime;

            if (remainingTime <= 0)
            {
                Debug.Log("Wave Timer Ended. Starting New Wave...");
                remainingTime = waveCooldown;
                OnWaveStart?.Invoke();
            }

            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void StartWaveCountdown()
    {
        Debug.Log("Starting Wave Countdown...");
        countdownActive = true;
    }

    public void StopWaveCountdown()
    {
        Debug.Log("Stopping Wave Countdown...");
        countdownActive = false;
    }
}
