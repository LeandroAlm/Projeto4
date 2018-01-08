using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class TowerControl : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPosition;
    private Transform target;

    private bool canFire, isInstantiated;
    public float fireRate = 1f;
    public float fireCountDown = 0f;
    public float range = 10f;
    float shortestDistance = Mathf.Infinity;

    void Start ()
	{
	    canFire = false;
	}

	void Update ()
    {
        DistanceToEnemy();
        Holding();
    }

    void DistanceToEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }

        else
        {
            target = null;
            return;
        }
    }

    void Holding()
    {
        if (shortestDistance <= range && fireCountDown <= 0f)
        {
            canFire = true;

            Fire();

            fireCountDown = 1f / fireRate;
        }

        else
        {
            canFire = false;
        }

        fireCountDown -= Time.deltaTime;
    }

    void Fire()
    {
        Debug.Log("Shoot");
        GameObject bulletGameObject = (GameObject)Instantiate(bulletPrefab, bulletSpawnPosition.position, bulletSpawnPosition.rotation);

        Bullet bullet = bulletGameObject.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.GetTarget(target);
        }
    }
}
