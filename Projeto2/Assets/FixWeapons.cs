using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixWeapons : MonoBehaviour {

    bool fix, isFixed, onTable, fixGun, fixAxe;

    private Animator animator;

    private GameObject player;

    Shot shootScript;
    PLayerControl playerControlScript;

    Farm farmScript;

    float timer;

	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = player.GetComponent<Animator>();
        shootScript = player.GetComponent<Shot>();
        playerControlScript = player.GetComponent<PLayerControl>();
        farmScript = player.GetComponent<Farm>();
        timer = 7f;
        fix = false;
	}
	
	void Update ()
    {
        Fixing();
    }


    void OnTriggerEnter(Collider collision)
    {
        if (collision.name == "Player")
        {
            onTable = true;        
        }

    }

    void Fixing()
    {
        if(onTable)
        {
            if (PLayerControl.usingAxe)
            {
                FixeAxe();
            }

            if (PLayerControl.usingGun)
            {
                FixeGun();
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                timer = 7f;

                fix = true;
            }
        }
   

    }

    void FixeAxe()
    {
        if(fix)
        {
            animator.SetBool("isFixing", true);
            playerControlScript.canMove = false;
            if (timer <= 0)
            {
                player.transform.LookAt(this.transform);

                playerControlScript.canMove = true;
                farmScript.axeCount = 0;
                farmScript.canFarm = true;

                fix = false;
                animator.SetBool("isFixing", false);

            }

            timer -= Time.deltaTime;

        }
        else
        {

            fix = false;
        }
    
    }

    void FixeGun()
    {
        if (fix)
        {
            animator.SetBool("isFixing", true);
            playerControlScript.canMove = false;
            if (timer <= 0)
            {
                playerControlScript.canMove = true;
                player.transform.LookAt(this.transform);
                shootScript.bulletCount = 0;
                Shot.canShoot = true;
                animator.SetBool("Gun", true);
                //isFixed = true;
                Debug.Log("fixed: " + isFixed);
                animator.SetBool("isFixing", false);

                fix = false;
                
            }

            timer -= Time.deltaTime;

        }
        else
        {
            fix = false;
        }
    }

}
