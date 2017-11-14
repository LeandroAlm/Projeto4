using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCam : MonoBehaviour {

    public Transform target;
    public float cameraSpeed = 15;
    public float zOffset = 4;
    public bool smoothFollow = true;

	
	
	void Update ()
    {
		
        if(target)
        {
            if(FadeOutRoof.isInside)
            {
                Vector3 newPos2 = transform.position;
                newPos2.x = target.position.x;
                newPos2.z = target.position.z + 3.5f;
                Quaternion newRot = Quaternion.Euler(77, 0, 0);


                transform.position = Vector3.Lerp(transform.position, newPos2, cameraSpeed * Time.deltaTime);

                transform.rotation = Quaternion.Slerp(transform.rotation, newRot, Time.deltaTime * 2.0f);

            }

            Vector3 newPos = transform.position;
            newPos.x = target.position.x;
            newPos.z = target.position.z - 5.5f;

            transform.rotation = Quaternion.Euler(61.4f, 0, 0);


          


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
