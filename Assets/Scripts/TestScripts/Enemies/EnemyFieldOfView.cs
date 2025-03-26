using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ENEMY_FOV_STATES { IN_VIEW, IN_SHOOT_VIEW, NOT_IN_VIEW};
public class EnemyFieldOfView : MonoBehaviour
{
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngleHorizontalGeneral;
    [Range(0, 360)]
    public float viewAngleHorizontalShootAngle;
    [Range(0, 360)]
    public float viewAngleVertical;
    [Range(0, 360)]
    public float viewAnglePeripheralVertical;

    public Transform player;
    //DetectionManager detectionManager;
    public LayerMask obstMask;

    public ENEMY_FOV_STATES CanSeePlayer()
    {
        ENEMY_FOV_STATES state = ENEMY_FOV_STATES.NOT_IN_VIEW;

        if (Vector3.Distance(transform.position, player.position) < viewRadius)
        {
            Vector3 distToTarget = (player.position - transform.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, distToTarget);
            float distance = Vector3.Distance(player.transform.position, transform.position);

            if (angleBetweenGuardAndPlayer < viewAngleHorizontalGeneral / 2 /* && angleBetweenGuardAndPlayer < viewAngleVertical / 2*/)
            {
                Debug.DrawRay(transform.position, distToTarget * distance, Color.yellow);
                if (!Physics.Raycast(transform.position, distToTarget, distance, obstMask))
                {
                    //Debug.Log("Player in main vision");
                    state = ENEMY_FOV_STATES.IN_VIEW;
                }
            }

            if (angleBetweenGuardAndPlayer < viewAngleHorizontalShootAngle / 2 /* && angleBetweenGuardAndPlayer < viewAngleVertical / 2*/)
            {
                Debug.DrawRay(transform.position, distToTarget * distance, Color.red);
                if (!Physics.Raycast(transform.position, distToTarget, distance, obstMask))
                {
                    //Debug.Log("Player in shooting vision");
                    state = ENEMY_FOV_STATES.IN_SHOOT_VIEW;
                }
            }
        }
        return state;
    }

    public bool CanSeeWeapon(Gun _weaponToTest)
    {
        Vector3 distToTarget = (_weaponToTest.transform.position - transform.position).normalized;
        float distance = Vector3.Distance(_weaponToTest.transform.position, transform.position);
        return !Physics.Raycast(transform.position, distToTarget, distance, obstMask);
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public Vector3 DirFromAngleVertical(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees -= transform.eulerAngles.x;
        }
        return new Vector3(0, Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

}