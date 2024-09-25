using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    public float moveSpeed = 3f;             // Speed of ghost movement
    public float directionChangeInterval = 2f; // How often the ghost changes direction
    private Vector2 direction;               // Current movement direction
    private float timeSinceLastChange = 0f;
    private Animator animator;               // Reference to the Animator

    public bool canMove = true;              // Control whether the ghost can move

    void Start()
    {
        // Get the Animator component attached to the ghost
        animator = GetComponent<Animator>();

        // Pick an initial random direction
        PickRandomDirection();
    }

    void Update()
    {
        if (canMove)
        {
            // Move the ghost in the current direction
            transform.position += new Vector3(direction.x, direction.y, 0) * moveSpeed * Time.deltaTime;

            // Update the Animator parameters for direction
            animator.SetFloat("MoveX", direction.x);
            animator.SetFloat("MoveY", direction.y);

            // Change direction after a set interval
            timeSinceLastChange += Time.deltaTime;
            if (timeSinceLastChange >= directionChangeInterval)
            {
                PickRandomDirection();
                timeSinceLastChange = 0f;
            }
        }
    }

    // Pick a random direction (up, down, left, or right)
    void PickRandomDirection()
    {
        int randomDir = Random.Range(0, 4);
        switch (randomDir)
        {
            case 0:
                direction = Vector2.up;
                break;
            case 1:
                direction = Vector2.down;
                break;
            case 2:
                direction = Vector2.left;
                break;
            case 3:
                direction = Vector2.right;
                break;
        }
    }
}
