using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public float maxHp = 100, hp;
    public bool distanceToRecover;
    public float armor;
    public float recoverTimer = 0;
    public int wood, stone;
    public GameObject Bag;
    //public GameObject mochila;
    private InventoryUI inventoryUI;
    public bool ExistWood, ExistStone;

    private void Start()
    {
        ExistStone = false;
        ExistWood = false;
        distanceToRecover = true;
        inventoryUI = Bag.GetComponent<InventoryUI>();
    }

    void Update()
    {
        RecoverDamage();
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

    public void RecoverDamage()
    {
        if (distanceToRecover == true && hp < maxHp)
        {
            //recoverTimer = 0

            hp += 0.1f;

            Debug.Log("Player Recover Life: " + hp);          
        }
    }
}
