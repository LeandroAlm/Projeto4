﻿using System.Collections;
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
            Debug.Log("sdsssssssssssssssssssssssss");

            BuildingManager.buildHouse = true;
            BuildWall2.firstFence = false;

        }

        else if (slotNumber == 1)
        {

            //BuildWall2.canBuild = true;
            BuildWall2.firstFence = true;
        }

        else if (slotNumber == 2)
        {
            BuildingManager.buildTower = true;
            BuildWall2.firstFence = false;

        }

        else if (slotNumber == 3)
        {
            BuildingManager.gate = true;
            BuildWall2.firstFence = false;

        }

        Craft.SetActive(false);
        CTitle.SetActive(false);
        ITitle.SetActive(false);
    }
}
