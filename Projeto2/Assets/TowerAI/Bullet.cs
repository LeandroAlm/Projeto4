using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public float speed = 5f;

    public void GetTarget(Transform enemyTarget)
    {
        target = enemyTarget;
    }

	void Update ()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;

        float distanceToMove = speed * Time.deltaTime;

        if (direction.magnitude <= distanceToMove)
        {
            HitEnemy();
            return;
        }

        transform.Translate(direction.normalized * distanceToMove, Space.World);
    }

    void HitEnemy()
    {
        Debug.Log("Hit");
        Destroy(gameObject, 3);
    }
}
