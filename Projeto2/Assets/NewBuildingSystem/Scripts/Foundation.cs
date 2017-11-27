using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foundation : MonoBehaviour
{

    public bool isPlaced;
    public bool isSnapped;

    public float mousePosX;
    public float mousePosY;

    //public Collider col1;
    //public Collider col2;
    //public Collider col3;
    //public Collider col4;

    public Quaternion InitialRot;

    public static bool isInstantiated;

    void Update()
    {
        //enquanto nao estiver posiciona segue o rato
        if (!isPlaced && !isSnapped)
        {
            BuildingManager.isBuilding = true;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                this.transform.position = new Vector3(hit.point.x, 0, hit.point.z);//hit.point.z +0.5f
                                                                                   //InitialRot = this.transform.rotation;


            }
        }


        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {

            this.transform.Rotate(Vector3.up * 250 * Time.deltaTime, Space.World);
        }

        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {

            this.transform.Rotate(Vector3.up * -250 * Time.deltaTime, Space.World);
        }

        if (Input.GetMouseButtonDown(0))
        {

            isPlaced = true;
            BuildingManager.isBuilding = false;
        }


        //Realese Snapping
        if (isSnapped && !isPlaced && Mathf.Abs(Input.GetAxis("Mouse X")) > 0.3f || Mathf.Abs(Input.GetAxis("Mouse Y")) > 0.3f)
        {
            isSnapped = false;
        }

        //if(!isSnapped && !isPlaced)
        //{
        //    this.transform.rotation = InitialRot;

        //}
    }
}
