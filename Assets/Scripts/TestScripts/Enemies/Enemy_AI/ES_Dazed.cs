using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES_Dazed : StateBase<EnemyStates>
{

    /* In comparison to the Incapacitated State, Dazed enemies are not on the floor, they are standing.
     * They cannot perform any action for the duration of the dazed state, and don't require a getting back up
     * animation. If Dazed has been refreshed for N times, the enemy will instead go Incapacitated.
     */
    public float secondsDazed;
    public int timesDazed;
    public ES_Dazed(EnemyStates _key) : base(_key)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Dazed for: "+secondsDazed+" secs.");
        timesDazed++;
        //Play some animation.
    }

    public override void ExitState()
    {
        Debug.Log("No longer dazed.");
    }

    public override void UpdateState()
    {
        if (secondsDazed <= 0)
        {
            Debug.Log("No longer Dazed");
            //Transition to another state.
        }
        else secondsDazed -= Time.deltaTime;
    }

    public void RefreshDazedStatus()
    {
        //Call this if the enemy is already dazed.
        timesDazed++;
    }
}
