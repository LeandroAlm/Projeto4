using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildWall : MonoBehaviour {

    public bool canBuild;

    public bool auxCheck;

    public bool IsBuilding;

    public GameObject wallPrefab;
    public GameObject wallPrefabGreen;

    GameObject ParentObj;

    private bool check;

    public Vector3 posIni;
    public Vector3 posEnd;
    public Vector3 dir;

    public Vector3 posAux2;

    private float timer;

    private List<Vector3> WallPositions = new List<Vector3>();

    int x;

    private int currentBuildStep;

    private int stepCount;

    public float stepDuration;

    void Start ()
    {
        x = 0;
     
        check = false;
        canBuild = true;
        auxCheck = false;

        ParentObj = new GameObject();

        timer = stepDuration;
        
        currentBuildStep = 0;
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
                    IsBuilding = true;

                }
            }

            dir = posEnd - posIni;
            int size = (int)dir.magnitude;
            Debug.Log("dir"+dir);
            if(size % 2 != 0)
            {
                size++;
            }
            Vector3 dirAux = (dir / size) * 2.83f;

            Vector3 posAux = posIni;
            float sizeAux = 0;
            dir = Quaternion.Euler(0, -90, 0) * dir;
            Quaternion xy = Quaternion.LookRotation(dir);

            if (auxCheck)
            {
                while (sizeAux != size)
                {
                    GameObject newWallGreen = Instantiate(wallPrefabGreen, posAux, xy);
                    newWallGreen.transform.parent = ParentObj.transform;
                    //WallPositions.Add(posAux);
                    posAux += dirAux;
                    sizeAux += 2;

                   
                }
                stepCount = ParentObj.transform.childCount;

                posAux2 = posIni;
                auxCheck = false;

               
            }

            for (int i = 0; i < ParentObj.transform.childCount; i++)
            {
               Debug.Log(ParentObj.transform.GetChild(i));
            }




            //INSTANCIAR PAREDES UMA A UMA
            //Bugado
            IsBuilding = true;

            Debug.Log(IsBuilding);


            if (timer <= 0 && currentBuildStep < stepCount)
            {
                timer = stepDuration;
                Instantiate(wallPrefab, ParentObj.transform.GetChild(currentBuildStep).transform.position, ParentObj.transform.GetChild(currentBuildStep).transform.rotation);
                ParentObj.transform.GetChild(currentBuildStep).gameObject.SetActive(false);
                currentBuildStep += 1;

            }

            timer -= Time.deltaTime;

        }
   

    }




}
