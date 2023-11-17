using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObstacleDelete : MonoBehaviour
{
    // Start is called before the first frame update
    private float timeToDespawn;
    // Update is called once per frame
    private void Start()
    {
        timeToDespawn = Time.time + 4.5f;
    }
    void Update()
    {
        if(Time.time > timeToDespawn)
        {
            Destroy(gameObject);
        }
    }
}
