using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES_Incapacitated : StateBase<EnemyStates>
{
    public float secondsOnTheGround;
    public bool backUp;
    public ES_Incapacitated(EnemyStates _key) : base(_key)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Enemy ON the Ground");
        //Play some animation.
        backUp = false;
    }

    public override void ExitState()
    {
        Debug.Log("Enemy OFF the Ground");
    }

    public override void UpdateState()
    {
        if (!backUp)
        {
            if (secondsOnTheGround <= 0)
            {
                //Get back up.
            }
            else
            {
                secondsOnTheGround -= Time.deltaTime;
            }
        }
        else
        {
            //Transition to another state.
        }
    }
}
