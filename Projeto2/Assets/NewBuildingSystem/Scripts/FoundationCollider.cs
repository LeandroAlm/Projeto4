using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundationCollider : MonoBehaviour {


    Foundation foundationScript;

    Vector3 sizeOfFoundation;


    void Start ()
    {
        foundationScript = transform.parent.parent.GetComponent<Foundation>();
        sizeOfFoundation = transform.parent.parent.GetComponent<Collider>().bounds.size;


    }
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    private void OnTriggerEnter(Collider other)
    {
        //Snapping
    //    if (BuildingManager.isBuilding && other.tag == "Foundation" && foundationScript.isPlaced && !other.GetComponent<Foundation>().isSnapped) // foundationScript.isPlaced
    //    {


    //        //Release Snapping
    //        Foundation foundation = other.GetComponent<Foundation>();
    //        foundation.isSnapped = true;
    //        foundation.mousePosX = Input.GetAxis("Mouse X");
    //        foundation.mousePosY = Input.GetAxis("Mouse Y");


    //        other.GetComponent<Foundation>().isSnapped = true;

    //        float sizeX = sizeOfFoundation.x;
    //        float sizeZ = sizeOfFoundation.z;

    //        if (BuildingManager.PreH)
    //        {
    //            switch (this.transform.tag)
    //            {
    //                case "WestCollider":
    //                    other.transform.position = new Vector3(transform.parent.parent.position.x - sizeX, 0, transform.parent.position.z);
    //                    break;

    //                case "EastCollider":
    //                    other.transform.position = new Vector3(transform.parent.parent.position.x + sizeX, 0, transform.parent.position.z);
    //                    break;

    //                case "NortCollider":
    //                    other.transform.position = new Vector3(transform.parent.parent.position.x, 0, transform.parent.position.z + sizeZ);
    //                    break;

    //                case "SouthCollider":
    //                    other.transform.position = new Vector3(transform.parent.parent.position.x, 0, transform.parent.position.z - sizeZ);
    //                    break;
    //            }

    //        }
    //        if (BuildingManager.PreV)
    //        {
    //            switch (this.transform.tag)
    //            {
    //                case "WestCollider":
    //                        other.transform.position = new Vector3(transform.parent.parent.position.x - (sizeX / 2), 1, transform.parent.position.z);
    //                        other.transform.eulerAngles = new Vector3(90, 0, -90);
    //                    break;

    //                case "EastCollider":
    //                        other.transform.position = new Vector3(transform.parent.parent.position.x + (sizeX / 2), 1, transform.parent.position.z);
    //                        other.transform.eulerAngles = new Vector3(-90, 0, 90);
    //                    break;

    //                case "NortCollider":
    //                    other.transform.position = new Vector3(transform.parent.parent.position.x, 1, transform.parent.position.z + (sizeZ / 2));
    //                    break;

    //                case "SouthCollider":
    //                    other.transform.position = new Vector3(transform.parent.parent.position.x, 1, transform.parent.position.z - (sizeZ / 2));
    //                    break;
    //            }
    //        }
    //    }

    }
}
