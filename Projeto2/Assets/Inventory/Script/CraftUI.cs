using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftUI : MonoBehaviour
{
    public List<Transform> Buttons;
    public GameObject Craft, CTitle, ITitle, Player;

    public RawImage HouseImage, FenceImage, TowerImage, FireplaceImage, GateImage;
    public PlayerStatus PlayerStatus;
    private bool canBuildHouse, canBuildFence, canBuildTower, canBuildFireplace;

	void Start ()
	{
	    PlayerStatus = Player.GetComponent<PlayerStatus>();

	    Buttons = new List<Transform>();

	    for (int i = 0; i < 20; i++)
	    {
	        Buttons.Add(transform.GetChild(0).GetChild(i));     
	    }
    }
	
	void Update ()
    {
        TexturesManagement();
        BuildManager();
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

    public void TexturesManagement()
    {
        Color transparentColor = new Color(255, 255, 255, 30);
        Color buildColor = new Color(255, 255, 255, 255);

        if(canBuildFence)
            FenceImage.color = buildColor;
        else
            FenceImage.color = transparentColor;

        if (canBuildHouse)
            HouseImage.color = buildColor;
        else
            HouseImage.color = transparentColor;

        if (canBuildTower)
            TowerImage.color = buildColor;
        else
            TowerImage.color = transparentColor;

        if (canBuildFireplace)
            FireplaceImage.color = buildColor;
        else
            FireplaceImage.color = transparentColor;
    }

    public void BuildManager()
    {
        if (PlayerStatus.wood >= 5 && PlayerStatus.stone >= 5)
        {
            canBuildFence = true;
        }
        else
            canBuildFence = false;

        if (PlayerStatus.wood >= 10 && PlayerStatus.stone >= 5)
        {
            canBuildHouse = true;
        }
        else
            canBuildHouse = false;
    }
}
