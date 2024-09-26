using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostStateController : MonoBehaviour
{
    private Animator animator;   
    private GhostMovement ghostMovement;  

    void Start()
    {
        animator = GetComponent<Animator>();
        ghostMovement = GetComponent<GhostMovement>();
    }

    void Update()
    {
        // press Y into ghost scared mode, press U into ghost dead mode, press I to recover
        if (Input.GetKeyDown(KeyCode.Y))
        {
            animator.SetTrigger("Scared");
            ghostMovement.canMove = false; 
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            animator.SetTrigger("Dead");
            ghostMovement.canMove = false; 
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            animator.SetTrigger("Recover");
            ghostMovement.canMove = true;   
        }
    }
}

