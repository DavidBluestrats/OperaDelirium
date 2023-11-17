using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCollision : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(gameObject.name + "collided with "+collision.gameObject.name);
    }
}
