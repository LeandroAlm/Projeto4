using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryBag, craftBag;
    public GameObject inventoryTitle, craftTitle;
    public GameObject Player;
    private List<Transform> slotsList;
    public static bool isOpen;
    private PlayerStatus playerStatus;
    public Texture wood, stone, meat, DefaultTexture;
    public Text SlotText;
    public Button InventoryButton, CraftButton;

    [HideInInspector]
    public Farm farm;

    void Start()
    {
        inventoryBag.SetActive(false);
        inventoryTitle.SetActive(false);
        craftBag.SetActive(false);
        craftTitle.SetActive(false);
        isOpen = false;
        playerStatus = Player.GetComponent<PlayerStatus>();
        farm = GetComponent<Farm>();
        slotsList = new List<Transform>();

        Button craftButton = CraftButton.GetComponent<Button>();
        Button inventoryButton = InventoryButton.GetComponent<Button>();

        craftButton.onClick.AddListener(CraftClickButton);
        inventoryButton.onClick.AddListener(InventoryClickButton);

        for (int i = 0; i < 20; i++)
        {
            slotsList.Add(inventoryBag.transform.GetChild(i));
        }
    }

    void Update()
    {
        if (isOpen == false)
        {
            Invoke("WaitToShoot", 2);
            if (Input.GetKeyDown(KeyCode.I))
            {
                isOpen = true;
                inventoryBag.SetActive(true);
                inventoryTitle.SetActive(true);
                craftTitle.SetActive(true);
            }
        }
        else
        {
            Player.GetComponent<Shot>().enabled = false;
            if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Escape))
            {
                // O QUE ESTA AQUI TEM DE PASSAR PARA UMA FUNÇÃO, DPS PARA CHAMAR TBM QUANDO APLICAS OBJS
                isOpen = false;
                inventoryBag.SetActive(false);
                inventoryTitle.SetActive(false);
                craftTitle.SetActive(false);
            }
        }
    }

    void WaitToShoot()
    {
        Player.GetComponent<Shot>().enabled = true;
    }
    
    public void CraftClickButton()
    {
        Debug.Log("Craft button clicked");
        inventoryBag.SetActive(false);
        inventoryTitle.SetActive(true);
        craftTitle.SetActive(true);
        craftBag.SetActive(true);
    }

    public void InventoryClickButton()
    {
        Debug.Log("Inventory button clicked");
        inventoryBag.SetActive(true);
        inventoryTitle.SetActive(true);
        craftTitle.SetActive(true);
        craftBag.SetActive(false);
    }

    public void CheckSlot(string Object)
    {
        // passei e tive de fazer à tuga
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
        else if (Object == "meat")
        {
            if (playerStatus.ExistMeat)
            {
                CheckMeat();
            }
            else
            {
                SetSlot("meat");
                playerStatus.ExistMeat = true;
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
                // 1st empty slot possivel
                SlotEmpty(Slot);
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
                SlotEmpty(Slot);
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
        else if (Slot == "meat")
        {
            if (CheckMeat() == 0)
            {
                // 1st empety slot possivel
                SlotEmpty(Slot);
            }
            else
            {
                // exist meat in somewhere!
                int i = 1;
                int Aux = CheckMeat();
                foreach (Transform Trans in slotsList)
                {
                    // Quando o SLot que recebemos que ja tem meat é igual ao i
                    if (i == Aux)
                    {
                        // mudar tetxo
                        Trans.GetChild(2).GetComponent<Text>().text = "" + playerStatus.meat;
                        break;
                    }
                    else
                        i++;
                }
            }
        }
    }

    void SlotEmpty(string Obj)
    {
        // procura por um slot empety e quando encontra poem logo a ima e o txt
        if (Obj == "wood")
        {
            foreach (Transform slot in slotsList)
            {
                if (slot.GetChild(0).GetChild(0).GetComponent<RawImage>().texture == DefaultTexture)
                {
                    slot.GetChild(0).GetChild(0).GetComponent<RawImage>().texture = wood;
                    slot.GetChild(2).GetComponent<Text>().text = "" + playerStatus.wood;
                    break;
                }
            }
        }
        else if (Obj == "stone")
        {
            foreach (Transform slot in slotsList)
            {
                if (slot.GetChild(0).GetChild(0).GetComponent<RawImage>().texture == DefaultTexture)
                {
                    slot.GetChild(0).GetChild(0).GetComponent<RawImage>().texture = stone;
                    slot.GetChild(2).GetComponent<Text>().text = "" + playerStatus.stone;
                    break;
                }
            }
        }
        else if (Obj == "meat")
        {
            foreach (Transform slot in slotsList)
            {
                if (slot.GetChild(0).GetChild(0).GetComponent<RawImage>().texture == DefaultTexture)
                {
                    slot.GetChild(0).GetChild(0).GetComponent<RawImage>().texture = meat;
                    slot.GetChild(2).GetComponent<Text>().text = "" + playerStatus.meat;
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

    int CheckMeat()
    {
        // se houver meat envia o nr do Slot, caso contrario envia 0
        int i = 1;
        foreach (Transform slot in slotsList)
        {
            if (slot.GetChild(0).GetChild(0).GetComponent<RawImage>().texture == meat)
            {
                return i;
            }
            else
                i++;
        }
        return 0;
    }
}
