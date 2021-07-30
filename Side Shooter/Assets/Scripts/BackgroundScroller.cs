using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public Transform background1, background2;
    public float scrollSpeed;

    private float backgroundWidth;


    // Start is called before the first frame update
    void Start()
    {
        backgroundWidth = background1.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        background1.position = new Vector3(background1.position.x - (scrollSpeed * Time.deltaTime), background1.position.y, background2.position.z);
        background2.position -= new Vector3(scrollSpeed * Time.deltaTime, 0f, 0f);

        if (background1.position.x < -backgroundWidth -1) 
        {
            background1.position += new Vector3(backgroundWidth * 2f, 0f, 0f);
        }

        if (background2.position.x < -backgroundWidth - 1)
        {
            background2.position += new Vector3(backgroundWidth * 2f, 0f, 0f);
        }

    }
}
