using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHit : MonoBehaviour
{
    private Color originalColor;
    // Start is called before the first frame update
    private void Start()
    {
        originalColor = GetComponent<MeshRenderer>().material.GetColor("_Color");
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponent<MeshRenderer>().material.color = Color.yellow;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponent<MeshRenderer>().material.color = originalColor;
        }
    }
}
