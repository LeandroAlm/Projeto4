using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildWall2 : MonoBehaviour
{

    public static bool canBuild;

    public bool auxCheck;

    public bool IsBuilding;

    public GameObject wallPrefab;
    public GameObject wallPrefabGreen;

    public GameObject wallPrefabCursor;


    GameObject ParentObj;

    GameObject WallsList;

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

    public Vector3 _lastDir;

    public Quaternion lastDir;


    //public float mousePosX;
    public static bool isDrawing;

    bool draw;

    bool move1slot;

    private Vector3 newDir;

    public Vector3 mouseVector;
    public Vector3 LastMouseVector;

    private float vectorMagnitude;

    private Vector3 distance;

    private Vector3 MousePosX;

    private Vector3 firstInstance;

    float sizeDistance;

    public static bool is2;

    GameObject fence;

    Quaternion newXy;

    int size = 0;

    public static bool isBuilding;

    Foundation foundationScript;

    public static bool firstFence;

    int countDisableWalls;

    void Start()
    {
        x = 0;

        check = false;
        canBuild = false;
        auxCheck = false;

        ParentObj = new GameObject();

        WallsList = new GameObject();

        timer = stepDuration;

        currentBuildStep = 0;

        isDrawing = false;

        draw = false;

        //fence = wallPrefabGreen.gameObject;

       

    }

    void Update()
    {
        if (firstFence == true)
        {


            fence = Instantiate(wallPrefabCursor, Vector3.zero, Quaternion.identity);
            canBuild = true;
            firstFence = false;
        }
        //Debug.Log(canBuild);
        if (canBuild)
        {
          
            if (Input.GetMouseButtonDown(0) && check == false)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    is2 = false;
                    //fence.gameObject.SetActive(false);
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
                    //fence.gameObject.SetActive(true);

                    isDrawing = false;

                    posEnd = hit2.point;
                    check = false;
                    auxCheck = true;
                    draw = false;
  
                    SnapWalls();
                    
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

                //possível erro na distância

                distance = mouseVector - posIni;


                sizeDistance = (float)distance.magnitude;

                if(Input.GetMouseButtonDown(0))
                {
                    move1slot = true;
                    nextPos = fence.gameObject.transform.position;
                }

                //mouseVector = snapPosition(getWorldPoint());

                for (int i = 0; i < sizeDistance - 1; i++)
                {
                    if (Mathf.Abs(Input.GetAxis("Mouse X")) < 0.8f)
                    {
                        if (sizeDistance > 2.6f)
                        {

                            nextPos = fence.gameObject.transform.position;

                            size++;

                            move1slot = true;
                            
                            //MousePosX = mouseVector;
                            newDir = mouseVector - posIni;
                            newDir = Quaternion.Euler(0, -90, 0) * newDir;
                            newXy = Quaternion.LookRotation(newDir);
                            posIni = mouseVector;
                            sizeDistance = 0;
                        }
                    }
                }

                fence.transform.rotation = newXy;

                //_lastDir =(nextPos + mouseVector) -  nextPos;
                //lastDir = newXy;
                //Debug.Log("lastPOS" + distance);

                //Debug.Log("nextPos" + nextPos);

            }
           
            if (move1slot)
            {
                Foundation.isInstantiated = true;
                GameObject newWallGreen = Instantiate(wallPrefabGreen, nextPos, newXy);
                newWallGreen.transform.parent = ParentObj.transform;

                move1slot = false;


            }

            stepCount = ParentObj.transform.childCount;


            if (timer <= 0 && currentBuildStep < stepCount)
            {

                timer = stepDuration;
                GameObject WoodWall =  Instantiate(wallPrefab, ParentObj.transform.GetChild(currentBuildStep).transform.position, ParentObj.transform.GetChild(currentBuildStep).transform.rotation);
                WoodWall.transform.parent = WallsList.transform;
                ParentObj.transform.GetChild(currentBuildStep).gameObject.SetActive(false);
                currentBuildStep += 1;
                countDisableWalls++;

            }

            timer -= Time.deltaTime;
            //canBuild = false;

            Debug.Log("fff"+ stepCount);
        }
    }

    void SnapWalls()
    {

        float distance;
        Transform firstWoodWall;
        Transform lastGreenWall;
        Vector3 newDir;
        Quaternion newXy;

        firstWoodWall = WallsList.transform.GetChild(0).GetChild(0).transform;

        lastGreenWall = ParentObj.transform.GetChild(stepCount - 1).transform;

        //firstWoodWall = WallsList.transform.GetChild(0).GetChild(0).transform;


        //lastGreenWall = ParentObj.transform.GetChild(stepCount - countDisableWalls + 1).transform;


        //Debug.Log("firstWoodWall" + WallsList.transform.GetChild(0).GetChild(0).gameObject.transform.position);

        //Debug.Log("lastGreenWall" + ParentObj.transform.GetChild(stepCount - countDisableWalls + 1).gameObject.transform.position);

        distance = Vector3.Distance(lastGreenWall.transform.position, firstWoodWall.transform.position);

        newDir = lastGreenWall.transform.position - firstWoodWall.transform.position;
        newDir = Quaternion.Euler(0, -90, 0) * newDir;
        newXy = Quaternion.LookRotation(newDir);

        Debug.Log("size distance = " + distance);

        if(distance < 7)
        {
            Instantiate(wallPrefab, ParentObj.transform.GetChild(stepCount - 1).transform.position, newXy);
            Debug.Log("vaca");
        }

    }
}