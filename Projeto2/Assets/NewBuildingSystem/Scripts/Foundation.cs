using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foundation : MonoBehaviour
{

    public static bool isPlaced;
    public bool isSnapped;

    public float mousePosX;
    public float mousePosY;

    //public Collider col1;
    //public Collider col2;
    //public Collider col3;
    //public Collider col4;

    public Quaternion InitialRot;

    public static bool isInstantiated;

    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
       

        //enquanto nao estiver posiciona segue o rato
        if (!isPlaced && !isSnapped && this.transform.tag == "Foundation")
        {
            BuildingManager.isBuilding = true;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                /*this.transform.position = new Vector3(hit.point.x, 0, hit.point.z + 0.5f);*///hit.point.z
                                                                                   //InitialRot = this.transform.rotation;
                this.transform.parent.position = new Vector3(hit.point.x + 1.5f, 0, hit.point.z - 0.5f);
                //transform.LookAt(player.transform.position);
            }
        }


        //if (Input.GetMouseButtonDown(0))
        //{

        //    isPlaced = true;
        //}


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
