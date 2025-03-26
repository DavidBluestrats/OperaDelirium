using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsHandler : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform mainHandRef;
    [SerializeField] private Transform secondHandRef;

    private Dictionary<int, Gun> weaponsHeld;
    private int currentlyHeldGun = 1;
    private Gun gunInHand;

    public LayerMask weaponsLayerMask;

    // Start is called before the first frame update
    void Start()
    {
        weaponsHeld = new Dictionary<int, Gun>();
        gunInHand = mainHandRef.GetComponentInChildren<Gun>();
        weaponsHeld.Add(currentlyHeldGun, gunInHand);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if(gunInHand!= null && gunInHand.canFire) FireGunInHand();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            ThrowGunInHand();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchMainHandWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchMainHandWeapon(2);
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
        if(gunInHand)
            gunInHand.canFire = true;
    }

    private void SwitchMainHandWeapon(int _weaponToSwitchTo)
    {


        currentlyHeldGun = _weaponToSwitchTo;
    }

    private void ThrowGunInHand()
    {
        if (weaponsHeld.ContainsKey(currentlyHeldGun))
        {
            //Play animation.
            Gun gunToThrow = weaponsHeld[currentlyHeldGun];
            gunToThrow.transform.parent = null;
            Rigidbody weaponRigidBody = gunToThrow.GetComponent<Rigidbody>();
            weaponRigidBody.maxAngularVelocity = 100f;
            weaponRigidBody.isKinematic = false;
            weaponRigidBody.AddForce(transform.forward * 55f, ForceMode.Impulse);
            weaponRigidBody.AddTorque(weaponRigidBody.transform.up * 30f, ForceMode.VelocityChange);
            gunToThrow.isThrown = true;

            weaponsHeld.Remove(currentlyHeldGun);
            gunInHand = null;
        }
        else
        {
            //If standing on another weapon, pick it up.
            Collider[] weaponsAround = Physics.OverlapSphere(transform.position, 1.5f, weaponsLayerMask);
            if (weaponsAround.Length > 0)
            {
                Gun gunToGrab = weaponsAround[0].GetComponent<Gun>();
                gunToGrab.transform.parent = mainHandRef;
                gunToGrab.transform.localRotation = Quaternion.identity;
                gunToGrab.transform.localPosition = Vector3.zero;
                gunToGrab.GetComponent<Rigidbody>().isKinematic = true;
                gunToGrab.canFire = true;

                gunInHand = gunToGrab;
                weaponsHeld.Add(currentlyHeldGun, gunToGrab);
                Debug.Log("Picked up: "+gunToGrab.name);
            }
            else
            {
                Debug.Log("No Weapons on the Ground");
            }
        }
    }

    private void MeleeAttack()
    {

    }
}
