using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour
{
    public int hp;
    public static int Amount;

    private Camera cam;

    bool fade = false;

    private GameObject player;

    AudioSource audio;


    private void Start()
    {
        audio = transform.GetComponent<AudioSource>();

        cam = Camera.main;
    }

    private void Update()
    {
        float distance = Vector3.Distance(this.transform.GetChild(0).transform.position, cam.transform.position);

        if (distance < 10)
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
        Amount = Random.Range(3, 10);
        return Amount;
    }

    public void Damage()
    {
        audio.Play();
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


