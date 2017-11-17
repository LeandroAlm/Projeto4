﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{

    public GameObject Mochila;
    public Transform Player;
    bool aberto;
    PlayerStatus PS;

    public GameObject Slot1, Slot2, Slot3, Slot4, Slot5;
    public GameObject Slot6, Slot7, Slot8, Slot9, Slot10;
    public GameObject Slot11, Slot12, Slot13, Slot14, Slot15;

    public Text Text1, Text2, Text3, Text4, Text5;
    public Text Text6, Text7, Text8, Text9, Text10;
    public Text Text11, Text12, Text13, Text14, Text15;

    List<Transform> SlotsList;

    public Texture wood;
    public Texture stone;

    void Start ()
    {
        SlotsList = new List<Transform>();
        for (int i = 0; i < 15; i++)
        {
            SlotsList.Add(Mochila.transform.GetChild(0).GetChild(0).GetChild(i));
        }
        Mochila.SetActive(false);
        aberto = false;
        PS = gameObject.GetComponent<PlayerStatus>();
    }


	void Update ()
    {
        // ABRIR E FECHAR
        if (Input.GetKeyDown("i") && aberto == false)
        {
            aberto = true;
            Mochila.SetActive(true);
        }
        else if (Input.GetKeyDown("i") && aberto == true)
        {
            aberto = false;
            Mochila.SetActive(false);
        }     
    }

    public void SetSlot(string Slot)
    {
        if (Slot == "wood")
        {
            if (CheckWood() == 0)
            {
                // 1st empety slot possivel
                SlotEmpety(Slot);

                //if (CheckSlot() == 1)
                //{
                //    Slot1.GetComponent<RawImage>().texture = wood;
                //    Text1.text = "" + Player.GetComponent<PlayerStatus>().wood;
                //}
                //else if (CheckSlot() == 2)
                //{
                //    Slot2.GetComponent<RawImage>().texture = wood;
                //    Text2.text = "" + Player.GetComponent<PlayerStatus>().wood;
                //}
                //else if (CheckSlot() == 3)
                //{
                //    Slot3.GetComponent<RawImage>().texture = wood;
                //    Text3.text = "" + Player.GetComponent<PlayerStatus>().wood;
                //}
                //else if (CheckSlot() == 4)
                //{
                //    Slot4.GetComponent<RawImage>().texture = wood;
                //    Text4.text = "" + Player.GetComponent<PlayerStatus>().wood;
                //}
                //else if (CheckSlot() == 5)
                //{
                //    Slot5.GetComponent<RawImage>().texture = wood;
                //    Text5 .text = "" + Player.GetComponent<PlayerStatus>().wood;
                //}
                //else if (CheckSlot() == 6)
                //{
                //    Slot6.GetComponent<RawImage>().texture = wood;
                //    Text6.GetComponent<Text>().text = "" + Player.GetComponent<PlayerStatus>().wood;
                //}
                //else if (CheckSlot() == 7)
                //{
                //    Slot7.GetComponent<RawImage>().texture = wood;
                //    Text7.GetComponent<Text>().text = "" + Player.GetComponent<PlayerStatus>().wood;
                //}
                //else if (CheckSlot() == 8)
                //{
                //    Slot8.GetComponent<RawImage>().texture = wood;
                //    Text8.GetComponent<Text>().text = "" + Player.GetComponent<PlayerStatus>().wood;
                //}
                //else if (CheckSlot() == 9)
                //{
                //    Slot9.GetComponent<RawImage>().texture = wood;
                //    Text9.GetComponent<Text>().text = "" + Player.GetComponent<PlayerStatus>().wood;
                //}
                //else if (CheckSlot() == 10)
                //{
                //    Slot10.GetComponent<RawImage>().texture = wood;
                //    Text10.GetComponent<Text>().text = "" + Player.GetComponent<PlayerStatus>().wood;
                //}
                //else if (CheckSlot() == 11)
                //{
                //    Slot11.GetComponent<RawImage>().texture = wood;
                //    Text11.GetComponent<Text>().text = "" + Player.GetComponent<PlayerStatus>().wood;
                //}
                //else if (CheckSlot() == 12)
                //{
                //    Slot12.GetComponent<RawImage>().texture = wood;
                //    Text12.GetComponent<Text>().text = "" + Player.GetComponent<PlayerStatus>().wood;
                //}
                //else if (CheckSlot() == 13)
                //{
                //    Slot13.GetComponent<RawImage>().texture = wood;
                //    Text13.GetComponent<Text>().text = "" + Player.GetComponent<PlayerStatus>().wood;
                //}
                //else if (CheckSlot() == 14)
                //{
                //    Slot14.GetComponent<RawImage>().texture = wood;
                //    Text14.GetComponent<Text>().text = "" + Player.GetComponent<PlayerStatus>().wood;
                //}
                //else if (CheckSlot() == 15)
                //{
                //    Slot15.GetComponent<RawImage>().texture = wood;
                //    Text15.GetComponent<Text>().text = "" + Player.GetComponent<PlayerStatus>().wood;
                //}
            }
            else
            { 
                int i = 1;
                int Aux = CheckWood();
                foreach (Transform Trans in SlotsList)
                {
                    if (i == Aux)
                    {
                        // mudar tetxo
                    }
                    else
                        i++;
                }
            }
            //else if (CheckWood() == 1)
            //{
            //    Text1.text = ("" + Player.GetComponent<PlayerStatus>().wood);
            //}
            //else if (CheckWood() == 2)
            //{
            //    Text2.text = ("" + Player.GetComponent<PlayerStatus>().wood);
            //}
        }
        else if (Slot == "stone")
        {
            if (CheckStone() == 0)
            {
                if (CheckSlot() == 1)
                {
                    Slot1.GetComponent<RawImage>().texture = stone;
                    Text1.text = "" + Player.GetComponent<PlayerStatus>().stone;
                }
                else if (CheckSlot() == 2)
                {
                    Slot2.GetComponent<RawImage>().texture = stone;
                    Text2.text = "" + Player.GetComponent<PlayerStatus>().stone;
                }
                else if (CheckSlot() == 3)
                {
                    Slot3.GetComponent<RawImage>().texture = stone;
                    Text3.text = "" + Player.GetComponent<PlayerStatus>().stone;
                }
                else if (CheckSlot() == 4)
                {
                    Slot4.GetComponent<RawImage>().texture = stone;
                    Text4.text = "" + Player.GetComponent<PlayerStatus>().stone;
                }
                else if (CheckSlot() == 5)
                {
                    Slot5.GetComponent<RawImage>().texture = stone;
                    Text5.text = ("" + Player.GetComponent<PlayerStatus>().stone);
                }
                else if (CheckSlot() == 6)
                {
                    Slot6.GetComponent<RawImage>().texture = stone;
                    Text6.GetComponent<Text>().text = "" + Player.GetComponent<PlayerStatus>().stone;
                }
                else if (CheckSlot() == 7)
                {
                    Slot7.GetComponent<RawImage>().texture = stone;
                    Text7.GetComponent<Text>().text = "" + Player.GetComponent<PlayerStatus>().stone;
                }
                else if (CheckSlot() == 8)
                {
                    Slot8.GetComponent<RawImage>().texture = stone;
                    Text8.GetComponent<Text>().text = "" + Player.GetComponent<PlayerStatus>().stone;
                }
                else if (CheckSlot() == 9)
                {
                    Slot9.GetComponent<RawImage>().texture = stone;
                    Text9.GetComponent<Text>().text = "" + Player.GetComponent<PlayerStatus>().stone;
                }
                else if (CheckSlot() == 10)
                {
                    Slot10.GetComponent<RawImage>().texture = stone;
                    Text10.GetComponent<Text>().text = "" + Player.GetComponent<PlayerStatus>().stone;
                }
                else if (CheckSlot() == 11)
                {
                    Slot11.GetComponent<RawImage>().texture = stone;
                    Text11.GetComponent<Text>().text = "" + Player.GetComponent<PlayerStatus>().stone;
                }
                else if (CheckSlot() == 12)
                {
                    Slot12.GetComponent<RawImage>().texture = stone;
                    Text12.GetComponent<Text>().text = "" + Player.GetComponent<PlayerStatus>().stone;
                }
                else if (CheckSlot() == 13)
                {
                    Slot13.GetComponent<RawImage>().texture = stone;
                    Text13.GetComponent<Text>().text = "" + Player.GetComponent<PlayerStatus>().stone;
                }
                else if (CheckSlot() == 14)
                {
                    Slot14.GetComponent<RawImage>().texture = stone;
                    Text14.GetComponent<Text>().text = "" + Player.GetComponent<PlayerStatus>().stone;
                }
                else if (CheckSlot() == 15)
                {
                    Slot15.GetComponent<RawImage>().texture = stone;
                    Text15.GetComponent<Text>().text = "" + Player.GetComponent<PlayerStatus>().stone;
                }
            }
            else if (CheckStone() == 1)
            {
                Text1.text = ("" + Player.GetComponent<PlayerStatus>().stone);
            }
            else if (CheckStone() == 2)
            {
                Text2.text = ("" + Player.GetComponent<PlayerStatus>().stone);
            }
        }
    }

    int CheckSlot()
    {
        if (Slot1.GetComponent<RawImage>().texture == null)
        {
            return 1;
        }
        else if (Slot2.GetComponent<RawImage>().texture == null)
        {
            return 2;
        }
        else if (Slot3.GetComponent<RawImage>().texture == null)
        {
            return 3;
        }
        else if (Slot4.GetComponent<RawImage>().texture == null)
        {
            return 4;
        }
        else if (Slot5.GetComponent<RawImage>().texture == null)
        {
            return 5;
        }
        else if (Slot6.GetComponent<RawImage>().texture == null)
        {
            return 6;
        }
        else if (Slot7.GetComponent<RawImage>().texture == null)
        {
            return 7;
        }
        else if (Slot8.GetComponent<RawImage>().texture == null)
        {
            return 8;
        }
        else if (Slot9.GetComponent<RawImage>().texture == null)
        {
            return 9;
        }
        else if (Slot10.GetComponent<RawImage>().texture == null)
        {
            return 10;
        }
        else if (Slot11.GetComponent<RawImage>().texture == null)
        {
            return 11;
        }
        else if (Slot12.GetComponent<RawImage>().texture == null)
        {
            return 12;
        }
        else if (Slot13.GetComponent<RawImage>().texture == null)
        {
            return 13;
        }
        else if (Slot14.GetComponent<RawImage>().texture == null)
        {
            return 14;
        }
        else if (Slot15.GetComponent<RawImage>().texture == null)
        {
            return 15;
        }
        else
        { 
            return 0;
        }
    }

    void SlotEmpety(string Obj)
    {
        if (Obj == "wood")
        {
            foreach (Transform x in SlotsList)
            {
                if (x.GetComponent<RawImage>().texture == null)
                    x.GetComponent<RawImage>().texture = wood;

                break;
            }
        }
        else if (Obj == "stone")
        {
            foreach (Transform x in SlotsList)
            {
                if (x.GetComponent<RawImage>().texture == null)
                    x.GetComponent<RawImage>().texture = stone;

                break;
            }
        }
    }

    int CheckWood()
    {
        int i = 1;
        foreach (Transform slot in SlotsList)
        {
            if (slot.GetComponent<RawImage>().texture == wood)
            {
                return i;
            }
            else
                i++;
        }
        return 0;
        //if (Slot1.GetComponent<RawImage>().texture == wood)
        //{
        //    return 1;
        //}
        //else if (Slot2.GetComponent<RawImage>().texture == wood)
        //{
        //    return 2;
        //}
        //else if (Slot3.GetComponent<RawImage>().texture == wood)
        //{
        //    return 3;
        //}
        //else if (Slot4.GetComponent<RawImage>().texture == wood)
        //{
        //    return 4;
        //}
        //else if (Slot5.GetComponent<RawImage>().texture == wood)
        //{
        //    return 5;
        //}
        //else if (Slot6.GetComponent<RawImage>().texture == wood)
        //{
        //    return 6;
        //}
        //else if (Slot7.GetComponent<RawImage>().texture == wood)
        //{
        //    return 7;
        //}
        //else if (Slot8.GetComponent<RawImage>().texture == wood)
        //{
        //    return 8;
        //}
        //else if (Slot9.GetComponent<RawImage>().texture == wood)
        //{
        //    return 9;
        //}
        //else if (Slot10.GetComponent<RawImage>().texture == wood)
        //{
        //    return 10;
        //}
        //else if (Slot11.GetComponent<RawImage>().texture == wood)
        //{
        //    return 11;
        //}
        //else if (Slot12.GetComponent<RawImage>().texture == wood)
        //{
        //    return 12;
        //}
        //else if (Slot13.GetComponent<RawImage>().texture == wood)
        //{
        //    return 13;
        //}
        //else if (Slot14.GetComponent<RawImage>().texture == wood)
        //{
        //    return 14;
        //}
        //else if (Slot15.GetComponent<RawImage>().texture == wood)
        //{
        //    return 15;
        //}
        //else
        //{
        //    return 0;
        //}
    }

    int CheckStone()
    {
        int i = 1;
        foreach (Transform slot in SlotsList)
        {
            if (slot.GetComponent<RawImage>().texture == stone)
            {
                return i;
            }
            else
                i++;
        }
        return 0;

        //if (Slot1.GetComponent<RawImage>().texture == stone)
        //{
        //    return 1;
        //}
        //else if (Slot2.GetComponent<RawImage>().texture == stone)
        //{
        //    return 2;
        //}
        //else if (Slot3.GetComponent<RawImage>().texture == stone)
        //{
        //    return 3;
        //}
        //else if (Slot4.GetComponent<RawImage>().texture == stone)
        //{
        //    return 4;
        //}
        //else if (Slot5.GetComponent<RawImage>().texture == stone)
        //{
        //    return 5;
        //}
        //else if (Slot6.GetComponent<RawImage>().texture == stone)
        //{
        //    return 6;
        //}
        //else if (Slot7.GetComponent<RawImage>().texture == stone)
        //{
        //    return 7;
        //}
        //else if (Slot8.GetComponent<RawImage>().texture == stone)
        //{
        //    return 8;
        //}
        //else if (Slot9.GetComponent<RawImage>().texture == stone)
        //{
        //    return 9;
        //}
        //else if (Slot10.GetComponent<RawImage>().texture == stone)
        //{
        //    return 10;
        //}
        //else if (Slot11.GetComponent<RawImage>().texture == stone)
        //{
        //    return 11;
        //}
        //else if (Slot12.GetComponent<RawImage>().texture == stone)
        //{
        //    return 12;
        //}
        //else if (Slot13.GetComponent<RawImage>().texture == stone)
        //{
        //    return 13;
        //}
        //else if (Slot14.GetComponent<RawImage>().texture == stone)
        //{
        //    return 14;
        //}
        //else if (Slot15.GetComponent<RawImage>().texture == stone)
        //{
        //    return 15;
        //}
        //else
        //{
        //    return 0;
        //}
    }
}
