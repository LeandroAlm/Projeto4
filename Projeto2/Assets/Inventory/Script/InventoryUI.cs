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
    //public void SetSlot(string slot)
    //{        
    //    if (slot == "wood")
    //    {
    //        foreach (Transform Slot in slotsList)
    //        {
    //            if (Slot.GetChild(0).GetChild(0).GetComponent<RawImage>().texture == DefaultTexture)
    //            {
    //                Slot.GetChild(0).GetChild(0).GetComponent<RawImage>().texture = wood;
    //                SlotText.text = "" + playerStatus.wood;
    //            }
    //        }          
    //    }
    //    else if (slot == "stone")
    //    {
    //        foreach (Transform Slot in slotsList)
    //        {
    //            if (Slot.GetChild(0).GetChild(0).GetComponent<RawImage>().texture == DefaultTexture)
    //            {
    //                Slot.GetChild(0).GetChild(0).GetComponent<RawImage>().texture = stone;
    //                SlotText.text = "" + playerStatus.stone;
    //            }
    //        }          
    //    }
    //}

    //void CheckWood()
    //{
    //    foreach (Transform Trans in slotsList)
    //    {
    //        if (Trans.GetComponent<RawImage>().texture == wood)
    //        {
    //            Trans.GetChild(2).GetComponent<Text>().text = "" + playerStatus.wood;
    //        }
    //    }
    //}

    //void CheckStone()
    //{
    //    foreach (Transform Trans in slotsList)
    //    {
    //        if (Trans.GetComponent<RawImage>().texture == stone)
    //        {
    //            Trans.GetChild(2).GetComponent<Text>().text = "" + playerStatus.stone;
    //        }
    //    }
    //}

    public void SetSlot(string Slot)
    {
        if (Slot == "wood")
        {
            if (CheckWood() == 0)
            {
                // 1st empety slot possivel
                SlotEmpety(Slot);
            }
            else
            {
                // exist wood in somewhere!
                int i = 1;
                int Aux = CheckWood();
                foreach (Transform Trans in slotsList)
                {
                    // Quando o SLot que recebemos que ja tem wood é igual ao i
                    if (i == Aux)
                    {
                        // mudar tetxo
                        Trans.GetChild(2).GetComponent<Text>().text = "" + playerStatus.wood;
                        break;
                    }
                    else
                        i++;
                }
            }
        }
        else if (Slot == "stone")
        {
            if (CheckStone() == 0)
            {
                // 1st empety slot possivel
                SlotEmpety(Slot);
            }
            else
            {
                // exist stone in somewhere!
                int i = 1;
                int Aux = CheckStone();
                foreach (Transform Trans in slotsList)
                {
                    // Quando o SLot que recebemos que ja tem stone é igual ao i
                    if (i == Aux)
                    {
                        // mudar tetxo
                        Trans.GetChild(2).GetComponent<Text>().text = "" + playerStatus.stone;
                        break;
                    }
                    else
                        i++;
                }
            }
        }
    }

    void SlotEmpety(string Obj)
    {
        // procura por um slot empety e quando encontra poem logo a ima e o txt
        if (Obj == "wood")
        {
            foreach (Transform x in slotsList)
            {
                if (x.GetChild(0).GetChild(0).GetComponent<RawImage>().texture == DefaultTexture)
                {
                    x.GetChild(0).GetChild(0).GetComponent<RawImage>().texture = wood;
                    x.GetChild(2).GetComponent<Text>().text = "" + playerStatus.wood;
                    break;
                }
                
            }
        }
        else if (Obj == "stone")
        {
            foreach (Transform x in slotsList)
            {
                if (x.GetChild(0).GetChild(0).GetComponent<RawImage>().texture == DefaultTexture)
                {
                    x.GetChild(0).GetChild(0).GetComponent<RawImage>().texture = stone;
                    x.GetChild(2).GetComponent<Text>().text = "" + playerStatus.stone;
                    break;
                }
                
            }
        }
    }

    int CheckWood()
    {
        // se houver wood envia o nr do Slot, caso contrario envia 0
        int i = 1;
        foreach (Transform slot in slotsList)
        {
            if (slot.GetChild(0).GetChild(0).GetComponent<RawImage>().texture == wood)
            {
                return i;
            }
            else
                i++;
        }
        return 0;
    }

    int CheckStone()
    {
        // se houver stone envia o nr do Slot, caso contrario envia 0
        int i = 1;
        foreach (Transform slot in slotsList)
        {
            if (slot.GetChild(0).GetChild(0).GetComponent<RawImage>().texture == stone)
            {
                return i;
            }
            else
                i++;
        }
        return 0;
    }
}
