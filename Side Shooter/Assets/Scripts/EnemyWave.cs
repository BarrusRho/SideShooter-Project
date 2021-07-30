using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DetachChildren();

        Destroy(this.gameObject, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
