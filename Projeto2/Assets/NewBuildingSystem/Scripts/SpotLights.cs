using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotLights : MonoBehaviour {

    private Light light;

    GameObject moon;

	void Start ()
    {
        moon = GameObject.Find("Moon");
        light = gameObject.GetComponent<Light>();
        light.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		

        if(moon.transform.position.y > 0)
        {
            light.enabled = true;
        }
        else
        {
            light.enabled = false;
        }
	}
}
