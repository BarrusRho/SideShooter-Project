using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    public float shotSpeed = 7.0f;

    public GameObject impactEffect;

    public GameObject objectExplosion;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3((shotSpeed * Time.deltaTime), 0f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(impactEffect, transform.position, transform.rotation);

        if (other.tag == "SpaceObject")
        {
            Instantiate(objectExplosion, other.transform.position, other.transform.rotation);

            MusicController.instance.enemyExplosionAudio.Play();

            MusicController.instance.shotImpactAudio.Play();

            Destroy(other.gameObject, 0f);

            GameManager.instance.AddScore(50);
        }

        if(other.tag == "Enemy") 
        {
            other.GetComponent<EnemyController>().DamageEnemy();

            MusicController.instance.shotImpactAudio.Play();
        }

        if(other.tag == "Boss") 
        {
            BossManager.instance.DamageBoss();

            MusicController.instance.shotImpactAudio.Play();
        }

        Destroy(this.gameObject, 0f);
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject, 0f);
    }
}
