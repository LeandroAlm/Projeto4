﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectMeat : MonoBehaviour {
    
	void Start () {
		
	}
	

	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            Debug.Log("Colidi!!!!!!!!!!!!!");
            collision.gameObject.GetComponent<PlayerStatus>().MeatAmount(1);
            Destroy(gameObject, 1);
        }
    }
}
