using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {

    public GameObject foundationPrefab;
    public GameObject foundationPrefab2;

    public static bool isBuilding;

    public static bool PreH, PreV;

   


    void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Alpha1) && !isBuilding)
        {
            PreH = true;
            PreV = false;
            isBuilding = true;
            Instantiate(foundationPrefab, Vector3.zero, foundationPrefab.transform.rotation);
           

        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && !isBuilding)
        {
            PreV = true;
            PreH = false;
            isBuilding = true;
            Instantiate(foundationPrefab2, Vector3.zero, foundationPrefab2.transform.rotation);

        }

    }
}
