using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int currentLives;

    public float respawnTime = 2f;       

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UIManager.instance.livesText.text = "x " + currentLives;
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

    public IEnumerator RespawnCo()
    {
        yield return new WaitForSeconds(respawnTime);

        HealthManager.instance.Respawn();

        WaveManager.instance.ContinueSpawning();
    }

}
