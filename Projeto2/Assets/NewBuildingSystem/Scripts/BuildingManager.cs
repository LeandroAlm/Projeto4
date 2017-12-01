using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public GameObject foundationPrefab;
    public GameObject foundationPrefab2;
    public GameObject foundationPrefab3;
    public GameObject foundationPrefab4;

    public static bool buildHouse = false;
    public static bool buildTower = false;

    public static bool isBuilding;

    public static bool PreH, PreV;

    void Update ()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha4) && !isBuilding)
        //{
        //    PreH = true;
        //    PreV = false;
        //    isBuilding = true;
        //    Instantiate(foundationPrefab, Vector3.zero, foundationPrefab.transform.rotation);
            
        //    //mainCamera.transform.position =  Vector3.Lerp(new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y , mainCamera.transform.position.z ),  new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y + 6.0f, mainCamera.transform.position.z - 4.0f), Time.deltaTime / 2);
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha5) && !isBuilding)
        //{
        //    PreV = true;
        //    PreH = false;
        //    isBuilding = true;
        //    Instantiate(foundationPrefab2, Vector3.zero, foundationPrefab2.transform.rotation);

        //}
        if (buildHouse == true) //&& !isBuilding)
        {
            isBuilding = true;
            Instantiate(foundationPrefab3, Vector3.zero, foundationPrefab3.transform.rotation);
            buildHouse = false;
        }

        if (buildTower == true) //&& !isBuilding)
        {
            isBuilding = true;
            Instantiate(foundationPrefab4, Vector3.zero, foundationPrefab4.transform.rotation);
            buildTower = false;
        }
    }
}
