using UnityEngine;

public class ZombieAttackLittleZombie : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public int damage = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Hit player!");
            playerHealth.TakeDamage(damage);
        }
    }
}
