using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsHandler : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Dictionary<int, Gun> weaponsHeld;
    private int currentlyHeldGun = 1;
    private Gun gunInHand;

    // Start is called before the first frame update
    void Start()
    {
        weaponsHeld = new Dictionary<int, Gun>();
        gunInHand = GetComponentInChildren<Gun>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if(gunInHand.canFire) FireGunInHand();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            ThrowGunInHand();
        }
    }

    private void FireGunInHand()
    {
        gunInHand.canFire = false;
        Vector3 mousePositionInWorld = CameraTopFollower.Ins.GetMousePositionInWorld();
        mousePositionInWorld.y = 0;
        Vector3 differenceOfPositions = mousePositionInWorld - transform.position;
        Vector3 directionFromPlayerToMouse = differenceOfPositions.normalized;

        gunInHand.Shoot(transform.position, player.forward);
        Invoke("ResetGunFireRate", gunInHand.timeBetweenShots);
        
    }

    void ResetGunFireRate()
    {
        gunInHand.canFire = true;
    }

    private void ThrowGunInHand()
    {
        //if holding weapon
            //Play generic throw animation.
            //Send weapon in hand flown towards direction player is looking.
        //If standing on another weapon, pick it up.
    }

    private void MeleeAttack()
    {

    }
}
