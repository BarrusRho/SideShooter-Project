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

    public void DestroyPlayer()
    {
        currentLives = currentLives - 1;

        if (currentLives > 0)
        {
            StartCoroutine(RespawnCo());
        }
        else
        {
            //game over
        }
    }

    public IEnumerator RespawnCo()
    {
        yield return new WaitForSeconds(respawnTime);

        HealthManager.instance.Respawn();

        WaveManager.instance.ContinueSpawning();
    }

}
