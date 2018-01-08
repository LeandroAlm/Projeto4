using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLight : MonoBehaviour
{
    public Light light;

    public float range;
    public float duration;
    
	void Start ()
	{
	    light = GetComponent<Light>();
	    range = light.range;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    float amplitude = Mathf.PingPong(Time.time, duration);

	    amplitude = amplitude / duration * 0.5f + 0.5f;
	    range = range * amplitude;
	}
}
