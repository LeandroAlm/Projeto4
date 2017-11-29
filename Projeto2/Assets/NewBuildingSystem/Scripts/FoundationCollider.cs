using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundationCollider : MonoBehaviour {


    Foundation foundationScript;

    Vector3 sizeOfFoundation;

    BuildWall2 buildWallScript;

    Vector3 Pos;

    public GameObject player;

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        foundationScript = transform.parent.GetComponent<Foundation>();
        //sizeOfFoundation = transform.parent.parent.GetComponent<Collider>().bounds.size;
        buildWallScript = player.GetComponent<BuildWall2>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    private void OnTriggerEnter(Collider other)
    {
        //Snapping

        //if(other.tag == "Foundation")

        //if (BuildingManager.isBuilding && other.tag == "Foundation" && foundationScript.isPlaced && !other.GetComponent<Foundation>().isSnapped)

        if (other.tag == "Foundation" )
        {
            Foundation.isPlaced = true;
            //Release Snapping
            //Foundation foundation = other.GetComponent<Foundation>();
            //foundation.isSnapped = true;
            //foundation.mousePosX = Input.GetAxis("Mouse X");
            //foundation.mousePosY = Input.GetAxis("Mouse Y");




            //other.GetComponent<Foundation>().isSnapped = true;

            float sizeX = 1;
            float sizeZ = 1;

           
            switch (this.transform.tag)
            {
                case "LeftCollider":
                    other.transform.rotation = buildWallScript.lastDir;
                    other.transform.position = new Vector3(buildWallScript._lastDir.x, 0, buildWallScript._lastDir.z);

                    break;

                case "RightCollider":
                    other.transform.rotation = buildWallScript.lastDir;
                    other.transform.position = new Vector3(buildWallScript._lastDir.x, 0, buildWallScript._lastDir.z);

                    break;

                
            }

            
        
        }

    }
}
