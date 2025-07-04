using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events; 

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private Image fill;

    private void Start()
    {
        currentHealth = maxHealth;
    }

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
            Debug.Log("Player is Dead"); 
        }

        UpdateHealthUI();
    }

    public void UpdateHealth(float amount)
    {
        if (currentHealth == maxHealth)
        {
            return;
        }

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth; 
        }

        currentHealth += amount;
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        float targetFillAmount = currentHealth / maxHealth; 
        fill.fillAmount = targetFillAmount;
        Debug.Log("Update Health Bar"); 
    }
}