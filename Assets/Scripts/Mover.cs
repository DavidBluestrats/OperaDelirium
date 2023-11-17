using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    float moveSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        printInstructionsToConsole();
    }
    // Update is called once per frame
    void Update()
    {
        playerMovement();
        playerJump();
    }

    void printInstructionsToConsole()
    {
        Debug.Log("Welcome to the game.");
        Debug.Log("Use WASD to move around.");
        Debug.Log("Have fun gamer.");
    }

    void playerMovement()
    {
        float xValue = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float zValue = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        transform.Translate(xValue, 0, zValue);
    }
    void playerJump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * 600f * Time.deltaTime);
        }
    }
}
