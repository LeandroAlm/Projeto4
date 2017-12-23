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
    public bool isWood, isStone; //rego

    Ray ray;

    Vector3 forward;

	// Use this for initialization
	void Start ()
    {
        player = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        isStone = false;
        isWood = false;
    }
	
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            forward = transform.TransformDirection(Vector3.forward * 10 + new Vector3(0, 0.5f, 0));
            Debug.DrawRay(transform.position + Vector3.up, forward, Color.green);
            Ray ray = new Ray(transform.position, transform.forward);
            
            if (anim.GetBool("Sword") == true)
            {
                if (Physics.Raycast(ray, out hit, 4))
                {
                    if (hit.collider.tag == "Stone")
                    {
                        Debug.Log("Stone HIT");
                        isStone = true;
                        player.GetComponent<PlayerStatus>().StoneAmout(hit.collider.GetComponent<Stone>().GetAmount());
                        hit.collider.GetComponent<Stone>().Damage();
                    }
                    else if (hit.collider.tag == "Tree")
                    {
                        Debug.Log("Wood HIT");
                        isWood = true;
                        player.GetComponent<PlayerStatus>().WoodAmout(hit.collider.GetComponent<Tree>().GetAmount());
                        hit.collider.GetComponent<Tree>().Damage();
                    }
                }
            }
        }

        isStone = false;
        isWood = false;
    }
    
}
