using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES_SeekWeapon : StateBase<EnemyStates>
{
    public Gun gunToSeek;
    public Enemy enemyRef;
    public ES_SeekWeapon(EnemyStates _key) : base(_key)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Seeking Gun: "+gunToSeek.name);
        enemyRef = GetMainClassReference<Enemy>();
    }

    public override void ExitState()
    {
        Debug.Log("Stopped Seeking Weapon");
    }

    public override void UpdateState()
    {
        if(gunToSeek.isPickedUp) //Switch to some other state
        if (!enemyRef.navmesh.hasPath || enemyRef.navmesh.remainingDistance <= enemyRef.navmesh.stoppingDistance)
        {
            if (enemyRef.navmesh.remainingDistance <= enemyRef.navmesh.stoppingDistance)
            {
            
            }
        }
        //if (enemyRef.fieldOfView.CanSeePlayer() != ENEMY_FOV_STATES.NOT_IN_VIEW) enemyRef.TransitionToState(EnemyStates.AttackStatic);        
    }
}
