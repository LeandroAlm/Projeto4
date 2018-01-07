using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cook : MonoBehaviour {

    public GameObject Player;
    float distance;

    private Animator anim;

    bool isCooking;

    float timer;

    PLayerControl playerControlScript;

    void Start ()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        anim = Player.GetComponent<Animator>();

        playerControlScript = Player.GetComponent<PLayerControl>();

        timer = 7;

        isCooking = false;
    }


    void Update ()
    {
        cooking();
        distance = Vector3.Distance(Player.transform.position, transform.position);
        Debug.Log(timer);
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
                        timer = 7;
                        isCooking = true;
                        //timer = 7f;
                        //anim.SetBool("isCooking", true);
                        //// tem carne entao cozinha
                        //// desconta 1 de wood
                        //if (timer <= 0)
                        //{
                        //    Debug.Log("ola linda");
                        //    Player.GetComponent<PlayerStatus>().WoodAmount(-1);
                        //    // desconta 1 de meat
                        //    Player.GetComponent<PlayerStatus>().MeatAmount(-1);
                        //    // adiciona 1 de CookMeat
                        //    Player.GetComponent<PlayerStatus>().CookMeatAmount(1);
                        //}
                        //timer -= Time.deltaTime;


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
        if(isCooking)
        {
            playerControlScript.canMove = false;
            //Player.transform.LookAt(this.transform);
            anim.SetBool("isCooking", true);
            // tem carne entao cozinha
            // desconta 1 de wood
            if (timer <= 0)
            {
                playerControlScript.canMove = true;
                Debug.Log("ola linda");
                Player.GetComponent<PlayerStatus>().WoodAmount(-1);
                // desconta 1 de meat
                Player.GetComponent<PlayerStatus>().MeatAmount(-1);
                // adiciona 1 de CookMeat
                Player.GetComponent<PlayerStatus>().CookMeatAmount(1);

                isCooking = false;
            }
            timer -= Time.deltaTime;

        }
        else
        {
            anim.SetBool("isCooking", false);
        }
    }
}
