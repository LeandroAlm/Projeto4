using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDrag : MonoBehaviour {

    bool isPlaced;
    bool isSnapped;
    bool isDragging;


    //public GameObject wall;

    public float mousePosX;
    public float mousePosY;

    void Start () {
		
	}
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
        
            Instantiate(this.gameObject, Vector3.zero, this.transform.rotation);

        }
        if (!isPlaced && !isSnapped)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                this.transform.position = new Vector3(hit.point.x, 0f, hit.point.z + 0.5f);

            }
        }


        if (Input.GetMouseButton(0))
        {

            isPlaced = true;

            //firstWall.position = this.transform.position;


            if (Input.GetKey(KeyCode.LeftShift) && Mathf.Abs(mousePosX - Input.GetAxis("Mouse X")) > 0.3f || Mathf.Abs(mousePosY - Input.GetAxis("Mouse Y")) > 0.3f)
            {
                isDragging = true; 
                Debug.Log("SIM");


                //if (isSnapped && !isPlaced && Mathf.Abs(mousePosX - Input.GetAxis("Mouse X")) > 0.3f || Mathf.Abs(mousePosY - Input.GetAxis("Mouse Y")) > 0.3f)
                //{
                //}
            }
            else
            {
                isDragging = false;
            }
        }

        if(isDragging)
        {
            Instantiate(this.gameObject, this.transform.position + new Vector3(2.74f, 0, 0), this.transform.rotation);

        }

    }
}
