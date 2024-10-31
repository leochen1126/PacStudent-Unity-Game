using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameUIManager : MonoBehaviour
{
    public Text countdownText;
    public Text gameTimerText;
    private float gameTime;

    void Start()
    {
        StartCoroutine(RoundStartCountdown());
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
        StartGame();
    }

    void StartGame()
    {
        InvokeRepeating("UpdateGameTimer", 0f, 0.1f);  // Start timer update every frame
    }

    void UpdateGameTimer()
    {
        gameTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(gameTime / 60F);
        int seconds = Mathf.FloorToInt(gameTime % 60F);
        int milliseconds = Mathf.FloorToInt((gameTime * 100F) % 100F);
        gameTimerText.text = $"{minutes:00}:{seconds:00}:{milliseconds:00}";
    }

    public void GameOver(int finalScore, float finalTime)
    {
        CancelInvoke("UpdateGameTimer");  // Stop the timer
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        float bestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue);

        if (finalScore > highScore || (finalScore == highScore && finalTime < bestTime))
        {
            PlayerPrefs.SetInt("HighScore", finalScore);
            PlayerPrefs.SetFloat("BestTime", finalTime);
            PlayerPrefs.Save();
        }

        StartCoroutine(ShowGameOverScreen());
    }

    IEnumerator ShowGameOverScreen()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("StartScene");
    }
}

