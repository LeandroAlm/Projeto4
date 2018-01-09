using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{

    public Transform target;
    float speed = 3f;
    Vector3[] path;
    int targetIndex;

    Vector3 currentWaypoint;

    Vector3 direction;

    Vector3 lastVector;

    Vector3 playerPos;

    //public static bool isAttacking;

    void Start()
    {
        playerPos = target.position;
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
        //isAttacking = false;
    }

    private void Update()
    {
        // Quando esta em persiguição
        if (playerPos != target.position)
        {
            // Muda a Pos logo tem de recalcular o caminho!
            PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
            playerPos = target.position;
        }

        //Inimigo vira se para os nodos principais para um movimento mais realista
        direction = currentWaypoint - this.transform.position;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
        Quaternion.LookRotation(direction), 0.17f);
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            targetIndex = 0;
            StopCoroutine(FollowPath());

            StartCoroutine(FollowPath());

        }
    }

    IEnumerator FollowPath()
    {
        Debug.Log("lastVector" + lastVector);

        currentWaypoint = path[0];
        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }


            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);

            yield return null;
        }
    }

    public void CalculateWay()
    {
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
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