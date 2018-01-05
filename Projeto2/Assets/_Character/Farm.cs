using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour
{
    Animator anim;
    private Transform player;
    private Terrain terreno;
    private GameObject go;
    
    public GUIContent Mochila;
    public bool isWood, isStone; //rego, obvio!!!! nobices!

    public GameObject AxeGO;

    Ray ray;

    Vector3 forward;

    public static bool axeSwing;
    public bool playerFarmed;

    // Use this for initialization
    public void Start ()
    {
        player = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        isStone = false;
        isWood = false;
        playerFarmed = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Stone")
        {
            isStone = true;
            playerFarmed = true;
            player.GetComponent<PlayerStatus>().StoneAmount(collision.GetComponent<Stone>().GetAmount());
            collision.GetComponent<Stone>().Damage();         
        }

        else if (collision.tag == "Tree")
        {
            Debug.Log("Wood HIT");
            isWood = true;
            playerFarmed = true;
            player.GetComponent<PlayerStatus>().WoodAmount(collision.GetComponent<Tree>().GetAmount());
            collision.GetComponent<Tree>().Damage();
        }
    }



    public void Update ()
    {
        if (Input.GetMouseButtonDown(0) && AxeGO.activeSelf)
        {
            //axeSwing = true;
            anim.SetTrigger("Farming");

            //RaycastHit hit;
            //forward = transform.TransformDirection(Vector3.forward);
            //Debug.DrawRay(transform.position + Vector3.up, forward, Color.green);
            //Ray ray = new Ray(transform.position, transform.forward);

            //if (Physics.Raycast(ray, out hit, 4))
            //{
            //    if (hit.collider.tag == "Stone")
            //    {
            //        Debug.Log("Stone HIT");
            //        isStone = true;
            //        player.GetComponent<PlayerStatus>().StoneAmout(hit.collider.GetComponent<Stone>().GetAmount());
            //        hit.collider.GetComponent<Stone>().Damage();
            //    }
            //    else if (hit.collider.tag == "Tree")
            //    {
            //        Debug.Log("Wood HIT");
            //        isWood = true;
            //        player.GetComponent<PlayerStatus>().WoodAmout(hit.collider.GetComponent<Tree>().GetAmount());
            //        hit.collider.GetComponent<Tree>().Damage();
            //    }
            //}
        }

        else if (Input.GetMouseButtonDown(1) && AxeGO.activeSelf)
        {
            anim.SetTrigger("attack2");
        }

        isStone = false;
        isWood = false;
    }
    
}
