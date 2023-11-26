using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES_AttackStatic : StateBase<EnemyStates>
{

    private Enemy enemyRef;
    private float rotationSpeed = 6f;
    private float shootReactionTimeInSecs = 0.25f;


    public ES_AttackStatic(EnemyStates _key) : base(_key)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Entered AttackStatic.");
        enemyRef = GetMainClassReference<Enemy>();

        enemyRef.navmesh.ResetPath();
    }

    public override void ExitState()
    {
        Debug.Log("Exit AttackStatic.");
    }

    public override void UpdateState()
    {
        ENEMY_FOV_STATES detectionState = enemyRef.fieldOfView.CanSeePlayer();
        if (detectionState == ENEMY_FOV_STATES.IN_VIEW || detectionState == ENEMY_FOV_STATES.IN_SHOOT_VIEW)
        {

            float distanceToPlayer = Vector3.Distance(enemyRef.transform.position, enemyRef.playerTransform.position);

            if (distanceToPlayer >= 13f)
            {
                // Follow the player
                enemyRef.navmesh.SetDestination(enemyRef.playerTransform.position);
            }
            else enemyRef.navmesh.ResetPath();

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
                    enemyRef.FireGunInHand();
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
            ES_SeekPlayer seekPlayerState = enemyRef.GetState<ES_SeekPlayer>(EnemyStates.SeekPlayer);
            seekPlayerState.playerLastKnownLocation = enemyRef.playerTransform.position;
            enemyRef.navmesh.ResetPath();

            enemyRef.TransitionToState(EnemyStates.SeekPlayer);
        }
    }
}
