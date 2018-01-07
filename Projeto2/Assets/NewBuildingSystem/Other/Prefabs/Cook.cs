using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cook : MonoBehaviour {

    public GameObject Player;
    float distance;

	void Start ()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
	}
	

	void Update ()
    {
        distance = Vector3.Distance(Player.transform.position, transform.position);

        if (Input.GetKeyDown(KeyCode.F))
        {
           if (distance < 2)
           {
                //Perto da fogueira
                if (Player.GetComponent<PlayerStatus>().wood >= 1)
                {
                    // Tem madeira
                    if (Player.GetComponent<PlayerStatus>().meat >= 1)
                    {
                        // tem carne entao cozinha
                        // desconta 1 de wood
                        Player.GetComponent<PlayerStatus>().WoodAmount(-1);
                        // desconta 1 de meat
                        Player.GetComponent<PlayerStatus>().MeatAmount(-1);
                        // adiciona 1 de CookMeat
                        Player.GetComponent<PlayerStatus>().CookMeatAmount(1);
                    }
                    else
                    {
                        //sms falta de carne
                    }
                }
                else
                {
                    // sms de falta de madeira
                }
           }
        }
    }


    void cooking()
    {

    }
}
