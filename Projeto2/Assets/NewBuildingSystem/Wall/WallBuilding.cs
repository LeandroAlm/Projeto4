using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBuilding : MonoBehaviour {

    bool creating;
    ShowMouse pointer;
    public GameObject polePrefab;
    public GameObject fencePrefab;
    GameObject lastPole;

    void Start () {
        pointer = GetComponent<ShowMouse>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        getInput();
		
	}

    void getInput()
    {
        if(Input.GetMouseButton(0))
        {
            startFence();
        }
        else if( Input.GetMouseButtonUp(0))
        {
            setFence();
        }
        else
        {
            if(creating)
            {
                updateFence();
            }
        }

    }

    void startFence()
    {
        creating = true;
        Vector3 startPos = pointer.getWorldPoint();
        startPos = pointer.snapPosition(startPos);
        GameObject startPole = Instantiate(polePrefab, startPos, Quaternion.identity);
        startPole.transform.position = new Vector3(startPos.x, startPos.y + 0.3f, startPos.z);
        lastPole = startPole;
    }

    void setFence()
    {
        creating = false;
    }

    void updateFence()
    {
        Vector3 current = pointer.getWorldPoint();
        current = pointer.snapPosition(current);
        current = new Vector3(current.x, current.y + 0.3f, current.z);
        if(!current.Equals(lastPole.transform.position))
        {
            creatFenceSegment(current);
        }
           

    }

    void creatFenceSegment(Vector3 current)
    {
        GameObject newPole = Instantiate(polePrefab, current, Quaternion.identity);
        //calcular ponto entre os dois poles
        Vector3 middle = Vector3.Lerp(newPole.transform.position, lastPole.transform.position, 0.5f);
        GameObject newFence = Instantiate(fencePrefab, middle, Quaternion.identity);
        newFence.transform.LookAt(lastPole.transform);
        lastPole = newPole;
    }
}
    //public Vector3 getWorldPoint()
    //{
    //    Camera cam = getc
    //}


