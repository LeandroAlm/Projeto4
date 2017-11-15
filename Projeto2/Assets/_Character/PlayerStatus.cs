using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public float hp;
    public float armor;
    public int wood, stone;
    public GameObject Bag;
    Inventario inv;
    private InventoryUI inventoryUI; //rego

    private void Start()
    {
        //inv = Bag.GetComponent<Inventario>();
        inventoryUI = Bag.GetComponent<InventoryUI>();
    }

    public void StoneAmout(int value)
    {
        stone += value;
        //inv.SetSlot("stone");
        inventoryUI.CheckSlot("stone");
    }

    public void WoodAmout(int value)
    {
        wood += value;
        //inv.SetSlot("wood");
        inventoryUI.CheckSlot("wood");
    }
}
