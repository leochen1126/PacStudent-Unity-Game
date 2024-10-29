using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Vector2Int gridPosition;
    private Vector2 targetPosition;
    private bool isMoving = false;
    private Vector2 lastInput;
    private Vector2 currentInput;
    private AudioSource walkAudio;
    void Start()
    {
        gridPosition = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        targetPosition = transform.position;
        walkAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        HandleInput();
        MovePacStudent();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W)) lastInput = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.S)) lastInput = Vector2.down;
        else if (Input.GetKeyDown(KeyCode.A)) lastInput = Vector2.left;
        else if (Input.GetKeyDown(KeyCode.D)) lastInput = Vector2.right;
    }

    private void MovePacStudent()
    {
        if (!isMoving)
        {
            Vector2Int nextGridPosition = gridPosition + Vector2Int.RoundToInt(lastInput);
            if (IsWalkable(nextGridPosition))
            {
                currentInput = lastInput;
                StartMoving(nextGridPosition);
            }
            else
            {
                nextGridPosition = gridPosition + Vector2Int.RoundToInt(currentInput);
                if (IsWalkable(nextGridPosition))
                {
                    StartMoving(nextGridPosition);
                }
            }
        }
        else
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);

            if ((Vector2)transform.position == targetPosition)
            {
                gridPosition = Vector2Int.RoundToInt(targetPosition);
                isMoving = false;
                walkAudio.Stop();
            }
        }
    }

    private void StartMoving(Vector2Int nextGridPosition)
    {
        targetPosition = new Vector2(nextGridPosition.x, nextGridPosition.y);
        isMoving = true;
        if (!walkAudio.isPlaying) 
            walkAudio.Play();
    }

    private bool IsWalkable(Vector2Int gridPos)
    {
        // Check against levelMap in LevelGenerator to see if gridPos is walkable
        // This should be customized based on your level's grid structure
        return true;
    }
}
