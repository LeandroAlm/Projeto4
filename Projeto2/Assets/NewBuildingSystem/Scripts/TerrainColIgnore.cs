using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainColIgnore : MonoBehaviour
{


     GameObject terrain;

    // Use this for initialization
    void Start()
    {
        terrain = GameObject.FindGameObjectWithTag("terrain");
    }

    // Update is called once per frame
    void Update()
    {
        Physics.IgnoreCollision(terrain.GetComponent<Collider>(), this.GetComponent<Collider>(), true);

    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == 8)
        {

            //this.GetComponent<Collider>().enabled = false;
        }

    }
}
