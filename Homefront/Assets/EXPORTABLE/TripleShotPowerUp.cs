using System;
using UnityEngine;

public class TripleShotPowerUp : MonoBehaviour, IItem
{
    public static event Action OnTripleShotPickup;

    private void Start()
    {
        Destroy(gameObject, 5f); // Auto destroy if not picked up
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnTripleShotPickup?.Invoke();
            Destroy(gameObject);
        }
    }
}

