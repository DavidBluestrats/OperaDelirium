using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mover : MonoBehaviour
{
    float moveSpeed = 10f;
    private Vector3 direction;
    private Vector3 currentPosition;
    private Vector3 previousPosition;
    [SerializeField] private CameraTopFollower worldCam;

    // Start is called before the first frame update
    void Start()
    {
        currentPosition = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        PlayerRotation();

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }
    }

    void PlayerMovement()
    {
        previousPosition = currentPosition;
        float xValue = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float zValue = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        currentPosition = new Vector3(xValue, currentPosition.y, zValue);

        direction = (currentPosition - previousPosition).normalized;

        transform.Translate(xValue, 0, zValue, Space.World);
    }

    void PlayerRotation()
    {
        Vector3 mousePositionInWorld = worldCam.GetMousePositionInWorld();
        mousePositionInWorld.y = transform.position.y;
        transform.LookAt(mousePositionInWorld,transform.up);
    }

    public Vector3 GetPlayerDirection()
    {
        return direction;
    }
}
