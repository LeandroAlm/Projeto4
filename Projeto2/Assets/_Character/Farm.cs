using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Farm : MonoBehaviour
{
    Animator anim;
    private Transform player;
    private Terrain terreno;
    private GameObject go;
    
    public GUIContent Mochila;

    public GameObject AxeGO;
    public GameObject ResourcesFromFarmCanvas;

    Ray ray;

    Vector3 forward;

    public static bool axeSwing;
    public bool playerFarmed;

    public int axeCount;

    public bool canFarm;
    private int stoneCollected = 0, woodCollected = 0;
    public Text woodGained, stoneGained;
    private float delayTimer = 0;


    public void Start ()
    {
        player = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        playerFarmed = false;
        axeCount = 0;
        canFarm = false;
        ResourcesFromFarmCanvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if  (canFarm)
        {
            if (collision.gameObject.tag == "Stone")
            {
                axeCount++;
                playerFarmed = true;
                player.GetComponent<PlayerStatus>().StoneAmount(collision.GetComponent<Stone>().GetAmount());
                stoneCollected += Stone.Amount;
                stoneGained.text = "x" + stoneCollected + " Stone";
                collision.GetComponent<Stone>().Damage();
            }
            else if (collision.tag == "Tree")
            {
                axeCount++;
                playerFarmed = true;
                player.GetComponent<PlayerStatus>().WoodAmount(collision.GetComponent<Wood>().GetAmount());
                woodCollected += Wood.Amount;
                woodGained.text = "x" + Wood.Amount + " Wood";
                collision.GetComponent<Wood>().Damage();
            }

            canFarm = false;
        }
    }


    public void Update ()
    {
        if (axeCount > 10)
        {
            canFarm = false;
        }

        if (Input.GetMouseButtonDown(0) && AxeGO.activeSelf || Input.GetMouseButton(0) && AxeGO)
        {
            canFarm = true;
            anim.SetTrigger("Farming");
        }
        else if (Input.GetMouseButtonDown(1) && AxeGO.activeSelf)
        {
            anim.SetTrigger("attack2");
        }

        if (playerFarmed)
        {
            delayTimer += Time.deltaTime;
            ResourcesFromFarmCanvas.SetActive(true);

            if (woodCollected == 0)
            {
                woodGained.gameObject.SetActive(false);
            }

            else
            {
                woodGained.gameObject.SetActive(true);
            }

            if (stoneCollected == 0)
            {
                stoneGained.gameObject.SetActive(false);
            }

            else
            {
                stoneGained.gameObject.SetActive(true);
            }

            if (delayTimer > 2f)
            {
                playerFarmed = false;
                delayTimer = 0;
            }
        }

        else
            ResourcesFromFarmCanvas.SetActive(false);

        //if (canFarm)
        //{
        //    if (Input.GetMouseButtonDown(0) && AxeGO.activeSelf)
        //    {
        //        //axeSwing = true;
        //        anim.SetTrigger("Farming");

        //        //RaycastHit hit;
        //        //forward = transform.TransformDirection(Vector3.forward);
        //        //Debug.DrawRay(transform.position + Vector3.up, forward, Color.green);
        //        //Ray ray = new Ray(transform.position, transform.forward);

        //        //if (Physics.Raycast(ray, out hit, 4))
        //        //{
        //        //    if (hit.collider.tag == "Stone")
        //        //    {
        //        //        Debug.Log("Stone HIT");
        //        //        isStone = true;
        //        //        player.GetComponent<PlayerStatus>().StoneAmout(hit.collider.GetComponent<Stone>().GetAmount());
        //        //        hit.collider.GetComponent<Stone>().Damage();
        //        //    }
        //        //    else if (hit.collider.tag == "Tree")
        //        //    {
        //        //        Debug.Log("Wood HIT");
        //        //        isWood = true;
        //        //        player.GetComponent<PlayerStatus>().WoodAmout(hit.collider.GetComponent<Tree>().GetAmount());
        //        //        hit.collider.GetComponent<Tree>().Damage();
        //        //    }
        //        //}
        //    }

        //    else if (Input.GetMouseButtonDown(1) && AxeGO.activeSelf)
        //    {
        //        anim.SetTrigger("attack2");
        //    }
        //}
    }
    
}
