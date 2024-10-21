using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Vector3[] corners;
    private int currentTarget = 0;
    private Animator animator;
    private AudioSource audioSource;

    void Start()
    {
        corners = new Vector3[]
        {
            new Vector3(-7, 4, 0),  // top-left 
            new Vector3(-1, 4, 0),  // top-right 
            new Vector3(-1, 0, 0),  // bottom-right 
            new Vector3(-7, 0, 0)   // bottom-left 
        };
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        PlayMovementAnimationAndAudio();
    }

    void Update()
    {
        MovePacStudent();
    }

    void MovePacStudent()
    {
        // Move PacStudent towards the current target corner
        //transform.position = Vector3.MoveTowards(transform.position, corners[currentTarget], moveSpeed * Time.deltaTime);

        // If PacStudent reaches the corner, move to the next one
        //if (Vector3.Distance(transform.position, corners[currentTarget]) < 0.1f)
        //{
            //currentTarget = (currentTarget + 1) % corners.Length;
        //}
    }

    void PlayMovementAnimationAndAudio()
    {
        animator.SetBool("isMoving", true);
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
    /* Saving for future use, for controlling pacstu with WASD
     * private Vector2 movement;
    movement.x = Input.GetAxisRaw("Horizontal"); 
    movement.y = Input.GetAxisRaw("Vertical");   

    diagonally
    if (movement != Vector2.zero)
    {
        movement.Normalize();
    }


    animator.SetFloat("MoveX", movement.x);
    animator.SetFloat("MoveY", movement.y);

    if (Input.GetKeyDown(KeyCode.K))
    {
    
        animator.SetTrigger("Dead");
    }

    transform.position += new Vector3(movement.x, movement.y, 0) * moveSpeed * Time.deltaTime;
     */
}