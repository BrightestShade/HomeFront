using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private Image fill;

    public UnityEvent OnDied;
    private bool IsTakingDmg;
    private Animator animator;

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public float RemainingHealthPercentage => currentHealth / maxHealth;

    public void TakeDamage(float damageAmount)
    {
        if (currentHealth <= 0)
            return;

        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (animator != null)
        {
            StartCoroutine(PlayDamageAnimation());
        }

        if (currentHealth <= 0)
        {
            Debug.Log("Player is Dead");
            Destroy(gameObject);
            SceneManager.LoadScene(2); // Death scene
        }
        else
        {
            OnDied.Invoke();
        }

        UpdateHealthUI();
    }

    public void UpdateHealth(float amount)
    {
        if (currentHealth >= maxHealth) return;

        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        float targetFillAmount = currentHealth / maxHealth;
        fill.fillAmount = targetFillAmount;
        Debug.Log("Update Health Bar");
    }

    private System.Collections.IEnumerator PlayDamageAnimation()
    {
        animator.SetBool("IsTakingDmg", true);
        yield return new WaitForSeconds(0.3f); // adjust to match animation length
        animator.SetBool("IsTakingDmg", false);
    }
}