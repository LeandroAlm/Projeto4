using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class TowerControl : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPosition;
    private Transform target;

    public float fireRate = 5f;
    public float fireCountDown = 0f;
    public float range = 50f;
    
    public string enemyTag = "Enemy";

    void Start()
    {
        InvokeRepeating("DistanceToEnemy", 0f, 0.5f);
    }

    void Update()
    {
        if (target == null)
            return;

        DistanceToEnemy();

        if (fireCountDown <= 0f)
        {
            Fire();

            fireCountDown = 1f / fireRate;
        }

        fireCountDown -= Time.deltaTime;
    }

    void DistanceToEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        GameObject nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
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
