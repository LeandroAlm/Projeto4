using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public GameObject foundationPrefab;
    public GameObject foundationPrefab2;
    public GameObject foundationPrefab3;
    public GameObject foundationPrefab4;
    public GameObject gatePrefabGreen;
    public GameObject gatePrefab;
    public GameObject fenceGreen;

    public static bool buildHouse = false;
    public static bool buildTower = false;
    public static bool gate = false;
    public static bool buildFence = false;


    public static bool isBuilding;

    private bool place;
    private bool isPlaced;

    public static bool PreH, PreV;

    GameObject GateObjGreen;
    GameObject hitedObj;

    GameObject newFence; 

    private void Start()
    {
      
    }

    void Update ()
    {
    
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

        if(buildFence == true)
        {
            isBuilding = true;
            newFence =  Instantiate(fenceGreen, Vector3.zero, fenceGreen.transform.rotation);
            buildFence = false;
        }

        if (gate)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
               if (hit.collider.name == "big_fence" && !place)
                {


                    GateObjGreen = Instantiate(gatePrefabGreen, hit.collider.transform.position, hit.collider.transform.rotation);
                    place = true;
                }
                if (hit.collider.name != "big_fence")
                {
                    place = false;
                    Destroy(GateObjGreen);
                }
            
            }

            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(gatePrefab, GateObjGreen.transform.position, GateObjGreen.transform.rotation);
            }
            //if(isPlaced)
            //{
            //    hitedObj = hit.collider.gameObject;
            //    hitedObj.SetActive(false);
            //    isPlaced = false;
            //}



        }
    }
}
