using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObj : MonoBehaviour {

    int currentBuildStep;
    int stepCount;
    

    void Start ()
    {
        
        stepCount = transform.childCount;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (currentBuildStep < stepCount)
        {
           
            NextBuildStep();


        }

        
    }


    void NextBuildStep()
    {
        Rigidbody rb;

        rb=transform.GetChild(currentBuildStep).gameObject.AddComponent<Rigidbody>();
        rb.useGravity = true;
        rb.mass = 40;
        transform.GetChild(currentBuildStep).gameObject.GetComponent<MeshCollider>().enabled = true;
        transform.GetChild(currentBuildStep).gameObject.GetComponent<MeshCollider>().convex = true;

        currentBuildStep +=1;
    }
}
