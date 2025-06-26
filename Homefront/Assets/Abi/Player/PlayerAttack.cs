using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "LZ")
        {
            Debug.Log("Hit Little Zombie!");
            collision.gameObject.GetComponent<LittleZombieHealth>().TakeDamage(1);
        }

        if (collision.gameObject.tag == "BZ")
        {
            Debug.Log("Hit Big Zombie!");
            collision.gameObject.GetComponent<BZHealth>().TakeDamage(1);
        }
    }
}
