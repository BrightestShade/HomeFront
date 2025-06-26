using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    private Vector2 targetDirection;

    public void SetTarget(Vector3 targetPosition)
    {
        // Calculate direction to target
        targetDirection = (targetPosition - transform.position).normalized;

        // Rotate the bullet to face the target
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Update()
    {
        // Move the bullet towards the target direction
        transform.position += (Vector3)targetDirection * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
