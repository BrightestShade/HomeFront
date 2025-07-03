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
    private Animator[] healthAnimators;

    public SpriteRenderer spriteRenderer;          // Assign in Inspector
    public BoxCollider2D attackCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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

    
}
