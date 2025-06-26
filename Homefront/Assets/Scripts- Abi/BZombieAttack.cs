using UnityEngine;

public class BZombieAttack : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public int damage = 2;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Hit player!");
            playerHealth.TakeDamage(damage);
        }
    }
}
