using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesNeeded : MonoBehaviour
{
    public Text woodAmount, stoneAmount;

    public GameObject ResourcesPanel;
    public GameObject DescriptionPanel; //HouseDescriptionPanel, FireplaceDescriptionPanel, TowerDescriptionPanel, GateDescriptionPanel, FenceDescriptionPanel;
    public Text DescriptionText;
    private int SlotNumber;

    void Start()
    {
        ResourcesPanel.SetActive(false);
        DescriptionPanel.SetActive(false);

        /*HouseDescriptionPanel.SetActive(false);
        FireplaceDescriptionPanel.SetActive(false);
        TowerDescriptionPanel.SetActive(false);
        GateDescriptionPanel.SetActive(false);
        FenceDescriptionPanel.SetActive(false);*/
    }

    void Update()
    {
        if (InventoryUI.isOpen == false)
        {
            ResourcesPanel.SetActive(false);
            DescriptionPanel.SetActive(false);
        }

        SetNecessaryResources();
    }

    public void SetNecessaryResources()
    {
        if (SlotNumber == 0)
        {
            woodAmount.text = "30";
            stoneAmount.text = "30";

            DescriptionText.text = "Build your house so you can be protected from enemies";
        }

        else if (SlotNumber == 1)
        {
            woodAmount.text = "20";
            stoneAmount.text = "10";

            DescriptionText.text = "Create a wall with this fences but be aware that they do not last forever";
        }

        else if (SlotNumber == 2)
        {
            woodAmount.text = "50";
            stoneAmount.text = "30";

            DescriptionText.text = "A powerfull tower that can fire your enemies for you";
        }

        else if (SlotNumber == 3)
        {
            woodAmount.text = "30";
            stoneAmount.text = "50";

            DescriptionText.text = "A gate so you can get in and out of your fence wall";
        }

        else if (SlotNumber == 4)
        {
            woodAmount.text = "20";
            stoneAmount.text = "30";

            DescriptionText.text = "This fireplace allows you to heal much faster if you are close to it";
        }
    }

    public void ActivatePanel(int slotNumber)
    {
        SlotNumber = slotNumber;
        ResourcesPanel.SetActive(true);
        DescriptionPanel.SetActive(true);
    }

    public void DeactivatePanel()
    {
        ResourcesPanel.SetActive(false);
        DescriptionPanel.SetActive(false);
    }
}
