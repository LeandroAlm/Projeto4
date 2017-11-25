using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IPCA.AI.DecisionTrees;

public class EnemySmartAI : MonoBehaviour
{
    public Transform player;
    Vector3 posDestino;
    float takeTime;


    void Start ()
    {
        posDestino = transform.position;
        takeTime = 11.0f;
    }
	

	void Update ()
    {
        DecisionIdle();
    }

    void DecisionIdle()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        DTBinaryDecision tree = new DTBinaryDecision(
                () => { return distance > 10; },
                new DTAction(() =>
                {
                    // Mover aleatoriamente de x em x segundos
                    Debug.Log("Move random!");
                    MoveRandom();
                }),
                new DTAction(() =>
                {
                    // Caso esteja < 10
                    Debug.Log("I'm watching you!");
                    GoToPlayer();
                })
            );
        tree.MakeDecision().Run();
    }

    void MoveRandom()
    {
        if (transform.position != posDestino)
        {
            Debug.Log("Moving to: " + posDestino);

            float speed = 1f;
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, posDestino, step);
            // Correção de olhar
            transform.LookAt(posDestino);
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

    void GoToPlayer()
    {
        // AQUI IRA TER UMA ARVORE DE DECISAO, PARA SIMPLIFICAR ESTA A ANDAR SO ATRAZ DO PLAYER
        // decisao de se estiver longe continua a andar atraz dele, se ja estiver perto ataca!

        float speed = 1f;
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, player.position, step);
        // Correção de olhar
        transform.LookAt(player.position);
    }
}
