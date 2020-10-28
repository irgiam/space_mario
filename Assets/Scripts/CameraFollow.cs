using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance;
    Vector3 cameraStartingPosition;

    public Vector2 offset = new Vector2(0.1f, 1f);

    public float dampTime = 0.3f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;


    void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;
        cameraStartingPosition = this.transform.position;
    }

    public void CameraStartingPosition()
    {
        this.transform.position = cameraStartingPosition;
    }


    public void ResetToStartPosition()
    {
        Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
        Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(offset.x, offset.y, point.z));
        Vector3 destination = transform.position + delta;

        destination = new Vector3(destination.x, offset.y, destination.z);
        transform.position = destination;
    }


    void Update()
    {

        Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
        Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(offset.x, offset.y, point.z));
        Vector3 destination = transform.position + delta;

        destination = new Vector3(destination.x, offset.y, destination.z);

        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);


    }
}
