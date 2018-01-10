using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftUI : MonoBehaviour
{
    public List<Transform> Buttons;
    public GameObject Craft, CTitle, ITitle, Player;
    //public GameObject HouseSlider, FenceSlider, TowerSlider, GateSlider, FireplaceSlider;

    public RawImage HouseImage, FenceImage, TowerImage, FireplaceImage, GateImage;
    public PlayerStatus PlayerStatus;
    public static bool canBuildHouse, canBuildFence, canBuildTower, canBuildFireplace, canBuildGate, isBuildingSomething;
    public static int SlotNumber;
    public ResourcesNeeded ResourcesNeeded;

    void Start ()
	{
	    PlayerStatus = Player.GetComponent<PlayerStatus>();
	    ResourcesNeeded = GetComponent<ResourcesNeeded>();
	    Buttons = new List<Transform>();

	    for (int i = 0; i < 20; i++)
	    {
	        Buttons.Add(transform.GetChild(0).GetChild(i));     
	    }

	    /*HouseSlider.SetActive(false);
	    FenceSlider.SetActive(false);
	    TowerSlider.SetActive(false);
	    GateSlider.SetActive(false);
	    FireplaceSlider.SetActive(false);*/

	    isBuildingSomething = false;
	}
	
	void Update ()
    {
        TexturesManagement();
        BuildManager();

        if (canBuildHouse || canBuildFence || canBuildTower || canBuildFireplace || canBuildGate)
            isBuildingSomething = true;
    }

    public void CraftSlotConstruction(int slotNumber)
    {
        //HouseSlider.SetActive(true);

        //HouseSlider.GetComponent<Slider>().value += 0.001f;

        //Debug.Log("Slider Value: " + HouseSlider.GetComponent<Slider>().value);

        //if (HouseSlider.GetComponent<Slider>().value >= 1f)
        //{
        //    HouseSlider.SetActive(false);
        //    canBuildHouse = true;
        //}

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
        Color32 transparentColor = new Color32(255, 255, 255, 100);
        Color32 buildColor = new Color32(255, 255, 255, 255);

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

        if (canBuildGate)
            GateImage.color = buildColor;
        else
            GateImage.color = transparentColor;
    }

    public void BuildManager()
    {
        if (ResourcesNeeded.woodValue >= 5 && ResourcesNeeded.stoneValue >= 2)
        {
            canBuildFence = true;
            

        }
        else
            canBuildFence = false;

        if (ResourcesNeeded.woodValue >= 30 && ResourcesNeeded.stoneValue >= 30)
        {
            canBuildHouse = true;
       
        }
        else
            canBuildHouse = false;

        if (ResourcesNeeded.woodValue >= 50 && ResourcesNeeded.stoneValue >= 30)
        {
            canBuildTower = true;
         
        }
        else
            canBuildTower = false;

        if (ResourcesNeeded.woodValue >= 20 && ResourcesNeeded.stoneValue >= 10)
        {
            canBuildGate = true;
            
        }
        else
            canBuildGate = false;

        if (ResourcesNeeded.woodValue >= 20 && ResourcesNeeded.stoneValue >= 30)
        {
            canBuildFireplace = true;
 
        }
        else
            canBuildFireplace = false;
    }
}
