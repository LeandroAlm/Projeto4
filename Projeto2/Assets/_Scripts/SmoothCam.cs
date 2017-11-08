using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCam : MonoBehaviour {

    public Transform target;
    public float cameraSpeed = 15;
    public float zOffset = 10;
    public bool smoothFollow = true;

	
	
	void Update ()
    {
		
        if(target)
        {
            Vector3 newPos = transform.position;
            newPos.x = target.position.x;
            newPos.z = target.position.z - zOffset;

            if (!smoothFollow)
            {
                transform.position = newPos;
            }

            else
            {
                transform.position = Vector3.Lerp(transform.position, newPos, cameraSpeed * Time.deltaTime);
            }
                
        }
	}
}
