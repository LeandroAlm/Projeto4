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
    public GameObject FirePit;
    public GameObject FirePitGreen;

    public static bool buildHouse = false;
    public static bool buildTower = false;
    public static bool gate = false;
    public static bool buildFence = false;
    public static bool buildFirePit = false;
    

    public static bool isBuilding;

    private bool isPlaced;

    private bool gatePlaced = false;


    public static bool PreH, PreV;

    GameObject GateObjGreen;
    GameObject hitedObj;

    GameObject newFence;

    GameObject fence;

    GameObject Player;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (buildHouse == true && CraftUI.canBuildHouse) /*&& CraftUI.canBuildHouse == true)*/ //&& !isBuilding)
        {
            Player.GetComponent<PlayerStatus>().WoodAmount(-30);
            Player.GetComponent<PlayerStatus>().StoneAmount(-30);
            Debug.Log("Entrou");
            isBuilding = true;
            Instantiate(foundationPrefab3, Vector3.zero, foundationPrefab3.transform.rotation);
            buildHouse = false;
            InventoryUI.isOpen = false;
        }
        else if (buildTower == true && CraftUI.canBuildTower) //&& !isBuilding)
        {
            Player.GetComponent<PlayerStatus>().WoodAmount(-50);
            Player.GetComponent<PlayerStatus>().StoneAmount(-50);
            isBuilding = true;
            Instantiate(foundationPrefab4, Vector3.zero, foundationPrefab4.transform.rotation);
            buildTower = false;
            InventoryUI.isOpen = false;
        }
        else if (buildFence == true && CraftUI.canBuildFence)
        {
            isBuilding = true;
            newFence = Instantiate(fenceGreen, Vector3.zero, fenceGreen.transform.rotation);
            buildFence = false;
            InventoryUI.isOpen = false;
        }
        else if (buildFirePit && CraftUI.canBuildFireplace)
        {
            Player.GetComponent<PlayerStatus>().WoodAmount(-20);
            Player.GetComponent<PlayerStatus>().StoneAmount(-30);

            isBuilding = true;
            newFence = Instantiate(FirePitGreen, Vector3.zero, FirePitGreen.transform.rotation);
            PlayerStatus.Campfire.Add(newFence.transform);
            buildFirePit = false;
            InventoryUI.isOpen = false;
        }
        else if (gate && CraftUI.canBuildGate)
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

                Player.GetComponent<PlayerStatus>().WoodAmount(-20);
                Player.GetComponent<PlayerStatus>().StoneAmount(-10);

                InventoryUI.isOpen = false;
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
