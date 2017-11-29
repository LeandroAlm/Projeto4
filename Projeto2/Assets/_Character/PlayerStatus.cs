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
    private InventoryUI inventoryUI;
    public bool ExistWood, ExistStone;

    private void Start()
    {
        ExistStone = false;
        ExistWood = false;
        inventoryUI = Bag.GetComponent<InventoryUI>();
    }

    public void StoneAmout(int value)
    {
        stone += value;
        inventoryUI.SetSlot("stone");

    }

    public void WoodAmout(int value)
    {
        wood += value;
        inventoryUI.SetSlot("wood");
    }
}
