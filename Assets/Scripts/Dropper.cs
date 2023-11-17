using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    public GameObject playerLocation;
    public GameObject fallingObstaclePrefab;
    float secondsBetweenSpawns = 5f;
    float nextSpawn = 0f;
    // Start is called before the first frame update
 
    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextSpawn)
        {
            nextSpawn = Time.time + secondsBetweenSpawns;
            Vector3 spawnLocation = new Vector3(Random.Range(playerLocation.transform.position.x-3, playerLocation.transform.position.x+3),
                15f, Random.Range(playerLocation.transform.position.z - 3, playerLocation.transform.position.z + 3));
            Instantiate(fallingObstaclePrefab,spawnLocation,Quaternion.identity);
        }
    }
}
