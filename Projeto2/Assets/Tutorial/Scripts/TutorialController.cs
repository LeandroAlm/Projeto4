using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    public GameObject TutorialCanvas, ResourcesText, WeaponsText, InventoryText, InstructionText;

    void Start()
    {
        WeaponsText.GetComponent<Text>();
        ResourcesText.GetComponent<Text>();
        InventoryText.GetComponent<Text>();
        InstructionText.GetComponent<Text>();

        WeaponsText.SetActive(true);
        ResourcesText.SetActive(false);
        InventoryText.SetActive(false);
        InstructionText.SetActive(false);
    }

    public void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2))
        {
            WeaponsText.SetActive(false);
            ResourcesText.SetActive(true);
            InventoryText.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            WeaponsText.SetActive(false);
            InventoryText.SetActive(false);
            ResourcesText.SetActive(false);
        }

        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            InstructionText.SetActive(true);
            Destroy(TutorialCanvas, 5);
        }
    }
}
