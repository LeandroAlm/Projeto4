using System.Collections;
using System.Collections.Generic;
//using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    public GameObject TutorialCanvas, ResourcesText, WeaponsText, InventoryText, InstructionText, WarningText, repairingText;
    public GameObject Player;
    public Farm farm;

    void Start()
    {
        farm = Player.GetComponent<Farm>();
        WeaponsText.GetComponent<Text>();
        ResourcesText.GetComponent<Text>();
        InventoryText.GetComponent<Text>();
        InstructionText.GetComponent<Text>();

        WeaponsText.SetActive(true);
        WarningText.SetActive(true);
        ResourcesText.SetActive(false);
        InventoryText.SetActive(false);
        InstructionText.SetActive(false);
        repairingText.SetActive(false);
    }

    public void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2))
        {
            WeaponsText.SetActive(false);
            ResourcesText.SetActive(true);
        }

        if (farm.playerFarmed == true)
        {
            ResourcesText.SetActive(false);
            InventoryText.SetActive(true);
        }

        if (InventoryUI.isOpen == true)
        {
            WeaponsText.SetActive(false);
            InventoryText.SetActive(false);
            WarningText.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && InventoryUI.isOpen == true|| Input.GetKeyDown(KeyCode.I) && InventoryUI.isOpen == true)
        {
            InstructionText.SetActive(true);
            WeaponsText.SetActive(false);
            Destroy(TutorialCanvas, 5);
        }

        if(BuildingManager.houseIsCreated)
        {
            repairingText.SetActive(true);

            if(Input.GetKeyDown(KeyCode.F))
            {
                repairingText.SetActive(false);
                BuildingManager.houseIsCreated = false;
            }
        }
    }
}
