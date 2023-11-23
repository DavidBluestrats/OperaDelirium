using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 Opposite to the normal Patrol, PatrolRand will select random waypoints
 for the enemy to patrol around.
 */
public class ES_PatrolRand : StateBase<EnemyStates>
{
    private Enemy enemyRef;

    private float secondsTilNextPath = 0f;

    public ES_PatrolRand(EnemyStates _key) : base(_key)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Patrolling Randomly.");
        enemyRef = GetMainClassReference<Enemy>();
        enemyRef.navmesh.stoppingDistance = 0.5f;
    }

    public override void ExitState()
    {
        Debug.Log("Stopped Patrolling randomly.");
    }

    public override void UpdateState()
    {
        
        if (!enemyRef.navmesh.hasPath  || enemyRef.navmesh.remainingDistance <= enemyRef.navmesh.stoppingDistance)
        {
            //Debug.Log("Has Path: " + enemyRef.navmesh.hasPath);
            Vector3 accuratePosition = enemyRef.transform.position;
            accuratePosition.y += enemyRef.navmesh.height;

            if (enemyRef.navmesh.remainingDistance <= enemyRef.navmesh.stoppingDistance && secondsTilNextPath <= Time.time)
            {
                Debug.Log("Moving Somewhere else.");
                float randSecs = Random.Range(2f, 5f);
                float timeToReachNewDestination;

                RandomPoint(accuratePosition, 9f, out Vector3 result);
                //enemyRef.navmesh.SetDestination(result);
                
                NavMeshPath newPath = new NavMeshPath();
                NavMesh.CalculatePath(accuratePosition,result,NavMesh.AllAreas, newPath);
                enemyRef.navmesh.SetPath(newPath);
                timeToReachNewDestination = GetPathLength(newPath) / enemyRef.navmesh.speed;

                secondsTilNextPath = Time.time + randSecs + timeToReachNewDestination;
                Debug.Log("1. Seconds till next path: " + randSecs+ " Distance to destination: "+ timeToReachNewDestination);
            }
        }

        if (enemyRef.fieldOfView.CanSeePlayer() != ENEMY_FOV_STATES.NOT_IN_VIEW) enemyRef.TransitionToState(EnemyStates.AttackStatic);

    }

    public float GetPathLength(NavMeshPath _path)
    {
        //Debug.Log("Path corners: " + _path.corners.Length);
        if (_path.corners.Length < 2)
        {
            return Vector3.Distance(enemyRef.transform.position, _path.corners[0]);
        }
        
        float totalDistance = 0f;
        int i = 1;
        Vector3 previousNode = _path.corners[0];

        while (i<_path.corners.Length)
        {
            Vector3 currentNode = _path.corners[i];
            totalDistance += Vector3.Distance(previousNode, currentNode);
            Debug.DrawLine(currentNode, previousNode, Color.red,2f);
            previousNode = currentNode;
            i++;
        }
        return totalDistance;
        
    }

    bool RandomPoint(Vector3 _center, float _range, out Vector3 _result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = _center + Random.insideUnitSphere * _range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                _result = hit.position;
                return true;
            }
        }
        _result = Vector3.zero;
        return false;
    }
}
