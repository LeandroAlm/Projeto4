using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofFoundation : MonoBehaviour {


    public bool isPlaced;
    public bool isSnapped;

    public float mousePosX;
    public float mousePosY;

    public Vector3 roofPos;


    void Start () {
		
	}
	
	void Update ()
    {
        if (!isPlaced && !isSnapped)
        {
            BuildingManager.isBuilding = true;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
               
                this.transform.position = new Vector3(hit.point.x, 0, hit.point.z);
            }
        }


        if (Input.GetMouseButtonDown(0))
        {

            isPlaced = true;
            BuildingManager.isBuilding = false;
        }


        //Realese Snapping
        if (isSnapped && !isPlaced && Mathf.Abs(mousePosX - Input.GetAxis("Mouse X")) > 0.2f || Mathf.Abs(mousePosY - Input.GetAxis("Mouse Y")) > 0.2f)
        {
            isSnapped = false;
        }
    }
}
