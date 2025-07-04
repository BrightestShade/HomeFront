using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.02f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform player;
    [SerializeField] private Animator animator;

    private PlayerHealth playerHealth;

    private bool playerInRange = false;
    private GameObject playerObj;

    private Coroutine attackCoroutine;

    void Start()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
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

    private IEnumerator RepeatedAttack()
    {
        while (playerInRange)
        {
            animator.SetBool("IsAttacking", true);

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1f); // Adjust damage amount as needed
                Debug.Log("Enemy attacked the player!");
            }
            else
            {
                Debug.LogWarning("PlayerHealth not found on the player object.");
            }

            yield return new WaitForSeconds(0.2f); // Animation duration
            animator.SetBool("IsAttacking", false);

            yield return new WaitForSeconds(0.5f); // Remaining cooldown time (0.5s total)
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
            playerObj = collision.gameObject;
            playerHealth = playerObj.GetComponent<PlayerHealth>();

            if (attackCoroutine == null)
            {
                attackCoroutine = StartCoroutine(RepeatedAttack());
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
            playerObj = null;
            playerHealth = null;
            animator.SetBool("IsAttacking", false);

            if (attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
                attackCoroutine = null;
            }
        }
    }
}