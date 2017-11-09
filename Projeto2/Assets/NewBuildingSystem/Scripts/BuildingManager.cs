using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {

    public GameObject foundationPrefab;
    public GameObject foundationPrefab2;

    public static bool isBuilding;

    public static bool PreH, PreV;

    //public Camera mainCamera;



    void Update ()
    {
      


        if (Input.GetKeyDown(KeyCode.Alpha1) && !isBuilding)
        {
            PreH = true;
            PreV = false;
            isBuilding = true;
            Instantiate(foundationPrefab, Vector3.zero, foundationPrefab.transform.rotation);


            //mainCamera.transform.position =  Vector3.Lerp(new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y , mainCamera.transform.position.z ),  new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y + 6.0f, mainCamera.transform.position.z - 4.0f), Time.deltaTime / 2);
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
