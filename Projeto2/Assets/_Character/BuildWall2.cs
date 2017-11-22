using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildWall2 : MonoBehaviour
{

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

    private float vectorMagnitude;

    private Vector3 distance;

    private Vector3 MousePosX;

    private Vector3 firstInstance;

    int sizeDistance;

    bool is2;

    GameObject fence;

    Quaternion newXy;

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
            //Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
            //RaycastHit hit4;
            //if (Physics.Raycast(ray2, out hit4))
            //{
            //    firstInstance = new Vector3(hit4.point.x, 0, hit4.point.z);

            //}

            //if (Input.GetKeyDown(KeyCode.Alpha2))
            //{
            //    is2 = true;

            //    fence = Instantiate(wallPrefabGreen, firstInstance, Quaternion.identity);

            //}

            //if (is2)
            //{


            //    fence.transform.position = firstInstance;

            //}


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


            if (isDrawing)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit2;
                if (Physics.Raycast(ray, out hit2))
                {
                    mouseVector = new Vector3(hit2.point.x, 0, hit2.point.z);
                }
                Debug.Log("mouse Vector " + mouseVector);

                distance = mouseVector - posIni;

                //Vector3 distance2 = posIni - mouseVector;

                //int sizeDistance2 = (int)distance2.magnitude;

                sizeDistance = (int)distance.magnitude;

                for (int i = 0; i < 10; i++)
                {
                    if (sizeDistance > 1f)
                    {
                        Debug.Log("distance" + sizeDistance);
                        move1slot = true;
                        sizeDistance = 0;
                        MousePosX = mouseVector;
                        newDir = mouseVector - posIni;
                        newDir = Quaternion.Euler(0, -90, 0) * newDir;
                         newXy = Quaternion.LookRotation(newDir);
                        posIni = MousePosX;
                    }

                    //if (sizeDistance2 > 1f)
                    //{
                    //    Debug.Log("aiaiaiaiaiaiaiiaia distance2 = " + distance2);

                    //    //move1slot = true;
                    //    //sizeDistance = 0;
                    //    //MousePosX = mouseVector;
                    //    //posIni = MousePosX;
                    //}


                }


            }

            

           

            if (move1slot)
            {

                nextPos = (mouseVector + new Vector3(2.9f, 0, 0));
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
