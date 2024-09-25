using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostStateController : MonoBehaviour
{
    private Animator animator;   // Reference to the Animator
    private GhostMovement ghostMovement;  // Reference to the GhostMovement script

    void Start()
    {
        // Get the Animator component attached to the ghost
        animator = GetComponent<Animator>();

        // Get the GhostMovement script attached to the same GameObject
        ghostMovement = GetComponent<GhostMovement>();
    }

    void Update()
    {
        // Check if the 'Y' key is pressed to trigger the Scared state and stop movement
        if (Input.GetKeyDown(KeyCode.Y))
        {
            animator.SetTrigger("Scared");
            ghostMovement.canMove = false;  // Stop the ghost from moving
        }

        // Check if the 'U' key is pressed to trigger the Dead state and stop movement
        if (Input.GetKeyDown(KeyCode.U))
        {
            animator.SetTrigger("Dead");
            ghostMovement.canMove = false;  // Stop the ghost from moving
        }

        // Check if the 'I' key is pressed to resume movement
        if (Input.GetKeyDown(KeyCode.I))
        {
            animator.SetTrigger("Recover");
            ghostMovement.canMove = true;   // Allow the ghost to move again
        }
    }
}

