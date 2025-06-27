using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor.ShaderGraph.Internal;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;

    [Header("Health Bar")]
    [SerializeField] private Image healthBarFill; 

    private void Start()
    {
        UpdateHealthUI();
        
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
        UpdateHealthUI(); 

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
            SceneManager.LoadScene(2);
            Time.timeScale = 1;
            Debug.Log("Player is dead!");
        }
    }

    public void AddHealth(float amountToAdd)
    {
        UpdateHealthUI(); 

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

    public void UpdateHealth(float amount)
    {
        currentHealth += amount;
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        float targetFillAmount = currentHealth / maxHealth;
        healthBarFill.fillAmount = targetFillAmount;
    }
}