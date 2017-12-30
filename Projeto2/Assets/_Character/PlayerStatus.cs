using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public float maxHp = 100, HP;
    public float armor;
    public int wood, stone;
    public GameObject Bag;
    //public GameObject mochila;
    private InventoryUI inventoryUI;
    public bool ExistWood, ExistStone;
    int HealBuff = 1;
    float Timer = 0;

    private void Start()
    {
        ExistStone = false;
        ExistWood = false;
        inventoryUI = Bag.GetComponent<InventoryUI>();
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Y))
        {
            // If (tem poções)
            BuffHP();
        }

        RecouverHP();
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

    public void GetDamage(int DamageAmount)
    {
        HP -= DamageAmount;
    }

    void CheckDie()
    {
        if(HP <= 0)
        {
            // PLAYER DEAD
            Debug.Log("Player died!");
        }
    }

    void RecouverHP()
    {
        if (HP < maxHp)
        {
            HP += 0.1f * Time.deltaTime * HealBuff;
            Debug.Log("Player is healing: " + HP);          
        }
    }
    
    void BuffHP()
    {
        if (Timer < 10)
        {
            // Buff of HP ON
            HealBuff = 5;
            Timer += 1 * Time.deltaTime;
        }
        else
        {
            // Buff of HP OFF
            HealBuff = 1;
            Timer = 0;
        }
    }
}
