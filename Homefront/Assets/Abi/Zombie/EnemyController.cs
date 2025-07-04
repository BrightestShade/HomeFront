using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.02f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform player;
    [SerializeField] private Animator animator;

    private PlayerHealth playerHealth;
    private GameObject playerObj;
    private bool playerInRange = false;

    private bool isOnCooldown = false;
    [SerializeField] private float attackCooldown = 0.5f;
    private float lastAttackTime = -999f;

    void Start()
    {
        if (player == null)
        {
            GameObject foundPlayerObj = GameObject.FindGameObjectWithTag("Player");
            if (foundPlayerObj != null)
            {
                player = foundPlayerObj.transform;
            }
            else
            {
                Debug.LogWarning("Player not found in the scene. Make sure the player GameObject has the 'Player' tag.");
            }
        }
    }

    void Update()
    {
        if (!playerInRange)
        {
            FollowPlayer();
        }

        RotateTowardsPlayer();

        if (playerInRange && Time.time >= lastAttackTime + attackCooldown)
        {
            AttackPlayer();
        }
    }

    void FollowPlayer()
    {
        if (player == null) return;
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    void RotateTowardsPlayer()
    {
        if (player == null) return;

        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    private void AttackPlayer()
    {
        if (playerHealth != null)
        {
            animator.SetTrigger("Attack");
            playerHealth.TakeDamage(1f);
            Debug.Log("Enemy attacked the player!");
            lastAttackTime = Time.time;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
            playerObj = collision.gameObject;
            playerHealth = playerObj.GetComponent<PlayerHealth>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
            playerObj = null;
            playerHealth = null;
        }
    }
}