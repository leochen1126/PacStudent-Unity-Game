using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class PacStudentController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Vector2Int gridPosition;
    private Vector2 targetPosition;
    private bool isMoving = false;
    private Vector2 lastInput;
    private Vector2 currentInput;
    private AudioSource walkAudio;
    public int score = 0;
    public int lives = 3;
    public Text scoreText;
    public Text livesText;
    public Text gameOverText;
    public ParticleSystem wallCollisionEffect;
    public AudioClip wallCollisionSound;
    private AudioSource audioSource;
    private Animator animator;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            HandleWallCollision();
        }
        else if (other.CompareTag("Pellet"))
        {
            HandlePelletCollision(other.gameObject);
        }
        else if (other.CompareTag("Cherry"))
        {
            HandleCherryCollision(other.gameObject);
        }
        if (other.CompareTag("Teleporter"))
        {
            HandleTeleport();
        }
        // Add other collision cases here for Power Pills and Ghosts
    }

    void HandleWallCollision()
    {
        wallCollisionEffect.Play();
        audioSource.PlayOneShot(wallCollisionSound);
        // Implement logic to prevent further movement in this direction
    }

    void HandlePelletCollision(GameObject pellet)
    {
        Destroy(pellet);
        score += 10;
        UpdateUI();
    }

    void HandleCherryCollision(GameObject cherry)
    {
        Destroy(cherry);
        score += 100;
        UpdateUI();
    }

    void Start()
    {
        gridPosition = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        targetPosition = transform.position;
        walkAudio = GetComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
        UpdateUI();
        animator = GetComponent<Animator>();
    }
    void UpdateAnimation()
    {
        animator.SetFloat("MoveX", lastInput.x);
        animator.SetFloat("MoveY", lastInput.y);
    }
    void Update()
    {
        HandleInput();
        MovePacStudent();
        UpdateAnimation();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W)) lastInput = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.S)) lastInput = Vector2.down;
        else if (Input.GetKeyDown(KeyCode.A)) lastInput = Vector2.left;
        else if (Input.GetKeyDown(KeyCode.D)) lastInput = Vector2.right;
    }
    void UpdateUI()
    {
        scoreText.text = "Score: " + score;
        livesText.text = "Lives: " + lives;
    }

    void HandleTeleport()
    {
        Vector3 newPosition = transform.position.x < 0 ? new Vector3(10, transform.position.y, 0) : new Vector3(-10, transform.position.y, 0);
        transform.position = newPosition;
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
