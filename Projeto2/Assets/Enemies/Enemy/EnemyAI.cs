using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    float distance;
    Vector3 posDestino;
    float takeTime;

	void Start ()
    {
        posDestino = transform.position;
        takeTime = 11.0f;
    }
	

	void Update ()
    {
        distance = Vector3.Distance(transform.position, player.position);

        Debug.Log("Inipos: " + transform.position);
        Debug.Log("Pos ->: " + posDestino);
        IAStart();
    }

    void IAStart ()
    {
        if (distance < 10)
        {
            // Distancia para reconhecer o player
            if(distance > 2)
            {
                Move(player.position);
            }
            else
            {
                Attack();
            }

            // Correção de olhar
            transform.LookAt(player.position);

        }
        else
        {
            // Longe de player, toma outra decisão
            if (transform.position != posDestino)
            {
                Debug.Log("Entrei");

                Move(posDestino);
            }
            else
            {
                if (takeTime > 10.0f)
                {
                    takeTime = 0.0f;
                    float x = Random.Range(transform.position.x - 20, transform.position.x + 20);
                    float z = Random.Range(transform.position.z - 20, transform.position.z + 20);
                    posDestino = new Vector3(x, 1f, z);
                }
                else
                    takeTime += Time.deltaTime;
            }
        }
    }

    void Move(Vector3 pos)
    {
        // Mover o enemiy para pos
        Debug.Log("Moving to: " + pos);
        float speed = 1f;
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, pos, step);
    }

    void Attack()
    {

    }
}
