using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyStates { Idle, Patrol, PatrolRandom, SeekPlayer, Incapacitated, Dazed, Dead, Attack, AttackChasing, SeekWeapon };
public class Enemy : StateMachine<EnemyStates>
{
    // Start is called before the first frame update
    public NavMeshAgent navmesh;
    public DamageableEntity damageManager;
    public LayerMask pathfinderObstacleMask;
    void Awake()
    {
        navmesh = GetComponent<NavMeshAgent>();
        damageManager = GetComponent<DamageableEntity>();

        AddState(EnemyStates.Patrol, new ES_Patrol(EnemyStates.Patrol), this);

    }
    private void Start()
    {
        EnterInitialState(States[EnemyStates.Patrol]); 
    }

}
