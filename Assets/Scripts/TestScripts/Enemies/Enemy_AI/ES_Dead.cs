using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES_Dead : StateBase<EnemyStates>
{
    public ES_Dead(EnemyStates _key) : base(_key)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Enemy Killed");
    }

    public override void ExitState()
    {
        return;
    }

    public override void UpdateState()
    {
        return;
    }
}
