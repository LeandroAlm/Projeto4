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
	    playerStatus = GetComponent<PlayerStatus>();
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
        int AuxWood = CheckWood();

        Debug.Log("Nr: " + AuxWood);
        if (AuxWood != 20)
        {
            SetSlot("wood", AuxWood);
        }
        else
        {
            int i = 0;
            bool pode = true;
            foreach (Transform slot in slotsList)
            {
                if (slot.GetChild(0).GetChild(0).GetComponent<RawImage>().texture == DefaultTexture  && pode)
                {
                    if (Object == "wood")
                    {
                        SetSlot("wood", i);
                        pode = false;
                        break;
                    }

                    else if (Object == "stone")
                    {
                        SetSlot("stone", i);
                        pode = false;
                        break;
                    }
                }
                else
                {
                    i++;
                    Debug.Log("i: " + i);
                    pode = false;
                }
            }
        }
        

        //if (slotPrefab.GetComponent<RawImage>().texture == null)
        //{
        //    if (farm.isWood == true)
        //    {
        //        SetSlot("wood");
        //    }

        //    else if (farm.isStone == true)
        //    {
        //        SetSlot("stone");
        //    }
        //}
    }

    public void SetSlot(string slot, int SlotNumber)
    {        
        if (slot == "wood")
        {
            int woodI = 0;
            foreach (Transform Slot in slotsList)
            {
                if (woodI == SlotNumber)
                {
                    Debug.Log("Desenhei a tua mãe de 4 a mamar num preto");
                    Slot.GetChild(0).GetChild(0).GetComponent<RawImage>().texture = wood;
                    SlotText.text = "" + PlayerTransform.GetComponent<PlayerStatus>().wood;
                }
                else
                {
                    woodI++;
                    Debug.Log(woodI);
                }
            }
            
        }

        else if (slot == "stone")
        {
            int stoneI = 0;

            foreach (Transform Slot in slotsList)
            {
                if (stoneI == SlotNumber)
                {
                    Slot.GetChild(0).GetChild(0).GetComponent<RawImage>().texture = stone;
                    SlotText.text = "" + PlayerTransform.GetComponent<PlayerStatus>().stone;
                }
                else
                {
                    stoneI++;
                }
            }          
        }
    }

    int CheckWood()
    {
        int i = 0;

        foreach (Transform trans in slotsList)
        {
            if (trans.GetChild(0).GetChild(0).GetComponent<RawImage>().texture == wood)
            {
                return i;

            }
            else
                i++;
        }
        //if (i < 20)
        //    return i;
        //else
        //    return 20;
        return 0;
    }

    int CheckStone()
    {
        int i = 0;

        foreach (Transform trans in slotsList)
        {
            if (trans.GetChild(0).GetChild(0).GetComponent<RawImage>().texture == stone)
            {
                return i;
            }
            else
                i++;
        }
        if (i < 20)
            return i;
        else
            return 20;
    }
}
