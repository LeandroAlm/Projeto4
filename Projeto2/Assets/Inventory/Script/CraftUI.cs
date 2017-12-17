using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftUI : MonoBehaviour
{
    public List<Transform> Buttons;
    public GameObject Craft, CTitle, ITitle;

	void Start ()
	{
	    Buttons = new List<Transform>();

	    for (int i = 0; i < 20; i++)
	    {
	        Buttons.Add(transform.GetChild(0).GetChild(i));     
	    }

    }
	
	void Update ()
    {


	}

    public void CraftSlotConstruction(int slotNumber)
    {
        if (slotNumber == 0)
        {

            BuildingManager.buildHouse = true;

        }

        else if (slotNumber == 1)
        {

            BuildingManager.buildFence = true;
            BuildWall2.isPlaced = false;

        }

        else if (slotNumber == 2)
        {
            BuildingManager.buildTower = true;

        }

        else if (slotNumber == 3)
        {
            BuildingManager.gate = true;
  
        }

        else if (slotNumber == 4)
        {
            BuildingManager.buildFirePit = true;

        }

        Craft.SetActive(false);
        CTitle.SetActive(false);
        ITitle.SetActive(false);
    }
}
