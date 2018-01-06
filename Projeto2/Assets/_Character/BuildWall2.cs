using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildWall2 : MonoBehaviour
{
    public static bool canBuild, isDrawing, isPlaced;

    public bool IsBuilding, check, move1slot;

    public GameObject wallPrefab, wallPrefabGreen, wallPrefabCursor, ParentObj, fence;

    private Vector3 posIni, posEnd, nextPos, newDir, mouseVector, distance;
 
    private int currentBuildStep, stepCount;

    public float stepDuration;

    private float sizeDistance;

    private Quaternion newXy;

    RaycastHit hit;

    Ray ray;


    Grid grid;

    public GameObject pathFindingObj;
    void Start()
    {
        pathFindingObj = GameObject.FindGameObjectWithTag("A");

        grid = pathFindingObj.gameObject.GetComponent<Grid>();


        check = false;

        canBuild = false;
        
        ParentObj = new GameObject();

        currentBuildStep = 0;

        isDrawing = false;
    }

    void Update()
    {
        FollowMouse();

        RotateWall();

        if (canBuild)
        {
            fence = this.transform.gameObject;

            if (Input.GetMouseButtonDown(0) && check == false)
            {
                if (RayShooter())
                {       
                    isDrawing = true;
                    posIni = hit.point;
                    nextPos = posIni;
                    check = true;
                }
            }

            else if (Input.GetMouseButtonUp(0) && check == true)
            {              
                if (RayShooter())
                {
                    fence.gameObject.SetActive(false);         
                    isDrawing = false;
                    posEnd = hit.point;
                    check = false;
                    SnapWalls();
                    ParentObj.AddComponent<WallAutoBuild>();
                    ParentObj.GetComponent<WallAutoBuild>().wallPrefab = wallPrefab;
                    ParentObj.GetComponent<WallAutoBuild>().stepDuration = 1.5f;
                }
            }

            if (isDrawing)
            {              
                if (RayShooter())
                {
                    mouseVector = new Vector3(hit.point.x, 0, hit.point.z);
                }

                distance = mouseVector - posIni;

                sizeDistance = (float)distance.magnitude;

                if (Input.GetMouseButtonDown(0))
                {
                    newXy = fence.gameObject.transform.rotation;
                    nextPos = fence.gameObject.transform.position;
                    InstantiateWallGreen();
                }

                MoveOneSlot();
                
                fence.transform.rotation = newXy;
            }
          
            stepCount = ParentObj.transform.childCount;
        }

    }


    void RotateWall()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            this.transform.Rotate(Vector3.up * 250 * Time.deltaTime, Space.World);
        }

        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            this.transform.Rotate(Vector3.up * -250 * Time.deltaTime, Space.World);
        }
    }

    void MoveOneSlot()
    {
        for (int i = 0; i < sizeDistance - 1; i++)
        {
            if (sizeDistance > 2.6f)
            {

                
                nextPos = fence.gameObject.transform.position;

                newDir = mouseVector - posIni;
                newDir = Quaternion.Euler(0, -90, 0) * newDir;

                newXy = Quaternion.LookRotation(newDir);

                posIni = mouseVector;

                sizeDistance = 0;


                InstantiateWallGreen();
            }
        }
    }

    void InstantiateWallGreen()
    {
        Foundation.isInstantiated = true;
        GameObject newWallGreen = Instantiate(wallPrefabGreen, nextPos, newXy);
        newWallGreen.transform.parent = ParentObj.transform;
    }

    bool RayShooter()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100f, 1 << 8))
        {
            if (Input.GetMouseButton(0))
            {
                Node node = grid.NodeFromWorldPoint(hit.point);
                node.walkable = false;
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    void FollowMouse()
    {
        if (!isPlaced && this.transform.tag == "Foundation")
        {
            BuildingManager.isBuilding = true;

            if (RayShooter())
            {
                /*this.transform.position = new Vector3(hit.point.x, 0, hit.point.z + 0.5f);*///hit.point.z
                                                                                              //InitialRot = this.transform.rotation;
                this.transform.position = new Vector3(hit.point.x + 1.5f, 0, hit.point.z - 0.5f);
                //transform.LookAt(player.transform.position);
            }

            canBuild = true;
        }
    }

    void SnapWalls()
    {
        float distance;

        Transform firstGreenWall;
        Transform lastGreenWall;
        Vector3 newDir;
        Quaternion newXy;

        lastGreenWall = ParentObj.transform.GetChild(stepCount - 1).transform;

        firstGreenWall = ParentObj.transform.GetChild(0).transform;

        distance = Vector3.Distance(lastGreenWall.transform.position, firstGreenWall.transform.position);

        newDir = lastGreenWall.transform.position - firstGreenWall.transform.position;
        newDir = Quaternion.Euler(0, -90, 0) * newDir;
        newXy = Quaternion.LookRotation(newDir);

        if(distance < 7)
        {
            GameObject SnapFence = Instantiate(wallPrefabGreen, ParentObj.transform.GetChild(stepCount - 1).transform.position, newXy);
            SnapFence.transform.parent = ParentObj.transform;
        }
    }
}