using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int currentLives;

    public float respawnTime = 2f;

    public int currentScore;

    private int highScore;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UIManager.instance.livesText.text = "x " + currentLives;

        UIManager.instance.scoreText.text = "Score: " + currentScore;

        highScore = PlayerPrefs.GetInt("HighScore");

        UIManager.instance.highScoreText.text = "High Score: " + highScore;
    }

    void Update()
    {

    }

    public void DestroyPlayer()
    {
        currentLives = currentLives - 1;

        UIManager.instance.livesText.text = "x " + currentLives;

        if (currentLives > 0)
        {
            StartCoroutine(RespawnCo());
        }
        else
        {
            UIManager.instance.userInterface.SetActive(true);

            WaveManager.instance.canSpawnWaves = false;
        }
    }

    public void AddScore(int scoreToAdd)
    {
        currentScore += scoreToAdd;

        UIManager.instance.scoreText.text = "Score: " + currentScore;

        if (currentScore > highScore)
        {
            highScore = currentScore;

            UIManager.instance.highScoreText.text = "High Score: " + highScore;

            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }

    public IEnumerator RespawnCo()
    {
        yield return new WaitForSeconds(respawnTime);

        HealthManager.instance.Respawn();

        WaveManager.instance.ContinueSpawning();
    }

}
