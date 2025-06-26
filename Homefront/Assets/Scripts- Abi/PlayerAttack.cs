using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    void Update()
    {
        if (Input.touchCount > 1)
        {
            Touch touch = Input.GetTouch(1);

            if (gameObject.tag == "Enemy")
            {
                gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
            }
        }
    }
}
