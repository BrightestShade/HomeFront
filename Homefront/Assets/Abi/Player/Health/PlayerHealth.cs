using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events; 

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;

    public float RemainingHealthPercentage
    {
        get
        {
            return currentHealth / maxHealth;
        }
    }
    public UnityEvent OnDied; 

    public void TakeDamage(float damageAmount)
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }

        currentHealth -= damageAmount;

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        if(currentHealth > 0)
        {
            OnDied.Invoke();
        }
        
        if(currentHealth == 0)
        {
            Destroy(gameObject);
        }
    }

    public void AddHealth(float amountToAdd)
    {
        if (currentHealth == maxHealth)
        {
            return;
        }

        currentHealth += amountToAdd;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth; 
        }
    }
}