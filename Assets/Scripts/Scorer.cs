using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorer : MonoBehaviour
{
    // Start is called before the first frame update
    private int numberOfCollisions = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Hit"){
            numberOfCollisions++;
            Debug.Log("You've bumped into something " + numberOfCollisions + " times.");
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            collision.gameObject.tag = "Hit";
        }
        
    }
    private void OnCollisionExit(Collision collision)
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
