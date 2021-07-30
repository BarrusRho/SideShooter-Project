using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance; //singleton

    public int currentHealth;
    public int maxHealth;

    public GameObject playerExplosion;

    public float invincibleLength = 2f;
    private float invincibilityCounter;

    public SpriteRenderer sR;

    public void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibilityCounter >= 0)
        {
            invincibilityCounter -= Time.deltaTime;

            if (invincibilityCounter <= 0) 
            {
                sR.color = new Color(sR.color.r, sR.color.g, sR.color.b, 1f);
            }
        }
    }

    public void DamagerPlayer()
    {
        if (invincibilityCounter <= 0) 
        {
            currentHealth = currentHealth - 1;

            if (currentHealth <= 0)
            {
                Instantiate(playerExplosion, transform.position, transform.rotation);

                gameObject.SetActive(false);

                GameManager.instance.DestroyPlayer();

                WaveManager.instance.canSpawnWaves = false;
            }
        }
    }

    public void Respawn()
    {
        gameObject.SetActive(true);
        currentHealth = maxHealth;

        invincibilityCounter = invincibleLength;

        sR.color = new Color(sR.color.r, sR.color.g, sR.color.b, 0.5f);

    }
}
