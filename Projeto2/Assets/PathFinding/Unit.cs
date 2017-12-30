using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{
    Transform OldTarget;
    public Transform target;
    float speed = 1;
    Vector3[] path;
    int targetIndex;
    

    void Start()
    {
        target = transform;
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
        OldTarget = target;
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (OldTarget == target)
        {
            if (pathSuccessful)
            {
                path = newPath;
                targetIndex = 0;
                StopCoroutine("FollowPath");
                StartCoroutine("FollowPath");
            }
        }
        else
        {
            // Mudou de target temos de calcular de novo!
            PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
        OldTarget = target;
        }
        
    }

    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];
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

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.red;
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