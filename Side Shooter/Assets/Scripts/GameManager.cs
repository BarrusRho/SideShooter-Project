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

    public bool levelEnd;

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
        if(levelEnd == true) 
        {
            PlayerController.instance.transform.position += new Vector3((PlayerController.instance.boostSpeed * Time.deltaTime), 0f, 0f);
        }
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
            UIManager.instance.gameOverPanel.SetActive(true);

            WaveManager.instance.canSpawnWaves = false;

            MusicController.instance.PlayGameOverAudio();
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

    public IEnumerator EndLevelCo() 
    {
        UIManager.instance.levelEnd_Panel.SetActive(true);

        PlayerController.instance.stopMovement = true;

        levelEnd = true;

        MusicController.instance.PlayVictoryAudio();

        yield return new WaitForSeconds(0.5f);
    }

}
