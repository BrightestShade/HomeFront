using Unity.VisualScripting;
using UnityEngine;

public class LittleZombieHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;

    public CurrencyManager cm;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;

        if (cm == null)
        {
            cm = FindObjectOfType<CurrencyManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
            Debug.Log("Little Zombie is Dead!");
        }
    }

    void Die()
    {
        cm.currencyCount+= 10;
        Debug.Log("Zombie died! Currency count increased.");
        Destroy(gameObject);
    }
}
