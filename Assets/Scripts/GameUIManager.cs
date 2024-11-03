using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    public Text countdownText;
    public Text gameTimerText;
    public Text gameOverText; // For displaying "Game Over"
    public AudioSource backgroundMusic;
    private float gameTime;
    public bool gameStarted = false;

    void Start()
    {
        gameOverText.gameObject.SetActive(false); // Hide Game Over text initially
        StartCoroutine(RoundStartCountdown());
        gameStarted = true;
        InvokeRepeating("UpdateGameTimer", 0f, 0.1f);
    }
    private void Awake()
    {
        gameStarted = true;
        InvokeRepeating("UpdateGameTimer", 0f, 0.1f);
    }

    void StartGame()
    {
        
        // Start background music if it¡¦s assigned
        if (backgroundMusic != null)
        {
            backgroundMusic.loop = true;
            backgroundMusic.Play();
        }

        InvokeRepeating("UpdateGameTimer", 0f, 0.1f); // Start game timer
    }

    void UpdateGameTimer()
    {
        if (!gameStarted) return;

        gameTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(gameTime / 60F);
        int seconds = Mathf.FloorToInt(gameTime % 60F);
        int milliseconds = Mathf.FloorToInt((gameTime * 60F) % 60F);
        gameTimerText.text = $"{minutes:00}:{seconds:00}:{milliseconds:00}";
    }

    IEnumerator RoundStartCountdown()
    {
        countdownText.gameObject.SetActive(true);

        countdownText.text = "3";
        yield return new WaitForSeconds(1);

        countdownText.text = "2";
        yield return new WaitForSeconds(1);

        countdownText.text = "1";
        yield return new WaitForSeconds(1);

        countdownText.text = "GO!";
        yield return new WaitForSeconds(1);

        countdownText.gameObject.SetActive(false);
        gameStarted = true; // Set this to true before starting the timer
        StartGame();
    }

    public void GameOver(int finalScore, float finalTime)
    {
        CancelInvoke("UpdateGameTimer");  // Stop the timer
        gameStarted = false; // Prevent further movement or actions

        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        float bestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue);

        if (finalScore > highScore || (finalScore == highScore && finalTime < bestTime))
        {
            PlayerPrefs.SetInt("HighScore", finalScore);
            PlayerPrefs.SetFloat("BestTime", finalTime);
            PlayerPrefs.Save();
        }

        // Display Game Over text
        gameOverText.gameObject.SetActive(true);
        StartCoroutine(ShowGameOverScreen());
    }

    IEnumerator ShowGameOverScreen()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("StartScene"); // Return to Start Scene after 3 seconds
    }
}
