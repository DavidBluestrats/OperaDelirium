using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES_Dazed : StateBase<EnemyStates>
{
    public Enemy enemyRef;
    /* In comparison to the Incapacitated State, Dazed enemies are not on the floor, they are standing.
     * They cannot perform any action for the duration of the dazed state, and don't require a getting back up
     * animation. If Dazed has been refreshed for N times, the enemy will instead go Incapacitated.
     */
    public float secondsDazed;
    public int timesDazed = 0;
    public ES_Dazed(EnemyStates _key) : base(_key)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Dazed for: "+secondsDazed+" secs.");
        enemyRef = GetMainClassReference<Enemy>();
        RefreshDazedStatus();
    }

    public override void ExitState()
    {
        Debug.Log("No longer dazed.");
    }

    public override void UpdateState()
    {
        if (secondsDazed <= 0)
        {
            //Transition to another state.
            if (enemyRef.fieldOfView.CanSeePlayer() != ENEMY_FOV_STATES.NOT_IN_VIEW) enemyRef.TransitionToState(EnemyStates.AttackStatic);
            else enemyRef.TransitionToState(EnemyStates.PatrolRandom);
        }
        else secondsDazed -= Time.deltaTime;
    }

    //Call this if the enemy is already dazed.
    public void RefreshDazedStatus()
    {
        timesDazed++;
        if (timesDazed >= 1)
        {
            timesDazed = 0;
            enemyRef.TransitionToState(EnemyStates.Incapacitated);
        }
        else
        {
            //Play some animation.
            enemyRef.navmesh.ResetPath();
            secondsDazed = 1f;
            Debug.Log("Dazed for: " + secondsDazed + " secs.");
        }
    }
}
