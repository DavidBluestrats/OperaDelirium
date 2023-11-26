using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    public string gunName;
    public string type;

    public float damagePerBullet;
    public float bulletSpeed;
    public int currentAmmo;
    public float coneMinAngle;
    public float coneMaxAngle;
    
    public bool canFire = true;
    public float timeBetweenShots;

    protected virtual void Init()
    {

    }

    public abstract void Shoot(Vector3 _shootPosition, Vector3 _targetDirection);

}
