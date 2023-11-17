using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTopFollower : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject player;
    public static CameraTopFollower Ins;
    private Camera cam;
    public float zoomLookDistance = 11f;
    public float normalLookDistance = 4f;
    void Start()
    {
        Ins = this;
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
    }

    void MoveCamera()
    {
        Vector3 mousePositionInWorld = GetMousePositionInWorld();
        mousePositionInWorld.y = 0;
        Vector3 differenceOfPositions = mousePositionInWorld - cam.transform.position;
        Vector3 directionFromCamToMouse = differenceOfPositions.normalized;
        Vector3 cameraPosition = player.transform.position;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            cameraPosition += directionFromCamToMouse * zoomLookDistance;
        }
        else
        {
            cameraPosition += directionFromCamToMouse * normalLookDistance;

        }
        cameraPosition.y = player.transform.position.y + 15f;
        transform.position = Vector3.Lerp(transform.position, cameraPosition, 10f * Time.deltaTime);
    }

    public Vector3 GetMousePositionInWorld()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitPoint, 999f))
        {
            return hitPoint.point;
        }
        return new Vector3();
    }

    public Vector3 GetMiddlePointBetweenTwoPoints(Vector3 p1, Vector3 p2)
    {
        return new Vector3((p1.x + p2.x) / 2, 0f, (p1.z + p2.z)/2);
    }
    /*
    void OldMoveCamera()
    {
        
            //Vector3 mousePositionInWorld = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 mousePositionInWorld = GetMousePositionInWorld();
            Vector3 middlePoint = GetMiddlePointBetweenTwoPoints(mousePositionInWorld, player.transform.position);
            Vector3 clampedPosition = new Vector3(Mathf.Clamp(middlePoint.x, player.transform.position.x - camXBounds, camXBounds + player.transform.position.x)
                , 0f,
                Mathf.Clamp(middlePoint.z, player.transform.position.z - camZBounds, camZBounds + player.transform.position.z));
            transform.position = new Vector3(clampedPosition.x, player.transform.position.y + 13f, clampedPosition.z);
            
    }
    */
}
