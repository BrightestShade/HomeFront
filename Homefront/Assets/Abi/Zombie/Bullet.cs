using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    private Vector2 targetDirection;
    private bool hasTarget = false;

    public float lifeTime = 3f; // Bullet will be destroyed after 3 seconds

    public void SetTarget(Vector3 targetPosition)
    {
        targetDirection = (targetPosition - transform.position).normalized;
        hasTarget = true;

        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    void Start()
    {
        // Schedule self-destruction
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        if (hasTarget)
        {
            transform.position += (Vector3)targetDirection * speed * Time.deltaTime;
        }
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