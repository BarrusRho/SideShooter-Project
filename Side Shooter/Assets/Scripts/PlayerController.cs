using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D playerRigidbody;

    public Transform lowerLeftLimit, upperRightLimit;

    public Transform shotPoint;
    public GameObject shot;

    public float timeBetweenShots = 0.2f;
    private float shotCounter;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playerRigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, lowerLeftLimit.position.x, upperRightLimit.position.x), Mathf.Clamp(transform.position.y, lowerLeftLimit.position.y, upperRightLimit.position.y), transform.position.z);

        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(shot, shotPoint.position, shotPoint.rotation);

            shotCounter = timeBetweenShots;
        }

        if (Input.GetButton("Fire1"))
        {
            shotCounter -= Time.deltaTime;

            if (shotCounter <= 0f)
            {
                Instantiate(shot, shotPoint.position, shotPoint.rotation);

                shotCounter = timeBetweenShots;
            }
        }
    }
}
