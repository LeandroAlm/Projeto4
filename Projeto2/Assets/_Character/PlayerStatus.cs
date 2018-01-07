using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public float maxHp = 100, HP;
    public float armor;
    public int wood, stone, meat, cookmeat;
    public GameObject Bag;
    //public GameObject mochila;
    private InventoryUI inventoryUI;
    public bool ExistWood, ExistStone, ExistMeat;
    int HealBuff = 1;
    float Timer = 0;
    public static List<Transform> Campfire;

    private void Start()
    {
        ExistStone = false;
        ExistWood = false;
        ExistMeat = false;
        inventoryUI = Bag.GetComponent<InventoryUI>();
        Campfire = new List<Transform>();
    }

    void Update()
    {
        foreach (Transform CF in Campfire)
        {
            float DistanceToBuff;
            DistanceToBuff = Vector3.Distance(transform.position, CF.transform.position);
            if (DistanceToBuff < 1)
            {
                BuffHP();
            }
        }

        RecouverHP();
    }

    public void StoneAmount(int value)
    {
        stone += value;
        inventoryUI.SetSlot("stone");
    }

    public void WoodAmount(int value)
    {
        wood += value;
        inventoryUI.SetSlot("wood");
    }

    public void MeatAmount(int value)
    {
        meat += value;
        inventoryUI.SetSlot("meat");
    }

    public void CookMeatAmount(int value)
    {
        cookmeat += value;
        inventoryUI.SetSlot("cookmeat");
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
        Debug.Log("Buff HP ++");
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
