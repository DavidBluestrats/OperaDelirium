using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES_AttackStatic : StateBase<EnemyStates>
{

    private Enemy enemyRef;
    private EnemyFieldOfView fov;
    private float rotationSpeed = 3.5f;
    private float shootReactionTimeInSecs = 0.7f;


    public ES_AttackStatic(EnemyStates _key) : base(_key)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Entered AttackStatic.");
        enemyRef = GetMainClassReference<Enemy>();
        fov = enemyRef.GetComponent<EnemyFieldOfView>();
    }

    public override void ExitState()
    {
        Debug.Log("Exit AttackStatic.");
    }

    public override void UpdateState()
    {
        ENEMY_FOV_STATES detectionState = fov.CanSeePlayer();
        if (detectionState == ENEMY_FOV_STATES.IN_VIEW || detectionState == ENEMY_FOV_STATES.IN_SHOOT_VIEW)
        {
            enemyRef.navmesh.updateRotation = false;
            Vector3 direction = enemyRef.playerTransform.position - enemyRef.transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            lookRotation.x = 0; lookRotation.z = 0;
            enemyRef.transform.rotation = Quaternion.Lerp(enemyRef.transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

            if (detectionState == ENEMY_FOV_STATES.IN_SHOOT_VIEW)
            {
                if(shootReactionTimeInSecs <= 0f)
                {
                    //Shoot at player
                    Debug.Log("SHOOTING AT PLAYER!!");
                }
                else
                {
                    shootReactionTimeInSecs -= Time.deltaTime;
                }
            }
        }
        else
        {
            shootReactionTimeInSecs = 0.5f;
            enemyRef.navmesh.updateRotation = true;
            //Transition to Seek State
        }
    }
}
