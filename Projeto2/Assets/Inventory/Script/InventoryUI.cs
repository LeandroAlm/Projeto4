using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject bag, slotPrefab;
    public Transform PlayerTransform; //SlotsListTransform;
    private PlayerStatus[] slotsList;
    private bool isOpen;
    private PlayerStatus playerStatus;
    public Texture wood, stone;
    public Text SlotText;

    [HideInInspector]
    public Farm farm;

	void Start ()
	{
        bag.SetActive(false);
	    isOpen = false;
	    playerStatus = GetComponent<PlayerStatus>();
	    //slotsList = SlotsListTransform.GetComponentsInChildren<PlayerStatus>();
	    farm = GetComponent<Farm>();
	}
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.I) && isOpen == false)
        {
            isOpen = true;
            bag.SetActive(true);
        }

        else if (Input.GetKeyDown(KeyCode.I) && isOpen == true)
        {
            isOpen = false;
            bag.SetActive(false);
        }
    }

    public void CheckSlot()
    {
        for (int i = 0; i < slotsList.Length; i++)
        {
            
        }

        if (slotPrefab.GetComponent<RawImage>().texture == null)
        {
            if (farm.isWood == true)
            {
                SetSlot("wood");
            }

            else if (farm.isStone == true)
            {
                SetSlot("stone");
            }
        }
    }

    public void SetSlot(string slot)
    {
        if (slot == "wood")
        {
            slotPrefab.GetComponent<RawImage>().texture = wood;
            SlotText.text = "" + PlayerTransform.GetComponent<PlayerStatus>().wood;
        }

        else if (slot == "stone")
        {
            slotPrefab.GetComponent<RawImage>().texture = wood;
            SlotText.text = "" + PlayerTransform.GetComponent<PlayerStatus>().stone;
        }
    }
}
