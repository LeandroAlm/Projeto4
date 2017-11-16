using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject bag, slotPrefab;
    public GameObject title;
    public Transform PlayerTransform;
    private List<Transform> slotsList;
    private bool isOpen;
    private PlayerStatus playerStatus;
    public Texture wood, stone, DefaultTexture;
    public Text SlotText;

    [HideInInspector]
    public Farm farm;

	void Start ()
	{
        bag.SetActive(false);
        title.SetActive(false);
	    isOpen = false;
	    playerStatus = PlayerTransform.GetComponent<PlayerStatus>();
	    farm = GetComponent<Farm>();
        slotsList = new List<Transform>();

	    for (int i = 0; i < 20; i++)
	    {
	        slotsList.Add(bag.transform.GetChild(i));
        }
	}
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.I) && isOpen == false)
        {
            isOpen = true;
            bag.SetActive(true);
            title.SetActive(true);
        }

        else if (Input.GetKeyDown(KeyCode.I) && isOpen == true)
        {
            isOpen = false;
            bag.SetActive(false);
            title.SetActive(false);
        }
    }

    public void CheckSlot(string Object)
    {
        // Novo metodo gg izi
        //if (Object == "wood")
        //{
        //    if (playerStatus.ExistWood)
        //    {
        //        FindWood().GetComponent<Text>().text = "" + playerStatus.wood;
        //    }
        //    else 
        //    {
        //        SetSlot("wood");
        //        playerStatus.ExistWood = true;
        //    }
        //}
        //else if (Object == "stone")
        //{
        //    if (playerStatus.ExistStone)
        //    {
        //        FindStone().GetComponent<Text>().text = "" + playerStatus.stone;
        //    }
        //    else
        //    {
        //        SetSlot("stone");
        //        playerStatus.ExistStone = true;
        //    }
        //}

        // passei e tive de fazer á tuga
        if (Object == "wood")
        {
            if (playerStatus.ExistWood)
            {
                // Já existe madeira
                CheckWood();
            }
            else
            {
                // Nao existe madeira
                SetSlot("wood");
                playerStatus.ExistWood = true;
            }
        }
        else if (Object == "stone")
        {
            if (playerStatus.ExistStone)
            {
                CheckStone();
            }
            else
            {
                SetSlot("stone");
                playerStatus.ExistStone = true;
            }
        }
    }

    //public void SetSlot(string slot, int SlotNumber)
    public void SetSlot(string slot)
    {        
        if (slot == "wood")
        {
            foreach (Transform Slot in slotsList)
            {
                if (Slot.GetChild(0).GetChild(0).GetComponent<RawImage>().texture == DefaultTexture)
                {
                    Slot.GetChild(0).GetChild(0).GetComponent<RawImage>().texture = wood;
                    SlotText.text = "" + playerStatus.wood;
                }
            }          
        }
        else if (slot == "stone")
        {
            foreach (Transform Slot in slotsList)
            {
                if (Slot.GetChild(0).GetChild(0).GetComponent<RawImage>().texture == DefaultTexture)
                {
                    Slot.GetChild(0).GetChild(0).GetComponent<RawImage>().texture = stone;
                    SlotText.text = "" + playerStatus.stone;
                }
            }          
        }
    }

    void CheckWood()
    {
        foreach (Transform Trans in slotsList)
        {
            if (Trans.GetComponent<RawImage>().texture == wood)
            {
                Trans.GetChild(2).GetComponent<Text>().text = "" + playerStatus.wood;
            }
        }
    }

    void CheckStone()
    {
        foreach (Transform Trans in slotsList)
        {
            if (Trans.GetComponent<RawImage>().texture == stone)
            {
                Trans.GetChild(2).GetComponent<Text>().text = "" + playerStatus.stone;
            }
        }
    }
}
