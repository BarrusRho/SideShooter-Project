using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed;
    public Rigidbody2D playerRigidbody;

    public Transform lowerLeftLimit, upperRightLimit;

    public Transform shotPoint;
    public GameObject shot;

    public float timeBetweenShots = 0.2f;
    private float shotCounter;

    private float normalSpeed;
    public float boostSpeed;
    public float boostDuration;
    private float boostCounter;

    public bool doubleShotActive;
    public float doubleShotOffset;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        normalSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        playerRigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, lowerLeftLimit.position.x, upperRightLimit.position.x), Mathf.Clamp(transform.position.y, lowerLeftLimit.position.y, upperRightLimit.position.y), transform.position.z);

        if (Input.GetButtonDown("Fire1"))
        {
            if (!doubleShotActive) 
            {
                Instantiate(shot, shotPoint.position, shotPoint.rotation);
            }
            else 
            {
                Instantiate(shot, shotPoint.position + new Vector3(0f, doubleShotOffset, 0f), shotPoint.rotation);

                Instantiate(shot, shotPoint.position - new Vector3(0f, doubleShotOffset, 0f), shotPoint.rotation);
            }

            shotCounter = timeBetweenShots;
        }

        if (Input.GetButton("Fire1"))
        {
            shotCounter -= Time.deltaTime;

            if (shotCounter <= 0f)
            {
                if (!doubleShotActive)
                {
                    Instantiate(shot, shotPoint.position, shotPoint.rotation);
                }
                else
                {
                    Instantiate(shot, shotPoint.position + new Vector3(0f, doubleShotOffset, 0f), shotPoint.rotation);

                    Instantiate(shot, shotPoint.position - new Vector3(0f, doubleShotOffset, 0f), shotPoint.rotation);
                }

                shotCounter = timeBetweenShots;
            }
        }

        if (boostCounter > 0)
        {
            boostCounter -= Time.deltaTime;

            if (boostCounter <= 0) 
            {
                moveSpeed = normalSpeed;
            }
        }
    }

    public void ActivateSpeedBoost() 
    {
        boostCounter = boostDuration;
        moveSpeed = boostSpeed;
    }
}
