using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : MonoBehaviour
{
    public int bulletsPerShot;
    public float reloadTime, range, timeBetweenShots;
    public bool allowButtonHold;
    public int maxAmmo;
    public int currentAmmo;
    bool shooting, readyToShoot, reloading;
    // Start is called before the first frame update
    public GameObject arrowPrefab;
    public Transform arrowSpawner;
    Animator xbowAnimator;
    // Update is called once per frame
    void Start()
    {
        xbowAnimator = GetComponent<Animator>();
        readyToShoot = true;
        currentAmmo = maxAmmo;
    }
    void Update()
    {
        ManageInput();
    }
    
    private void ShootWeapon()
    {
        xbowAnimator.SetTrigger("ShootCrossbow");
        GameObject projectile = Instantiate(arrowPrefab);
        Physics.IgnoreLayerCollision(projectile.layer, gameObject.layer);
        projectile.transform.position = arrowSpawner.position;
        //Vector3 rotation = projectile.transform.rotation.eulerAngles;
        projectile.transform.rotation = Quaternion.Euler(arrowSpawner.eulerAngles.x, arrowSpawner.eulerAngles.y, arrowSpawner.eulerAngles.z);
        projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * 150f, ForceMode.Impulse);
        StartCoroutine(DestroyProjectile(projectile));
    }

    IEnumerator DestroyProjectile(GameObject projectile)
    {
        yield return new WaitForSeconds(2f);
        Destroy(projectile);
    }
    void Shoot()
    {
        readyToShoot = false;
        ShootWeapon();
        currentAmmo -= 1;
        StartCoroutine(resetShot());
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
        if (readyToShoot && shooting && currentAmmo > 0)
        {
            Debug.Log("Shooting.");
            Shoot();
        }
    }
}
