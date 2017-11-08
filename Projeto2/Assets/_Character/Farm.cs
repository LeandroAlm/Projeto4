using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour {

    Animator anim;
    private Transform player;
    private Terrain terreno;
    private GameObject go;
    
    public TreeInstance treeInstance;
    public GUIContent Mochila;

    Ray ray;
    Vector3 forward;

	// Use this for initialization
	void Start ()
    {
        player = GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }
	
	void Update ()
    {
        RaycastHit hit;
		
        forward = transform.TransformDirection(Vector3.forward * 10 + new Vector3(0,1,0));
        Debug.DrawRay(transform.position, forward, Color.green);
        

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            
            if (anim.GetBool("Sword") == true)
            {
                if (Physics.Raycast(ray, out hit, 4))
                {
                    if (hit.collider.tag == "Stone")
                    {
                        hit.collider.GetComponent<Stone>().Damage();
                        player.GetComponent<PlayerSatatus>().StoneAmout(hit.collider.GetComponent<Stone>().GetAmount());
                    }
                    if (hit.collider.tag == "Tree")
                    {
                        hit.collider.GetComponent<Tree>().Damage();
                        player.GetComponent<PlayerSatatus>().WoodAmout(hit.collider.GetComponent<Tree>().GetAmount());
                    }
                }
            }
        }
    }
    
}
