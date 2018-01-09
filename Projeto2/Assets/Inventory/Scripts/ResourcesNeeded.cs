using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesNeeded : MonoBehaviour
{
    public Text woodNeeded, stoneNeeded, currentWood, currentStone;
    public int woodValue, stoneValue, woodNeededValue;
    public GameObject ResourcesPanel;
    public GameObject DescriptionPanel;
    public Text DescriptionText;
    private int SlotNumber;
    public GameObject Player;

    void Start()
    {
        ResourcesPanel.SetActive(false);
        DescriptionPanel.SetActive(false);
    }

    void Update()
    {
        currentWood.text = Player.GetComponent<PlayerStatus>().wood.ToString() + " /";
        currentStone.text = Player.GetComponent<PlayerStatus>().stone.ToString() + " /";

        //woodValue = Player.GetComponent<PlayerStatus>().wood;
        //stoneValue = Player.GetComponent<PlayerStatus>().stone;

        /*woodNeededValue = int.Parse(woodNeeded.text);

        if (woodValue < woodNeededValue)
        {
            currentWood.color = Color.red;
        }

        else
        {
            currentWood.color = Color.white;
        }*/

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
            woodNeeded.text = "30";
            stoneNeeded.text = "30";

            DescriptionText.text = "Build your house so you can be protected from enemies";
        }

        else if (SlotNumber == 1)
        {
            woodNeeded.text = "20";
            stoneNeeded.text = "10";

            DescriptionText.text = "Create a wall with these fences but be aware that they do not last forever";
        }

        else if (SlotNumber == 2)
        {
            woodNeeded.text = "50";
            stoneNeeded.text = "30";

            DescriptionText.text = "A powerfull tower that can fire your enemies for you";
        }

        else if (SlotNumber == 3)
        {
            woodNeeded.text = "30";
            stoneNeeded.text = "50";

            DescriptionText.text = "A gate so you can get in and out of your fence wall";
        }

        else if (SlotNumber == 4)
        {
            woodNeeded.text = "20";
            stoneNeeded.text = "30";

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
