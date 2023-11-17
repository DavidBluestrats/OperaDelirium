using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 15f;

    public float turnSmoothTime = 0.1f;
    public Transform playerBody;


    //Jump Stuff
    Vector3 velocity;
    public float gravity = -9.8f;
    public Transform groundCheck;
    public float groundDist;
    public LayerMask groundMask;
    bool isGrounded;
    public float jumpHeight = 3;
    public int availableJumps = 2;

    //Dash & Movement
    Vector3 moveDirection;
    public Vector3 horizontalDir;
    bool isDashing;
    public float dashesAvailable = 3f;
    float dashStartTime;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(h, 0f, v).normalized;
        
        moveDirection = transform.right * h + transform.forward*v;
        controller.Move(moveDirection*speed*Time.deltaTime);

        HandleJump();
        HandleDash();
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleJump()
    {
        //Jump
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDist, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            //Debug.Log("Velocity on Y: " + velocity.y);
            velocity.y = -1f;
            availableJumps = 2;
        }

        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || availableJumps > 0))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            availableJumps--;
        }
    }
    void HandleDash()
    {
        bool isTryingToDash = Input.GetKeyDown(KeyCode.LeftShift);
        if (isTryingToDash)
        {
            if ((dashesAvailable - 1f) >= 0f)
            {
                Debug.Log("Will dash");
                OnStartDash();
            }         
        }
        if (isDashing)
        {
                if (Time.time - dashStartTime <= 0.25f) {
                    if (horizontalDir.Equals( Vector3.zero))
                    {
                        controller.Move(transform.forward * 20f * Time.deltaTime);
                    }
                    else
                    {
                        controller.Move(horizontalDir.normalized * 20f * Time.deltaTime);
                    }
                }
                else
                {
                    OnEndDash();
                }
        }
            
    }
    void OnStartDash()
    {
        isDashing = true;
        velocity.y = 0f;
        horizontalDir = moveDirection;
        //Debug.Log("Direction: (" + horizontalDir.x + "," + horizontalDir.y + "," + horizontalDir.z + ")");
        dashesAvailable -= 1f;
        dashStartTime = Time.time;
    }
    void OnEndDash()
    {
        isDashing = false;
        horizontalDir = Vector3.zero;
        dashStartTime = 0f;
    }
}
