using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    float mouseX, mouseY;
    public float mouseSpeedX = 300f;
    public float mouseSpeedY = 300f;
    float xRotation, yRotation;
    float tilt = 0f;
    public float camTilt;
    public float camTiltTimeStart;
    public float camTiltTimeReturn;
    public Transform playerBody;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSpeedX * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSpeedY * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        yRotation += mouseX;
        CameraTiltFromMovement();
        playerBody.localRotation = Quaternion.Euler(0f, yRotation, 0f);
        //transform.localRotation = Quaternion.Euler(xRotation,0f, 0f);
        //transform.localRotation = Quaternion.RotateTowards(transform.localRotation,Quaternion.Euler(0f, 0f, tilt),1f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, tilt);
    }
    void CameraTiltFromMovement()
    {
        //float walkDirectionInput = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.A))
        {
            tilt = Mathf.Lerp(tilt, camTilt, camTiltTimeStart * Time.deltaTime);
        }else if (Input.GetKey(KeyCode.D)) {
            tilt = Mathf.Lerp(tilt, -camTilt, camTiltTimeStart * Time.deltaTime);
        }else
        {
            tilt = Mathf.Lerp(tilt, 0, camTiltTimeReturn * Time.deltaTime);
        }

    }

    void Tests()
    {
        
    }
   

}
