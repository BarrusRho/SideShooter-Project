using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int currentLives = 3;

    public float respawnTime = 2f;

    public int currentScore;

    private int highScore;

    public bool levelEnd;

    private int levelScore;

    public float waitForLevelEnd = 5f;

    public string nextLevel;

    private bool canPause;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentLives = PlayerPrefs.GetInt("CurrentLives");

        UIManager.instance.livesText.text = "x " + currentLives;        

        highScore = PlayerPrefs.GetInt("HighScore");

        UIManager.instance.highScoreText.text = "High Score: " + highScore;

        currentScore = PlayerPrefs.GetInt("CurrentScore");

        UIManager.instance.scoreText.text = "Score: " + currentScore;

        canPause = true;
    }

    void Update()
    {
        if(levelEnd == true) 
        {
            PlayerController.instance.transform.position += new Vector3((PlayerController.instance.boostSpeed * Time.deltaTime), 0f, 0f);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && canPause == true) 
        {
            PauseUnpause();
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

            PlayerPrefs.SetInt("HighScore", highScore);

            canPause = false;
        }
    }

    public void AddScore(int scoreToAdd)
    {
        currentScore += scoreToAdd;

        levelScore += scoreToAdd;

        UIManager.instance.scoreText.text = "Score: " + currentScore;

        if (currentScore > highScore)
        {
            highScore = currentScore;

            UIManager.instance.highScoreText.text = "High Score: " + highScore;

            //PlayerPrefs.SetInt("HighScore", highScore);
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
        canPause = false;

        UIManager.instance.levelEndPanel.SetActive(true);

        PlayerController.instance.stopMovement = true;

        levelEnd = true;

        MusicController.instance.PlayVictoryAudio();

        yield return new WaitForSeconds(0.5f);

        UIManager.instance.endLevelScore.text = "Level Score: " + levelScore;

        UIManager.instance.endLevelScore.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        PlayerPrefs.SetInt("CurrentScore", currentScore);

        UIManager.instance.endCurrentScore.text = "Total Score: " + currentScore;

        UIManager.instance.endCurrentScore.gameObject.SetActive(true);

        if (currentScore == highScore) 
        {
            yield return new WaitForSeconds(0.5f);

            UIManager.instance.highScoreNotice.SetActive(true);
        }

        PlayerPrefs.SetInt("HighScore", highScore);

        PlayerPrefs.SetInt("CurrentLives", currentLives);

        yield return new WaitForSeconds(waitForLevelEnd);

        SceneManager.LoadScene(nextLevel);
    }

    public void PauseUnpause() 
    {
        if (UIManager.instance.pauseMenuPanel.activeInHierarchy) 
        {
            UIManager.instance.pauseMenuPanel.SetActive(false);

            Time.timeScale = 1f;

            PlayerController.instance.stopMovement = false;
        }
        else 
        {
            UIManager.instance.pauseMenuPanel.SetActive(true);

            Time.timeScale = 0f;

            PlayerController.instance.stopMovement = true;
        }
    }
}
