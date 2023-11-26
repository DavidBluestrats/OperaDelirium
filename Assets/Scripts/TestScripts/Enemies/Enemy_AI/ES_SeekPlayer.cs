using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ES_SeekPlayer : StateBase<EnemyStates>
{
    private float timeToStopSearching;
    private Vector2 seekPositionOffsetAngles = new Vector2(-5f, 5f);

    public Vector3 playerLastKnownLocation;
    public Enemy enemyRef;


    public ES_SeekPlayer(EnemyStates _key) : base(_key)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Started Seeking Player");
        Debug.Log("Player's last known location: "+playerLastKnownLocation);
        enemyRef = GetMainClassReference<Enemy>();
        enemyRef.navmesh.speed = 9f;
        enemyRef.navmesh.stoppingDistance = 0.5f;
        timeToStopSearching = Random.Range(1.5f, 3f);

        //The following offset position is to avoid enemies trying to reach the same location, and to get them to actually manage to
        //see the player by turning around the corner in which he disappeared.
        Vector3 playerLocationWithOffsets = GetRandomPositionWithDirection(playerLastKnownLocation, enemyRef.playerMover.GetPlayerDirection(), seekPositionOffsetAngles);
        enemyRef.navmesh.SetDestination(playerLocationWithOffsets);
    }

    public override void ExitState()
    {
        Debug.Log("Stopped Seeking");
    }

    public override void UpdateState()
    {
        ENEMY_FOV_STATES detectionState = enemyRef.fieldOfView.CanSeePlayer();
        if (detectionState == ENEMY_FOV_STATES.IN_VIEW || detectionState == ENEMY_FOV_STATES.IN_SHOOT_VIEW)
        {
            enemyRef.TransitionToState(EnemyStates.AttackStatic);
        }
        else
        {
            if (!enemyRef.navmesh.pathPending)
            {
                if (enemyRef.navmesh.remainingDistance <= enemyRef.navmesh.stoppingDistance)
                {
                    if (!enemyRef.navmesh.hasPath || enemyRef.navmesh.velocity.sqrMagnitude == 0f)
                    {
                        if (timeToStopSearching <= 0f)
                        {
                            enemyRef.TransitionToState(EnemyStates.PatrolRandom);

                        }
                        else timeToStopSearching -= Time.deltaTime;
                    }
                }
            }
        }
    }

    private Vector3 GetRandomPositionWithDirection(Vector3 _initialPosition, Vector3 _direction, Vector2 _anglesInDirection)
    {
        Vector3 position;
        int distanceOffset = Random.Range(1, 4);
        NavMeshHit hitMesh;

        Vector3 randomDirection = Quaternion.Euler(0f, 
            Random.Range(_anglesInDirection.x, _anglesInDirection.y), 
            Random.Range(_anglesInDirection.x, _anglesInDirection.y)) * _direction;

        position = _initialPosition + randomDirection * distanceOffset;

        NavMesh.SamplePosition(position, out hitMesh, 30f, NavMesh.AllAreas);
        Debug.DrawRay(hitMesh.position, Vector3.up*5f, Color.cyan, 1.0f);

        return hitMesh.position;
    }
}
