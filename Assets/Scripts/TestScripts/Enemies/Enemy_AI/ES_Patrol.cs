using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    }

    public override void ExitState()
    {
        Debug.Log("Exit Patrol State");

    }

    public override void UpdateState()
    {
        if (!enemyRef.navmesh.hasPath)
        {
            RaycastHit hit;
            Vector3 accuratePosition = enemyRef.transform.position;
            accuratePosition.y += enemyRef.navmesh.height;

            bool forwardIsObstructed = Physics.Raycast(accuratePosition, enemyRef.transform.TransformDirection(Vector3.forward), out hit, 3f, enemyRef.pathfinderObstacleMask);

            if (forwardIsObstructed)
            {
                Debug.Log("ForwardObstructed");
            }
            else
            {
                bool destinationIsReachable = Physics.Raycast(accuratePosition, enemyRef.transform.TransformDirection(Vector3.forward), out hit, 50f, enemyRef.pathfinderObstacleMask);
                Debug.DrawRay(accuratePosition, enemyRef.transform.TransformDirection(Vector3.forward) * hit.distance, Color.red, 3f);
                Debug.Log("Forward Destination available: " + hit.point);

                NavMeshHit hitMesh;
                if (destinationIsReachable)
                {
                    NavMesh.SamplePosition(hit.point, out hitMesh, 30f, NavMesh.AllAreas);
                    Debug.Log("Destination found: " + hitMesh.position);
                    enemyRef.navmesh.SetDestination(hitMesh.position);
                    Debug.DrawRay(hitMesh.position, Vector3.up, Color.blue, 1.0f);
                    return;
                }
            }

            bool leftIsObstructed = Physics.Raycast(accuratePosition, enemyRef.transform.TransformDirection(Vector3.left), out hit, 3f, enemyRef.pathfinderObstacleMask);
            
            if (leftIsObstructed)
            {
                Debug.Log("LeftObstructed");
            }
            else
            {
                //Debug.Log("Left Available");
                bool destinationIsReachable = Physics.Raycast(accuratePosition, enemyRef.transform.TransformDirection(Vector3.left), out hit, 50f, enemyRef.pathfinderObstacleMask);
                Debug.DrawRay(accuratePosition, enemyRef.transform.TransformDirection(Vector3.left) * hit.distance, Color.red, 3f);
                Debug.Log("Left Destination available: "+hit.point);

                NavMeshHit hitMesh;
                if (destinationIsReachable)
                {
                    NavMesh.SamplePosition(hit.point, out hitMesh, 30f, NavMesh.AllAreas);
                    Debug.Log("Destination found: "+hitMesh.position);
                    enemyRef.navmesh.SetDestination(hitMesh.position);
                    Debug.DrawRay(hitMesh.position, Vector3.up, Color.blue, 1.0f);
                    return;
                }
                //if (destinationIsReachable) enemyRef.navmesh.destination = hit.point;
            }

            bool rightIsObstructed = Physics.Raycast(accuratePosition, enemyRef.transform.TransformDirection(Vector3.right), out hit, 3f, enemyRef.pathfinderObstacleMask);

            if (rightIsObstructed)
            {
                Debug.DrawRay(accuratePosition, enemyRef.transform.TransformDirection(Vector3.right) * hit.distance, Color.red);
                Debug.Log("RightObstructed");
            }
            else
            {
                //Debug.Log("Right Available");
                bool destinationIsReachable = Physics.Raycast(accuratePosition, enemyRef.transform.TransformDirection(Vector3.right), out hit, 50f, enemyRef.pathfinderObstacleMask);
                Debug.Log("Right Destination available: " + destinationIsReachable);

                NavMeshHit hitMesh;
                if (destinationIsReachable)
                {
                    NavMesh.SamplePosition(hit.point, out hitMesh, 30f, NavMesh.AllAreas);
                    enemyRef.navmesh.SetDestination(hitMesh.position);
                    Debug.DrawRay(hitMesh.position, Vector3.up, Color.blue, 1.0f);
                    return;
                }
                //if (destinationIsReachable) enemyRef.navmesh.destination = hit.point;
            }

            //bool backIsObstructed = Physics.Raycast(enemyRef.transform.position, enemyRef.transform.TransformDirection(-Vector3.forward), out hit, 3f, 7);

        }
        /*
         if(checkIfPlayerIsOnSight)
         */
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
