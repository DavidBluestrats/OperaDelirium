using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 In this Patrol state, the enemy will travel around a perimeter as long as it is able to.
 The enemy will turn counter clock wise around a perimeter
 */
public class ES_Patrol : StateBase<EnemyStates>
{
    private Enemy enemyRef;
    public ES_Patrol(EnemyStates _key) : base(_key)
    {
        
    }

    public override void EnterState()
    {
        Debug.Log("Entered Patrol State");
        enemyRef = GetMainClassReference<Enemy>();
        enemyRef.navmesh.stoppingDistance = 0f;
    }

    public override void ExitState()
    {
        Debug.Log("Exit Patrol State");

    }

    public override void UpdateState()
    {

        if (!enemyRef.navmesh.hasPath || enemyRef.navmesh.remainingDistance <= enemyRef.navmesh.stoppingDistance)
        {
            Vector3 accuratePosition = enemyRef.transform.position;
            accuratePosition.y += enemyRef.navmesh.height;

            if (HeadInDirection(Vector3.forward, accuratePosition))
            {

            }else if(HeadInDirection(Vector3.left, accuratePosition))
            {

            }else if (HeadInDirection(Vector3.right, accuratePosition))
            {

            }else if(HeadInDirection(Vector3.back, accuratePosition))
            {

            }
        }
        else
        {
        }

        if (enemyRef.fieldOfView.CanSeePlayer() != ENEMY_FOV_STATES.NOT_IN_VIEW) enemyRef.TransitionToState(EnemyStates.AttackStatic);
        //Debug.Log("Remaining distance: " + enemyRef.navmesh.remainingDistance);
        //If disarmed and pickable weapon is on sight, pickup weapon.
        //If player is on sight, transition to attack.
    }

    private bool HeadInDirection(Vector3 _direction, Vector3 _startPosition)
    {
        RaycastHit hit;
        bool directionIsObstructed = Physics.Raycast(_startPosition, enemyRef.transform.TransformDirection(_direction), out hit, 3f, enemyRef.pathfinderObstacleMask);

        if (directionIsObstructed)
        {
            Debug.DrawRay(_startPosition, enemyRef.transform.TransformDirection(_direction) * hit.distance, Color.red);
            Debug.Log("Direction Obstructed");
            return false;
        }
        else
        {
            //Debug.Log("Right Available");
            bool destinationIsReachable = Physics.Raycast(_startPosition, enemyRef.transform.TransformDirection(_direction), out hit, 50f, enemyRef.pathfinderObstacleMask);
            Debug.Log(_direction+" destination available: " + destinationIsReachable);

            NavMeshHit hitMesh;
            if (destinationIsReachable)
            {
                NavMesh.SamplePosition(hit.point, out hitMesh, 30f, NavMesh.AllAreas);
                enemyRef.navmesh.SetDestination(hitMesh.position);
                Debug.DrawRay(hitMesh.position, Vector3.up, Color.blue, 1.0f);
                return true;
            }
            return false;
            //if (destinationIsReachable) enemyRef.navmesh.destination = hit.point;
        }
    }

    private bool HasReachedDestination()
    {
        if (!enemyRef.navmesh.pathPending)
        {
            if (enemyRef.navmesh.remainingDistance <= enemyRef.navmesh.stoppingDistance)
            {
                if (!enemyRef.navmesh.hasPath || enemyRef.navmesh.velocity.sqrMagnitude == 0f)
                {
                    //Done
                }
            }
        }
        return true;
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
}
