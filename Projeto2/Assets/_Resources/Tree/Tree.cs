using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public int hp;
    private int Amount;

    private Camera cam;

    bool fade = false;

    private GameObject player;

    private void Start()
    {
        cam = Camera.main;


    }

    private void Update()
    {
        float distance = Vector3.Distance(this.transform.GetChild(0).transform.position, cam.transform.position);

        if(distance < 10)
        {
            iTween.FadeTo(this.transform.gameObject, 0, 0.7f);
            fade = true;
        }
        else if (fade)
        {
            iTween.FadeTo(this.transform.gameObject, 1, 1f);

            fade = false;

        }


    }

    public int GetAmount()
    {
        Amount = Random.Range(1, 3);
        return Amount;
    }

    public void Damage()
    {
        hp--;
        if (hp == 0)
        {
            player = GameObject.FindGameObjectWithTag("Player");

            Rigidbody rigidBody = this.gameObject.AddComponent<Rigidbody>();

            rigidBody.mass = 5;

            rigidBody.AddForce(player.gameObject.transform.forward, ForceMode.Impulse);


            Invoke("Destroy", 5f);
        }
    }

    void Destroy()
    {
        Destroy(gameObject);

    }
}


