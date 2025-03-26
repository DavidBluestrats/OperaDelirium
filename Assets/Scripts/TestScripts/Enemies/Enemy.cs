using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyStates { Idle, Patrol, PatrolRandom, SeekPlayer, SeekPlayerAround, Incapacitated, Dazed, Dead, AttackStatic, AttackChasing, SeekWeapon };
public class Enemy : StateMachine<EnemyStates>
{
    // Start is called before the first frame update
    public NavMeshAgent navmesh;
    public DamageableEntity damageManager;
    public EnemyFieldOfView fieldOfView;
    public LayerMask pathfinderObstacleMask;
    public LayerMask pickupWeaponsLayerMask;
    public Transform playerTransform;
    public Transform mainHandRef;
    public Mover playerMover;
    public EnemyStates startState;

    public Gun currentlyHeldGun;

    void Awake()
    {
        navmesh = GetComponent<NavMeshAgent>();
        damageManager = GetComponent<DamageableEntity>();
        fieldOfView = GetComponent<EnemyFieldOfView>();
        playerMover = playerTransform.GetComponent<Mover>();

        NavMesh.pathfindingIterationsPerFrame = 5000;
        NavMesh.avoidancePredictionTime = 0.5f;

        AddState(EnemyStates.Patrol, new ES_Patrol(EnemyStates.Patrol), this);
        AddState(EnemyStates.PatrolRandom, new ES_PatrolRand(EnemyStates.PatrolRandom), this);
        AddState(EnemyStates.AttackStatic, new ES_AttackStatic(EnemyStates.AttackStatic), this);
        AddState(EnemyStates.SeekPlayer, new ES_SeekPlayer(EnemyStates.SeekPlayer), this);
        AddState(EnemyStates.SeekPlayerAround, new ES_SeekPlayerAround(EnemyStates.SeekPlayerAround), this);
        AddState(EnemyStates.Dazed, new ES_Dazed(EnemyStates.Dazed), this);
        AddState(EnemyStates.Dead, new ES_Dead(EnemyStates.Dead), this);
        AddState(EnemyStates.Incapacitated, new ES_Incapacitated(EnemyStates.Incapacitated), this);
    }
    private void Start()
    {
        EnterInitialState(States[startState]); 
    }

    public void FireGunInHand()
    {
        if (currentlyHeldGun == null) return;
        if (currentlyHeldGun.canFire)
        {
            currentlyHeldGun.canFire = false;

            Vector3 directionToPlayer = (playerTransform.position- transform.position).normalized;

            currentlyHeldGun.Shoot(transform.position, directionToPlayer);
            Invoke("ResetGunFireRate", currentlyHeldGun.timeBetweenShots);
        }
    }

    private void ResetGunFireRate()
    {
        currentlyHeldGun.canFire = true;
    }

    private void OnCollisionEnter(Collision collision)
    {

    }

    public Gun FindGunToPickUp()
    {
        Gun elligibleGun = null;
        Collider[] weaponsAround = Physics.OverlapSphere(transform.position, 5f, pickupWeaponsLayerMask);
        if (weaponsAround.Length > 0)
        {
            foreach (var weapon in weaponsAround)
            {
                Gun gun = weapon.GetComponent<Gun>();
                if (!gun.isPickedUp && !gun.isSoughtAfter && gun.currentAmmo > 0 && fieldOfView.CanSeeWeapon(gun))
                {
                    elligibleGun = gun;
                    break;
                }
            }            
        }

        return elligibleGun;
    }

}
