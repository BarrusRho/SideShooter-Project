using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    public float movementSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3((movementSpeed * Time.deltaTime), 0f, 0f);
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject, 1f);
    }
}
