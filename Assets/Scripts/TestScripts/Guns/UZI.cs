using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UZI : Gun
{
    public GameObject bulletPrefab;
    public Transform bulletExit;
    public override void Shoot(Vector3 _shootPosition, Vector3 _targetDirection)
    {
        int bulletCount = 1;
        for (int i = 0; i < bulletCount; i++)
        {
            // Calculate random direction within the cone
            Vector3 randomDirection = Quaternion.Euler(0f, Random.Range(coneMinAngle, coneMaxAngle), Random.Range(coneMinAngle, coneMaxAngle)) * _targetDirection;
            //randomDirection = Quaternion.AngleAxis(randomAngle, Vector3.up) * randomDirection;

            // Calculate random starting position within the shotgun's range
            //Vector3 randomStartPosition = bulletExit.position + Random.insideUnitSphere * 0.5f; // Adjust 0.5f as needed

            Vector3 randomStartPosition = bulletExit.position;
            // Calculate bullet velocity
            Vector3 bulletVelocity = randomDirection.normalized * bulletSpeed;
            bulletVelocity.y = 0f;

            // Instantiate the bullet prefab and set its position and velocity
            GameObject bullet = Instantiate(bulletPrefab, randomStartPosition, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().velocity = bulletVelocity;
        }
    }
}
