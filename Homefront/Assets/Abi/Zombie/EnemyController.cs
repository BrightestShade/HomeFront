using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.02f;

    [SerializeField] private Rigidbody2D rb;
    // public Animator animator;

    [SerializeField] private Transform player;
    private bool facingRight = true;

    // Damage over time variables
    private float damageInterval = 1f; // One hit per second
    private float lastDamageTime = 0f;

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
            FollowPlayer();
            //UpdateAnimation();
            FacePlayer();
    }

    void FollowPlayer()
    {
        if (player == null) return;
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    /*void UpdateAnimation()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;

        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Speed", direction.sqrMagnitude);
    }*/

    void FacePlayer()
    {
        if (player == null) return;

        if (player.position.x > transform.position.x && !facingRight)
        {
            Flip();
        }
        else if (player.position.x < transform.position.x && facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Time.time - lastDamageTime >= damageInterval)
            {
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    //playerController.TakeDamage();
                    lastDamageTime = Time.time;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            lastDamageTime = 0f;
        }
    }
}