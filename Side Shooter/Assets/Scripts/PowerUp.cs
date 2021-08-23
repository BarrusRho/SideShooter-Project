using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public bool isShield;

    public bool isSpeedBoost;

    public bool isDoubleShot;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            MusicController.instance.powerUpAudio.Play();

            Destroy(this.gameObject, 0f);

            if (isShield == true)
            {
                HealthManager.instance.ActivateShield();               
            }
        }

        if (isSpeedBoost == true) 
        {
            PlayerController.instance.ActivateSpeedBoost();
        }

        if (isDoubleShot == true) 
        {
            PlayerController.instance.doubleShotActive = true;
        }
    }
}
