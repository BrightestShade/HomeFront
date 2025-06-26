using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Enemy")
        {
            Debug.Log("Hit Enemy!");
            collision.collider.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
        }
    }
}
