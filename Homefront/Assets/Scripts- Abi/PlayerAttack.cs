using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "LZ")
        {
            Debug.Log("Hit Little Zombie!");
            collision.collider.gameObject.GetComponent<LittleZombieHealth>().TakeDamage(1);
        }

        if (collision.collider.gameObject.tag == "BZ")
        {
            Debug.Log("Hit Big Zombie!");
            collision.collider.gameObject.GetComponent<BZHealth>().TakeDamage(1);
        }
    }
}
