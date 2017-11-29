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

    public Vector3 lastPos;

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

    int sizeDistance;

    public static bool is2;

    GameObject fence;

    Quaternion newXy;

    int size = 0;

    public static bool isBuilding;

    Foundation foundationScript;

    public static bool firstFence;

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

        //fence = wallPrefabGreen.gameObject;

       

    }

    void Update()
    {

        Debug.Log(canBuild);
        if (canBuild)
        {
            Debug.Log("tiuuuu");
            //Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
            //RaycastHit hit4;
            //if (Physics.Raycast(ray2, out hit4))
            //{
            //    Debug.Log("touuu");
            //    firstInstance = new Vector3(hit4.point.x, 0, hit4.point.z);

            //}

            //if (is2)
            //{


            //    is2 = false;

            //}

            //fence.transform.position = firstInstance;
            if (firstFence == true)
            {
                fence = Instantiate(wallPrefabCursor, Vector3.zero, Quaternion.identity);
                firstFence = false;
            }

            if (Input.GetMouseButtonDown(0) && check == false)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    is2 = false;
                    fence.gameObject.SetActive(false);
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
                    fence.gameObject.SetActive(true);

                    isDrawing = false;

                    posEnd = hit2.point;
                    check = false;
                    auxCheck = true;
                    //IsBuilding = true;
                    draw = false;
                    //isPlaced = true;

                   
                    //LastMouseVector = mouseVector;
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


                sizeDistance = (int)distance.magnitude;


                //mouseVector = snapPosition(getWorldPoint());

                for (int i = 0; i < sizeDistance - 1; i++)
                {
                    if (Mathf.Abs(Input.GetAxis("Mouse X")) < 0.8f)
                    {
                        if (sizeDistance > 1.95f)
                        {
                            Debug.Log("yououuououo");

                            size++;
                            Debug.Log("distance" + sizeDistance);
                            move1slot = true;
                            sizeDistance = 0;
                            //MousePosX = mouseVector;
                            newDir = mouseVector - posIni;
                            newDir = Quaternion.Euler(0, -90, 0) * newDir;
                            newXy = Quaternion.LookRotation(newDir);
                            nextPos = mouseVector;
                            posIni = mouseVector;
                        }
                    }
                }

                fence.transform.rotation = newXy;

                //lastPos = nextPos + mouseVector;
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
                Instantiate(wallPrefab, ParentObj.transform.GetChild(currentBuildStep).transform.position, ParentObj.transform.GetChild(currentBuildStep).transform.rotation);
                ParentObj.transform.GetChild(currentBuildStep).gameObject.SetActive(false);
                currentBuildStep += 1;

            }

            timer -= Time.deltaTime;
            //canBuild = false;
        }
    }
}