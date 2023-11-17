using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    public string gunName;
    public string type;

    protected virtual void Init()
    {

    }

    public abstract void Shoot(Vector3 _shootPosition, Vector3 _targetDirection);
}
