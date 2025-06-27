using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class LittleZombieAttack : MonoBehaviour
{
    [SerializeField] private float damageAmount;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerHealth>())
        {
            var PlayerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            PlayerHealth.TakeDamage(damageAmount);

            Debug.Log("Hit Player");
        }
    }
}