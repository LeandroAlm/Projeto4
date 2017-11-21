using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildWall2 : MonoBehaviour {

    public bool canBuild;

    public bool auxCheck;

    public bool IsBuilding;

    public GameObject wallPrefab;
    public GameObject wallPrefabGreen;

    GameObject ParentObj;

    private bool check;

    private Vector3 posIni;
    private Vector3 posEnd;


    private Vector3 dir;

    private Vector3 posAux2;

    private float timer;

    private List<Vector3> WallPositions = new List<Vector3>();

    int x;

    private int currentBuildStep;

    private int stepCount;

    public float stepDuration;

    //NOVO CODIGO
    private Vector3 nextPos;
    //public float mousePosX;
    bool isDrawing;
    
    bool draw;

    bool move1slot;

    private Vector3 newDir;

    public Vector3 mouseVector;
    public Vector3 LastMouseVector;


    void Start()
    {
        x = 0;

        check = false;
        canBuild = true;
        auxCheck = false;

        ParentObj = new GameObject();

        timer = stepDuration;

        currentBuildStep = 0;

        isDrawing = false;

        draw = false;
    }

    void Update()
    {

        if (canBuild)
        {
            if (Input.GetMouseButtonDown(0) && check == false)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    isDrawing = true;
                    posIni = hit.point;
                    nextPos = posIni;
                    check = true;
                }


            }
            else if (Input.GetMouseButtonUp(0) && check == true)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit2;
                if (Physics.Raycast(ray, out hit2))
                {
                    isDrawing = false;

                    posEnd = hit2.point;
                    check = false;
                    auxCheck = true;
                    IsBuilding = true;
                    draw = false;
                }
            }

           

            dir = posEnd - posIni;
            int size = (int)dir.magnitude;

            if (size % 2 != 0)
            {
                size++;
            }
            Vector3 dirAux = (dir / size) * 2.6f;
            Vector3 posAux = posIni;
            float sizeAux = 0;
            dir = Quaternion.Euler(0, -90, 0) * dir;
            Quaternion xy = Quaternion.LookRotation(dir);


            if (isDrawing)
            {
                mouseVector = posIni - Input.mousePosition;
                LastMouseVector = mouseVector;

                int sizeMouseVector = (int)mouseVector.magnitude;
                int sizeLastMouseVector = sizeMouseVector - 2;

                Debug.Log("sizeMouseVector" + sizeMouseVector);

                Debug.Log("sizeLastMouseVector" + sizeLastMouseVector);


                if (sizeMouseVector < sizeLastMouseVector)
                {
                    Debug.Log("tow");

                    sizeLastMouseVector = sizeMouseVector;


                }
                else
                {
                    sizeLastMouseVector = sizeMouseVector;
                    move1slot = true;

                }
                //if (sizeMousePosIni)
                //{
                //    move1slot = true;
                //}
            }
            //if(Mathf.Abs(Input.GetAxis("Mouse X")) > 0.7f)
            //{

            //    move1slot = true;
            //}

            newDir = nextPos - posIni;
            newDir = Quaternion.Euler(0, -90, 0) * newDir;
            Quaternion newXy = Quaternion.LookRotation(newDir);

            if (move1slot)
            {
               
                nextPos = nextPos + new Vector3(2.6f, 0, 0);
                GameObject newWallGreen = Instantiate(wallPrefabGreen, nextPos, newXy);
                Debug.Log("NextPos " + nextPos);
                move1slot = false;


            }
        

            //if (draw)
            //{
               
            //    //isDrawing = false;
            //    draw = false;
               

            //    //nextPos += dirAux;
            //}

            //if (auxCheck)
            //{
            //    while (sizeAux != size)
            //    {
            //        GameObject newWallGreen = Instantiate(wallPrefabGreen, posAux, xy);
            //        newWallGreen.transform.parent = ParentObj.transform;
            //        //WallPositions.Add(posAux);
            //        posAux += dirAux;
            //        sizeAux += 2;


            //    }
            //    stepCount = ParentObj.transform.childCount;

            //    posAux2 = posIni;
            //    auxCheck = false;


            //}






            //INSTANCIAR PAREDES UMA A UMA
            //IsBuilding = true;




            //if (timer <= 0 && currentBuildStep < stepCount)
            //{
            //    timer = stepDuration;
            //    Instantiate(wallPrefab, ParentObj.transform.GetChild(currentBuildStep).transform.position, ParentObj.transform.GetChild(currentBuildStep).transform.rotation);
            //    ParentObj.transform.GetChild(currentBuildStep).gameObject.SetActive(false);
            //    currentBuildStep += 1;

            //}

            //timer -= Time.deltaTime;

        }


    }




}
