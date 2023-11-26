using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ES_SeekPlayerAround : StateBase<EnemyStates>
{
    private int milestonesToStopSearching;
    private Vector2 seekPositionOffsetAngles = new Vector2(-90f, 90f);
    private float rotationSpeed = 6f;
    private float shootReactionTimeInSecs = 0.25f;
    private Vector3 nextMilestone;

    public Vector3 playerLastKnownLocation;
    public Enemy enemyRef;
    public Vector3 directionToLookAt = new Vector3();

    public ES_SeekPlayerAround(EnemyStates _key) : base(_key)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Started Seeking AROUND.");
        enemyRef = GetMainClassReference<Enemy>();
        enemyRef.navmesh.updateRotation = false;
        enemyRef.navmesh.stoppingDistance = 0.5f;
        enemyRef.navmesh.speed = 7f;

        milestonesToStopSearching = UnityEngine.Random.Range(5,10);
        playerLastKnownLocation = enemyRef.playerTransform.position;

        Debug.Log("First milestones limit: " + milestonesToStopSearching);
        /*
        var ellipsePoints = GetEllipsePoints(10, 6, 50, playerLastKnownLocation,-45f);
        foreach (var point in ellipsePoints)
        {
            Debug.DrawRay(point, Vector3.up * 5f, Color.red, 10.0f);

        }
        */
    }

    public override void ExitState()
    {
        Debug.Log("Stopped Seeking AROUND.");
        enemyRef.navmesh.updateRotation = true;
    }

    public override void UpdateState()
    {
        /*
         * Differently from ES_SeekPlayer, this type of Seek behaviour is based around a last known location.
         * The AI will attempt to travel to the last known location in angle milestone intervals.
         * These intervals are determined by a random location within an angle that is relative to the
         * direction between the AI and the last known location.
         * This way the AI will not directly travel towards the last known location, but instead, will try to reach
         * it by traveling to several other points.
         * 
         * At all times the AI has to be looking at the last known location. If the AI has traveled to a certain
         * number of milestones in between, without actually getting a visual on the player, it'll revert to another form
         * of patrol. However, if the AI actually sees the player while travelling the milestones, it'll update its last
         * known location and reset the number of milestones it needs to reach before assuming another patrol state.
         * 
        */
        
        //Next Milestone Logic
        if (!enemyRef.navmesh.hasPath || enemyRef.navmesh.remainingDistance <= enemyRef.navmesh.stoppingDistance)
        {
            if (enemyRef.navmesh.remainingDistance <= enemyRef.navmesh.stoppingDistance)
            {
                if (milestonesToStopSearching <= 0)
                {
                    //Switch to another patrol state.
                    enemyRef.TransitionToState(EnemyStates.PatrolRandom);
                }
                else
                {
                    nextMilestone = GetRandomPositionWithinBoundsAndDirection(playerLastKnownLocation, seekPositionOffsetAngles);
                    enemyRef.navmesh.SetDestination(nextMilestone);
                    milestonesToStopSearching--;
                    Debug.Log("Moving to new Milestone. Counter: " + milestonesToStopSearching);
                }
            }
        }

        //Player detection logic
        ENEMY_FOV_STATES detectionState = enemyRef.fieldOfView.CanSeePlayer();
        if (detectionState == ENEMY_FOV_STATES.IN_VIEW || detectionState == ENEMY_FOV_STATES.IN_SHOOT_VIEW)
        {
            directionToLookAt = enemyRef.playerTransform.position - enemyRef.transform.position;

            playerLastKnownLocation = enemyRef.playerTransform.position;
            milestonesToStopSearching = milestonesToStopSearching = UnityEngine.Random.Range(4, 9);

            if (detectionState == ENEMY_FOV_STATES.IN_SHOOT_VIEW)
            {
                if (shootReactionTimeInSecs <= 0f)
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
            directionToLookAt = playerLastKnownLocation - enemyRef.transform.position;
        }

        Quaternion lookRotation = Quaternion.LookRotation(directionToLookAt);
        lookRotation.x = 0; lookRotation.z = 0;
        enemyRef.transform.rotation = Quaternion.Lerp(enemyRef.transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        
    }

    private Vector3 GetRandomPositionWithinBoundsAndDirection(Vector3 _lastKnownLocation, Vector2 _anglesInDirection)
    {
        Vector3 nextMilestone = new Vector3();
        Vector3 directionTowardsLastLocation;
        float distanceOffset;
        NavMeshHit hitMesh;


        for (int i = 0; i < 30; i++)
        {
            distanceOffset = UnityEngine.Random.Range(3, 8);
            directionTowardsLastLocation = (_lastKnownLocation - enemyRef.transform.position).normalized;

            Vector3 randomDirection = Quaternion.Euler(0f, UnityEngine.Random.Range(_anglesInDirection.x, _anglesInDirection.y), 0f) * directionTowardsLastLocation;
            nextMilestone = _lastKnownLocation + randomDirection * distanceOffset;

            if (NavMesh.SamplePosition(nextMilestone, out hitMesh, 30f, NavMesh.AllAreas))
            {
                nextMilestone = hitMesh.position;
                break;
            }
        }
        Debug.DrawRay(nextMilestone, Vector3.up * 5f, Color.red, 3.0f);

        return nextMilestone;
    }

    Vector3[] GetEllipsePoints(float a, float b, int numPoints, Vector3 center, float angle)
    {
        Vector3[] points = new Vector3[numPoints];

        Quaternion rotation = Quaternion.Euler(0f, angle, 0f);

        for (int i = 0; i < numPoints; i++)
        {
            float theta = 2f * Mathf.PI * i / numPoints;
            float x = a * Mathf.Cos(theta);
            float z = b * Mathf.Sin(theta);
            points[i] = center + rotation * new Vector3(x, 0f, z);
        }

        return points;
    }
}
