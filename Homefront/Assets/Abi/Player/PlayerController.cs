using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public Joystick movementJoystick;
    private Rigidbody2D rb;
    private Vector2 movementInput;
    private bool facingRight = true;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float fireRate = 0.5f;
    private float nextFireTime = 0f;
    public int maxAmmo = 6;
    public float reloadTime = 2f;
    private int currentAmmo;
    private bool isReloading = false;

    [Header("UI")]
    public Button fireButton;
    public Image ammoImage;
    public Sprite fullBulletSprite;
    public Sprite emptyBulletSprite;
    public Sprite spriteFor1Bullet;
    public Sprite spriteFor2Bullets;
    public Sprite spriteFor3Bullets;
    public Sprite spriteFor4Bullets;

    private Animator animator;
    //private Animator[] healthAnimators;
    private Dictionary<int, Sprite> ammoSprites;
    //private Dictionary<int, Sprite> healthSprites;

    public SpriteRenderer spriteRenderer;          // Assign in Inspector
    public BoxCollider2D attackCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentAmmo = maxAmmo;
        //currentHealth = maxHealth;

        ammoSprites = new Dictionary<int, Sprite>
        {
            {6, fullBulletSprite},
            {5, fullBulletSprite},
            {4, spriteFor4Bullets},
            {3, spriteFor3Bullets},
            {2, spriteFor2Bullets},
            {1, spriteFor1Bullet},
            {0, emptyBulletSprite}
        };

        /*healthSprites = new Dictionary<int, Sprite>
        {
            {4, fullHeartSprite},
            {3, threeHeartSprite},
            {2, twoHeartSprite},
            {1, oneHeartSprite},
            {0, emptyHeartSprite}
        };

        healthAnimators = new Animator[healthImages.Length];
        for (int i = 0; i < healthImages.Length; i++)
        {
            healthAnimators[i] = healthImages[i].GetComponent<Animator>();
        }*/

        UpdateAmmoUI();

        if (fireButton != null)
            fireButton.onClick.AddListener(OnFireButtonPressed);

        //HealthPickUp.OnHealthPickUp += Heal;
    }

    void Update()
    {
        if (movementJoystick != null)
            movementInput = new Vector2(movementJoystick.Horizontal, movementJoystick.Vertical);
        else
            movementInput = Vector2.zero;

        if (movementInput.sqrMagnitude > 0.01f)
        {
            RotateToDirection(movementInput);
        }

        UpdateAnimation();
    }
    void FixedUpdate()
    {
        rb.velocity = movementInput * moveSpeed;
    }

    public void OnFireButtonPressed()
    {
        if (!isReloading && Time.time >= nextFireTime)
        {
            if (currentAmmo > 0)
            {
                Shoot();
                currentAmmo--;
                UpdateAmmoUI();
                nextFireTime = Time.time + fireRate;

                if (currentAmmo <= 0)
                    StartCoroutine(Reload());
            }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);

        GameObject nearestEnemy = FindNearestEnemy();
        if (nearestEnemy != null)
        {
            bullet.GetComponent<Bullet>().SetTarget(nearestEnemy.transform.position);
        }
        else
        {
            // Default direction: based on player's facing rotation
            Vector3 direction = transform.right; // Player's local "forward" direction
            Vector3 defaultTarget = bulletSpawnPoint.position + direction;
            bullet.GetComponent<Bullet>().SetTarget(defaultTarget);
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
        UpdateAmmoUI();
    }

    void UpdateAmmoUI()
    {
        if (ammoImage != null && ammoSprites.ContainsKey(currentAmmo))
        {
            ammoImage.sprite = ammoSprites[currentAmmo];
        }
    }

    void RotateToDirection(Vector2 direction)
    {
        if (direction.sqrMagnitude < 0.01f) return;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float offset = -90f;  // change this based on your sprite's facing direction

        transform.rotation = Quaternion.Euler(0, 0, angle + offset);
    }

    void UpdateAnimation()
    {
        bool isMoving = movementInput.sqrMagnitude > 0.01f; // if joystick is moving
        animator.SetBool("IsWalking", isMoving);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            //TakeDamage();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Wall"))
        {
            //TakeDamage();
        }
    }

    GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(currentPosition, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }
}
