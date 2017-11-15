using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildWall : MonoBehaviour {

    public bool canBuild;

    public bool auxCheck;

    public GameObject wallPrefab;

    private bool check;

    public Vector3 posIni;
    public Vector3 posEnd;
    public Vector3 dir;

	void Start ()
    {
        check = false;
        canBuild = false;
        auxCheck = false;
    }
	
	void Update ()
    {
        if(canBuild)
        {
            if(Input.GetMouseButtonDown(0) && check == false)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    posIni = hit.point;
                    check = true;
                }
               
            }
            else if (Input.GetMouseButtonUp(0) && check == true)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    posEnd = hit.point;
                    check = false;
                    auxCheck = true;
                }
            }

            dir = posEnd - posIni;
            int size = (int)dir.magnitude;
            if(size % 2 != 0)
            {
                size++;
            }
            Vector3 dirAux = (dir / size) * 2.82f;

            Vector3 posAux = posIni;
            float sizeAux = 0;
            dir = Quaternion.Euler(0, -90, 0) * dir;
            Quaternion xy = Quaternion.LookRotation(dir);

            if (auxCheck)
            {
                while (sizeAux != size)
                {
                    Debug.Log("eNTROU");
                    Instantiate(wallPrefab, posAux, xy);
                    posAux += dirAux;
                    sizeAux += 2;
                }
                auxCheck = false;
                Debug.Log("sAUI");
            }

            //Instantiate(wallPrefab, posIni, this.transform.rotation);

        }

    }
}
