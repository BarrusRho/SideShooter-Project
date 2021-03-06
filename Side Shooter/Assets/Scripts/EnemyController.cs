using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float movementSpeed;

    public Vector2 startDirection;

    public bool shouldChangeDirection;
    public float changeDirectionXPoint;
    public Vector2 changedDirection;

    public GameObject enemyShot;
    public Transform firePoint;
    public float timeBetweenShots;
    private float shotCounter;

    public bool canShoot;
    private bool allowShooting;

    public int currentHealth;
    public GameObject enemyExplosion;

    public int scoreValue = 100;

    public GameObject[] powerUps;

    public int dropRate = 15;

    void Start()
    {
        shotCounter = timeBetweenShots;
    }

    // Update is called once per frame
    void Update()
    {
        if (!shouldChangeDirection)
        {
            transform.position += new Vector3((startDirection.x * movementSpeed * Time.deltaTime), (startDirection.y * movementSpeed * Time.deltaTime), 0f);
        }
        else
        {
            if (transform.position.x > changeDirectionXPoint)
            {
                transform.position += new Vector3((startDirection.x * movementSpeed * Time.deltaTime), (startDirection.y * movementSpeed * Time.deltaTime), 0f);
            }
            else
            {
                transform.position += new Vector3((changedDirection.x * movementSpeed * Time.deltaTime), (changedDirection.y * movementSpeed * Time.deltaTime), 0f);
            }
        }

        if (allowShooting == true)
        {
            shotCounter -= Time.deltaTime;

            if (shotCounter <= 0)
            {
                shotCounter = timeBetweenShots;
                Instantiate(enemyShot, firePoint.position, firePoint.rotation);
            }
        }

        //transform.position -= new Vector3((movementSpeed * Time.deltaTime), 0f, 0f);

    }

    public void DamageEnemy()
    {
        currentHealth = currentHealth - 1;

        if (currentHealth <= 0) 
        {
            GameManager.instance.AddScore(scoreValue);

            int randomChance = Random.Range(0, 100);

            if (randomChance < dropRate) 
            {
                int randomDrop = Random.Range(0, powerUps.Length);

                Instantiate(powerUps[randomDrop], transform.position, transform.rotation);
            }            

            Instantiate(enemyExplosion, transform.position, transform.rotation);

            MusicController.instance.enemyExplosionAudio.Play();

            Destroy(this.gameObject, 0f);
        }

    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject, 1f);
    }

    private void OnBecameVisible()
    {
        if (canShoot == true) 
        {
            allowShooting = true;
        }
    }
}
