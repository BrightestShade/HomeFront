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

    private Animator animator;
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D attackCollider;
    public CurrencyManager cm;

    [Header("Shop UI")]
    public GameObject shopCanvas;
    private bool isShopOpen = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (shopCanvas != null)
            shopCanvas.SetActive(false); // Ensure it's off at start
    }

    void Update()
    {
        if (!isShopOpen)
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
        else
        {
            // Disable movement while in shop
            movementInput = Vector2.zero;
        }

        // Close shop with Escape key
        if (isShopOpen && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            CloseShop();
        }
    }

    void FixedUpdate()
    {
        if (!isShopOpen)
            rb.velocity = movementInput * moveSpeed;
        else
            rb.velocity = Vector2.zero;
    }

    void RotateToDirection(Vector2 direction)
    {
        if (direction.sqrMagnitude < 0.01f) return;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float offset = -90f;  // Adjust if needed based on sprite orientation
        transform.rotation = Quaternion.Euler(0, 0, angle + offset);
    }

    void UpdateAnimation()
    {
        bool isMoving = movementInput.sqrMagnitude > 0.01f;
        animator.SetBool("IsWalking", isMoving);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shop"))
        {
            OpenShop();
        }
    }

    void OpenShop()
    {
        if (shopCanvas != null)
        {
            shopCanvas.SetActive(true);
            Time.timeScale = 0f;
            isShopOpen = true;
        }
    }

    void CloseShop()
    {
        if (shopCanvas != null)
        {
            shopCanvas.SetActive(false);
            Time.timeScale = 1f;
            isShopOpen = false;
        }
    }
}
