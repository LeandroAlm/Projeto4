using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private List<Transform> targets = new List<Transform>();
    private Transform target;

    public float speed = 300f;
    public int bulletDamage = 5;
    private string enemyTag = "Enemy";
    private string enemyTag2 = "Enemy2";

    public void GetTarget(Transform enemyTarget)
    {
        target = enemyTarget;
        targets.Add(target);
    }

    void Update()
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
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (Transform target in targets)
        {
            if (collision.gameObject.tag == enemyTag)
            {
                Debug.Log("Fode-te boi, lobo da merda!");
                collision.gameObject.GetComponent<WolfAnimController>().GetDamage(bulletDamage);
                Destroy(gameObject);
            }
            else if (collision.gameObject.tag == enemyTag)
            {
                Debug.Log("Fode-te boi, lobisomem da merda");
                collision.gameObject.GetComponent<WerewolfAnimController>().GetDamage(bulletDamage);
                Destroy(gameObject);
            }
        }
    }
}
