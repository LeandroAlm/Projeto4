using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallWait : MonoBehaviour {

    public GameObject WallPrefab;

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Invoke("InstantiatPrefab", 2f);

    }

    void InstantiatPrefab()
    {
        Instantiate(WallPrefab, this.transform.position, this.transform.rotation);

    }
}
