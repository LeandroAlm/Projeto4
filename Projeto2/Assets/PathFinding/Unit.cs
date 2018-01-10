using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{
    public Transform target;
    public static Transform SecondTarget;
    float speed = 5;
    Vector3[] path;
    int targetIndex;

    public Vector3 playerPos;

    Vector3 currentWaypoint;

    Vector3 direction;

    float distance;

    public static bool lastPoint;

    bool newRotation, wallisDown;

    public static bool wallBuilded;

    bool canGo;

    GameObject obj;

    void Start()
    {
        canGo = true;
        wallBuilded = false;
        wallisDown = false;
        newRotation = false;
        lastPoint = false;
        playerPos = target.position;
        SecondTarget = null;

        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
    }

    private void Update()
    {
        obj = GameObject.FindGameObjectWithTag("parentObj");

        //if(wallBuilded)
        //{
        //    PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
        //    wallBuilded = false;
        //}
        if (playerPos != target.position)
        {
            if (canGo)
            {
                PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
                speed = 5;
                playerPos = target.position;
            }
            else
            {
                Debug.Log("PAREDE!!!!!");
                WolfAnimController.CloseToWall = true;
                PathRequestManager.RequestPath(transform.position, SecondTarget.position, OnPathFound);
                Debug.Log("WallPos: " + SecondTarget.position);
            }
        }

        distance = Vector3.Distance(this.transform.position, target.position);
    
        if(!newRotation)
        { 
            direction = currentWaypoint - this.transform.position;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
            Quaternion.LookRotation(direction), 0.18f);
        }
        else
        {
            Vector3 newdirection = target.position - this.transform.position;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
            Quaternion.LookRotation(newdirection), 0.18f);
        }



        //if (distance > 3 && lastPoint)
        //{  
        //    PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
            
        //    lastPoint = false;
        //}

        

    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {

        if (pathSuccessful)
        {
            canGo = true;
            path = newPath;
            targetIndex = 0;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
        else
        {
            canGo = false;
        }
    }

    IEnumerator FollowPath()
    {
        
        currentWaypoint = path[0];
        while (true)
        {
            if (transform.position == currentWaypoint)
            {               
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    targetIndex = 0;
                    path = new Vector3[0];
                    newRotation = true;                   
                    //newRotation = true;
                    lastPoint = true;
                    Debug.Log("target index" + targetIndex);
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }

            Move();

            yield return null;

        }
    }

    public void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);

    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}