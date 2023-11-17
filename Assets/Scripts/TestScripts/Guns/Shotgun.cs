using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    public float damagePerBullet = 20f;
    public float bulletSpeed = 50f;
    public int currentAmmo = 6;
    public int reloadShells = 0;
    public float coneMinAngle = -15f;
    public float coneMaxAngle = 15f;
    public float timeBetweenShots = 0.8f;



    public GameObject bulletPrefab;
    public Transform bulletExit;
    public override void Shoot(Vector3 _shootPosition, Vector3 _targetDirection)
    {
        int bulletCount = Random.Range(6, 12);
        for (int i = 0; i < bulletCount; i++)
        {
            // Calculate random direction within the cone
            float randomAngle = Random.Range(coneMinAngle, coneMaxAngle);
            Vector3 randomDirection = Quaternion.Euler(Random.Range(-15f, 15f), 0f, Random.Range(-15f, 15f)) * _targetDirection;
            //randomDirection = Quaternion.AngleAxis(randomAngle, Vector3.up) * randomDirection;

            // Calculate random starting position within the shotgun's range
            //Vector3 randomStartPosition = bulletExit.position + Random.insideUnitSphere * 0.5f; // Adjust 0.5f as needed

            Vector3 randomStartPosition = bulletExit.position;
            // Calculate bullet velocity
            Vector3 bulletVelocity = randomDirection.normalized * bulletSpeed;

            // Instantiate the bullet prefab and set its position and velocity
            GameObject bullet = Instantiate(bulletPrefab, randomStartPosition, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().velocity = bulletVelocity;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
