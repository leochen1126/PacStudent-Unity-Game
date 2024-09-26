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
            new Vector3(-7, 3, 0),  // top-left corner
            new Vector3(-1, 3, 0),  // top-right corner
            new Vector3(-1, 0, 0),  // bottom-right corner
            new Vector3(-7, 0, 0)   // bottom-left corner
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
        transform.position = Vector3.MoveTowards(transform.position, corners[currentTarget], moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, corners[currentTarget]) < 0.1f)
        {
            currentTarget = (currentTarget + 1) % corners.Length;
        }
    }

    void PlayMovementAnimationAndAudio()
    {
        animator.SetBool("isMoving", true);
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
