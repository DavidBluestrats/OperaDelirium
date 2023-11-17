using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTools : MonoBehaviour
{
    // Start is called before the first frame update
    MouseLook mouseSettings;
    void Start()
    {
        mouseSettings = GetComponent<MouseLook>();
    }
    // Update is called once per frame
    void Update()
    {
        ChangeMouseSensitivity();
    }
    void ChangeMouseSensitivity()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            mouseSettings.mouseSpeedX += 100f;
        }else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            mouseSettings.mouseSpeedX -= 100f;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            mouseSettings.mouseSpeedY += 100f;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            mouseSettings.mouseSpeedY -= 100f;
        }
    }
}
