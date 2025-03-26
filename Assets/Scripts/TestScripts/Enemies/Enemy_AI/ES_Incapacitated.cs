using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES_Incapacitated : StateBase<EnemyStates>
{
    public Enemy enemyRef;
    public float secondsOnTheGround;
    public bool backUp;
    public ES_Incapacitated(EnemyStates _key) : base(_key)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Enemy ON the Ground");
        enemyRef = GetMainClassReference<Enemy>();
        enemyRef.navmesh.enabled = false;
        //Play some animation.
        backUp = false;
        secondsOnTheGround = 5f;
        enemyRef.transform.Rotate(new Vector3(-90f,0f,0f), Space.Self);
        //enemyRef.transform.localRotation = Quaternion.Euler(-90f, 0f,0f);
    }

    public override void ExitState()
    {
        Debug.Log("Enemy OFF the Ground");
        enemyRef.transform.Rotate(new Vector3(90f, 0f, 0f), Space.Self);
        //enemyRef.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        enemyRef.navmesh.enabled = true;
    }

    public override void UpdateState()
    {
        if (!backUp)
        {
            if (secondsOnTheGround <= 0)
            {
                //Get back up.
                backUp = true;
            }
            else
            {
                secondsOnTheGround -= Time.deltaTime;
            }
        }
        else
        {
            //Transition to another state.
            enemyRef.TransitionToState(EnemyStates.PatrolRandom);
        }
    }
}
