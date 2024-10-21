using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    public float moveSpeed = 3f;             
    public float directionChangeInterval = 2f; 
    private Vector2 direction;               
    private float timeSinceLastChange = 0f;
    private Animator animator;              

    public bool canMove = true;              // Control ghost can move

    void Start()
    {
        animator = GetComponent<Animator>();
        //PickRandomDirection();
    }

    void Update()
    {
        if (canMove)
        {
            transform.position += new Vector3(direction.x, direction.y, 0) * moveSpeed * Time.deltaTime;
            animator.SetFloat("MoveX", direction.x);
            animator.SetFloat("MoveY", direction.y);

            timeSinceLastChange += Time.deltaTime;
            if (timeSinceLastChange >= directionChangeInterval)
            {
                //PickRandomDirection();
                timeSinceLastChange = 0f;
            }
        }
    }

    // Walk a random direction (up, down, left, or right)
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
