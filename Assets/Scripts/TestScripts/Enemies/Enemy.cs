using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyStates { Idle, Patrol, PatrolRandom, SeekPlayer, Incapacitated, Dazed, Dead, AttackStatic, AttackChasing, SeekWeapon };
public class Enemy : StateMachine<EnemyStates>
{
    // Start is called before the first frame update
    public NavMeshAgent navmesh;
    public DamageableEntity damageManager;
    public EnemyFieldOfView fieldOfView;
    public LayerMask pathfinderObstacleMask;
    public Transform playerTransform;
    void Awake()
    {
        navmesh = GetComponent<NavMeshAgent>();
        damageManager = GetComponent<DamageableEntity>();
        fieldOfView = GetComponent<EnemyFieldOfView>();

        AddState(EnemyStates.Patrol, new ES_Patrol(EnemyStates.Patrol), this);
        AddState(EnemyStates.PatrolRandom, new ES_PatrolRand(EnemyStates.PatrolRandom), this);
        AddState(EnemyStates.AttackStatic, new ES_AttackStatic(EnemyStates.AttackStatic), this);
    }
    private void Start()
    {
        EnterInitialState(States[EnemyStates.PatrolRandom]); 
        //EnterInitialState(States[EnemyStates.Patrol]);
        //EnterInitialState(States[EnemyStates.AttackStatic]);
    }

}
