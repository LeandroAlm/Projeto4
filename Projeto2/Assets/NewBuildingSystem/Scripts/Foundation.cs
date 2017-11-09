using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foundation : MonoBehaviour {

    public bool isPlaced;
    public bool isSnapped;

    public float mousePosX;
    public float mousePosY;

    //public Collider col1;
    //public Collider col2;
    //public Collider col3;
    //public Collider col4;

    public Quaternion InitialRot;



    void Update ()
    {
        //enquanto nao estiver posiciona segue o rato
		if(!isPlaced && !isSnapped)
        {
            BuildingManager.isBuilding = true;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if(BuildingManager.PreH)
                    this.transform.position = new Vector3(hit.point.x, 0, hit.point.z);

                if (BuildingManager.PreV)
                {
                    this.transform.position = new Vector3(hit.point.x, 1.5f, hit.point.z +0.5f);
                    InitialRot = this.transform.rotation;
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.H))
        {
            Time.timeScale = 0.0f;
        }

        if(Input.GetMouseButtonDown(0))
        {

            isPlaced = true;
            BuildingManager.isBuilding = false;
        }
       
        if(isPlaced && BuildingManager.PreV)
        {
            //col1.enabled = false;
            //col2.enabled = false;
            //col3.enabled = false;
            //col1.enabled = false;

        }
       
        //Realese Snapping
        if (isSnapped && !isPlaced && Mathf.Abs(mousePosX - Input.GetAxis("Mouse X")) > 0.2f || Mathf.Abs(mousePosY - Input.GetAxis("Mouse Y")) > 0.2f)
        {
            isSnapped = false;
        }

        if(!isSnapped && !isPlaced)
        {
            this.transform.rotation = InitialRot;

        }
    }
}
