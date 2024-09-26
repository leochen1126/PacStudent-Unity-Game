using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentMovement : MonoBehaviour
{
    public float moveSpeed = 5f;        // Speed of movement
    private Animator animator;          // Reference to the Animator
    private Vector2 movement;           // Store input movement

    void Start()
    {
        // Get the Animator component attached to PacStudent
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Get input from player (arrow keys or WASD)
        movement.x = Input.GetAxisRaw("Horizontal"); // Left/Right movement
        movement.y = Input.GetAxisRaw("Vertical");   // Up/Down movement

        // Normalize the movement vector so PacStudent doesn't move faster diagonally
        if (movement != Vector2.zero)
        {
            movement.Normalize();
        }

        // Set Animator parameters based on movement
        animator.SetFloat("MoveX", movement.x);
        animator.SetFloat("MoveY", movement.y);

        if (Input.GetKeyDown(KeyCode.K))
        {
            // Trigger the death animation
            animator.SetTrigger("Dead");
        }
        // Move PacStudent
        transform.position += new Vector3(movement.x, movement.y, 0) * moveSpeed * Time.deltaTime;
    }
}
