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
    public bool isPickedUp;
    public bool isSoughtAfter;
    public bool isThrown;
    public float timeBetweenShots;

    private Rigidbody gunRigidBody;
    
    private void Awake()
    {
        gunRigidBody = GetComponent<Rigidbody>();
    }

    protected virtual void Init()
    {

    }

    public abstract void Shoot(Vector3 _shootPosition, Vector3 _targetDirection);

    private void OnCollisionEnter(Collision collision)
    {
        if (isThrown && gunRigidBody.velocity.magnitude >= 0.5f)
        {
            //Debug.Log(gunName + " thrown at: " + collision.transform.name);
            Enemy enemyHit = collision.gameObject.GetComponent<Enemy>();
            if (enemyHit != null)
            {
                if(enemyHit.GetCurrentState().GetType() == typeof(ES_Dazed))
                {
                    Debug.Log("MANAGED TO REGISTER RE-DAZE");
                    enemyHit.GetState<ES_Dazed>(EnemyStates.Dazed).RefreshDazedStatus();
                }
                else enemyHit.TransitionToState(EnemyStates.Dazed);
                isThrown = false;
            }
        }
        else
        {
            isThrown = false;
            Debug.Log("Stopped detecting collisions.");
        }
    }

}
