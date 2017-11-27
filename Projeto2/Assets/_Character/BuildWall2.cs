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

    public static bool is2;

    GameObject fence;

    Quaternion newXy;

    int size = 0;

    public static bool isBuilding;

    public bool isPlaced;


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

        fence = wallPrefabGreen.gameObject;


    }

    void Update()
    {


        if (canBuild)
        {
            //Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
            //RaycastHit hit4;
            //if (Physics.Raycast(ray2, out hit4))
            //{
            //    Debug.Log("touuu");
            //    firstInstance = new Vector3(hit4.point.x, 0, hit4.point.z);

            //}

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {

                fence = Instantiate(wallPrefabCursor, Vector3.zero, Quaternion.identity);

            }

            //if (is2)
            //{


            //    is2 = false;

            //}

            fence.transform.position = firstInstance;

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
                    fence.gameObject.SetActive(false);
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
                    isPlaced = true;



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


                distance = mouseVector - posIni;


                sizeDistance = (int)distance.magnitude;


                //mouseVector = snapPosition(getWorldPoint());

                for (int i = 0; i < sizeDistance - 1; i++)
                {
                    if (Mathf.Abs(Input.GetAxis("Mouse X")) < 0.8f)
                    {
                        if (sizeDistance > 1.95f)
                        {

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

        }


    }



    public Vector3 getWorldPoint()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }
        return Vector3.zero;
    }

    public Vector3 snapPosition(Vector3 original)
    {
        Vector3 snapped;
        snapped.x = Mathf.Floor(original.x + 0.5f);
        snapped.y = Mathf.Floor(original.y + 0.5f);
        snapped.z = Mathf.Floor(original.z + 0.5f);
        return snapped;
    }


}