using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    // Start is called before the first frame update
    // Update is called once per frame
    [SerializeField] private float yRotation = 0.15f;
    void Update()
    {
        transform.Rotate(0,yRotation,0);
    }
}
