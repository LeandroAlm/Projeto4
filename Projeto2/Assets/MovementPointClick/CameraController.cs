using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public Vector3 offset;

    public float zoomSpeed = 4f;
    public float minZoom = 5f;
    public float maxZoom = 15f;

    public float pitch = 2f;

    public float yawSpeed = 75f;

    private float currentZoom = 10f;
    private float currentYaw = 0f;
    private float currentPitch = 0f;

    float q = 0;

	
	void Update ()
	{

        if(Input.GetKeyDown(KeyCode.E))
        {
            q = q + 0.5f;
        }
        else if(Input.GetKeyUp(KeyCode.E))
        {
            q = 0;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            q = q - 0.5f;
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            q = 0;
        }

        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
	    currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

	    currentYaw -= q * yawSpeed * Time.deltaTime;
	    currentPitch -= Input.GetAxis("Vertical") * yawSpeed * Time.deltaTime;

	    currentPitch = Mathf.Clamp(currentPitch, -25, 40);


        transform.position = target.position - offset * currentZoom;
        transform.LookAt(target.position + Vector3.up * pitch);

        transform.RotateAround(target.position, Vector3.up, currentYaw);
        //transform.RotateAround(target.position, Vector3.right, currentPitch);
	}
}
