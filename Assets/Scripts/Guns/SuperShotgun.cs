using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperShotgun : MonoBehaviour
{
    // Start is called before the first frame update
    public int bulletsPerShot;
    public float reloadTime,spreadX,spreadY,range,timeBetweenShots;
    public bool allowButtonHold;
    public int maxAmmo;
    public int currentAmmo;

    public Transform bulletExit;
    public Camera worldCamera;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    bool shooting, readyToShoot, reloading;
    void Start()
    {
        readyToShoot = true;
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        ManageInput();
    }
    void ManageInput()
    {
        if (allowButtonHold)
        {
            shooting = Input.GetMouseButton(0);
        }
        else
        {
            shooting = Input.GetMouseButtonDown(0);
        }
        //shoot
        if(readyToShoot && shooting && currentAmmo > 0)
        {
            Shoot();   
        }
    }
    void Shoot()
    {
        readyToShoot = false;
        for(int i = 0; i < bulletsPerShot; i++)
        {
            GenerateRaycast();
        }
        currentAmmo -= 2;
        StartCoroutine(resetShot());
    }
    void GenerateRaycast()
    {
        Vector3 shotDir = getShotDir();
        Vector3 worldDir = worldCamera.transform.forward;
        Vector3 direction = worldDir + shotDir;
        if (Physics.Raycast(worldCamera.transform.position, direction ,out rayHit, range, whatIsEnemy))
        {
            //Debug.Log("Hit: "+rayHit.collider.name);
            Debug.DrawRay(worldCamera.transform.position, direction*range, Color.red,3f);
            if (isPointBlank(rayHit.collider.gameObject.transform, worldCamera.transform))
            {
                Debug.Log("Point blank shot.");
            }
            else
            {
                Debug.Log("Not Point blank shot.");
            }
        }
    }
    void ResetShoot()
    {
        readyToShoot = true;
    }
    IEnumerator resetShot()
    {
        yield return new WaitForSeconds(timeBetweenShots);
        ResetShoot();
    }
    Vector3 getShotDir()
    {
        Vector3 shotDir = Vector3.zero;
        shotDir.x = Random.Range(-spreadX, spreadX);
        shotDir.y = Random.Range(-spreadY, spreadY);
        shotDir.z = 0f;
        return shotDir;
    }

    bool isPointBlank(Transform enemyPosition, Transform shotEmissionPosition)
    {
        return (Vector3.Distance(enemyPosition.position, shotEmissionPosition.position) <= 5.4f);
    }
}
