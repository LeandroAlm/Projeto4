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

    private bool isPlaced;

    private bool gatePlaced = false;


    public static bool PreH, PreV;

    GameObject GateObjGreen;
    GameObject hitedObj;

    GameObject newFence;

    GameObject fence;

    private void Start()
    {
      
    }

    void Update()
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

        if (buildFence == true)
        {
            isBuilding = true;
            newFence = Instantiate(fenceGreen, Vector3.zero, fenceGreen.transform.rotation);
            buildFence = false;
        }

        if (gate)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, 1 << 12) && !gatePlaced)
            {
                if(GateObjGreen != null)
                {
                    Destroy(GateObjGreen);
                }
                if (hit.collider.name == "big_fence")
                {
                    fence = hit.collider.gameObject;
                }

                GateObjGreen = Instantiate(gatePrefabGreen, hit.collider.transform.position, hit.collider.transform.rotation);

                gatePlaced = true;
            }
            else
            {

                gatePlaced = false;
            }

        }
    

        if (Input.GetMouseButtonDown(0))
        {
            Destroy(fence);
            Destroy(GateObjGreen);
            if (GateObjGreen != null)
            {
                Instantiate(gatePrefab, GateObjGreen.transform.position, GateObjGreen.transform.rotation);
            }
            gate = false;
        }
        //if(isPlaced)
        //{
        //    hitedObj = hit.collider.gameObject;
        //    hitedObj.SetActive(false);
        //    isPlaced = false;
        //}



        
    }
}
