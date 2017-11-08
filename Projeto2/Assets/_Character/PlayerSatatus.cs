using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSatatus : MonoBehaviour {

    public float hp;
    public float armor;
    public int wood, stone;
    public GameObject Mochila;
    Inventario inv;

    private void Start()
    {
        inv = Mochila.GetComponent<Inventario>();
    }

    public void StoneAmout(int valeu)
    {
        stone += valeu;
        inv.SetSlot("stone");
    }

    public void WoodAmout(int valeu)
    {
        wood += valeu;
        inv.SetSlot("wood");
    }
}
