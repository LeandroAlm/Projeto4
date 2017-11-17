using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public float hp;
    public float armor;
    public int wood, stone;
    public GameObject Bag;
    public GameObject mochila;
    Inventario inv;
    private InventoryUI inventoryUI; //rego
    public bool ExistWood, ExistStone;

    private void Start()
    {
        ExistStone = false;
        ExistWood = false;
        //inv = Bag.GetComponent<Inventario>();
        inventoryUI = Bag.GetComponent<InventoryUI>();
        //inv = mochila.GetComponent<Inventario>();
    }

    public void StoneAmout(int value)
    {
        stone += value;
        //inv.SetSlot("stone");
        inventoryUI.SetSlot("stone");

    }

    public void WoodAmout(int value)
    {
        wood += value;
        //inv.SetSlot("wood");
        inventoryUI.SetSlot("wood");
    }
}
