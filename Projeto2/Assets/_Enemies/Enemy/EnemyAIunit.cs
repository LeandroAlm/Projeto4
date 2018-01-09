using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IPCA.AI.DecisionTrees;

public class EnemyAIunit : MonoBehaviour {

    Vector3 posDestino;
    float distance, takeTime;
    public Transform GOTarget;
    public Transform player;
    Grid grid;
    public GameObject pathFindingObj;


    void Start ()
    {
        grid = pathFindingObj.gameObject.GetComponent<Grid>();
        posDestino = transform.position;
        takeTime = 11.0f;
    }
	

	void Update ()
    {
        distance = Vector3.Distance(transform.position, player.position);
        DecisionIdle();
    }

    void DecisionIdle()
    {
        // Inimigo branco
        DTBinaryDecision tree = new DTBinaryDecision(
                () => { return distance > 10; },
                new DTAction(() =>
                {
                    // Mover aleatoriamente de x em x segundos
                    //Debug.Log("Estou a ir random!!!");
                  
                    MoveRandom();
                }),
                new DTAction(() =>
                {
                    // Caso esteja < 10
                    Debug.Log("Estou a ser def!!!");
               

                    GoToPlayer();
                })
            );
        tree.MakeDecision().Run();
    }

    void GoToPlayer()
    {
        // AQUI IRA TER UMA ARVORE DE DECISAO, PARA SIMPLIFICAR ESTA A ANDAR SO ATRAZ DO PLAYER
        // decisao de se estiver longe continua a andar atraz dele, se ja estiver perto ataca!

        //float speed = 1f;
        //float step = speed * Time.deltaTime;
        //transform.position = Vector3.MoveTowards(transform.position, player.position + Vector3.up / 2, step);
        //// Correção de olhar
        //transform.LookAt(player.position);
        transform.GetComponent<Unit>().target = player;
    }

    void MoveRandom()
    {
        if (transform.position != posDestino)
        {
            //float speed = 1f;
            //float step = speed * Time.deltaTime;
            //enemy.position = Vector3.MoveTowards(enemy.position, posDestino, step);
            //// Correção de olhar
            //enemy.LookAt(posDestino);
            Debug.Log("A ir para um Random!!!");

            //NODE------------------------------------------------------------------------------------
            Node node = grid.NodeFromWorldPoint(posDestino);
            GOTarget.position = node.worldPosition;
            
            transform.GetComponent<Unit>().playerPos = posDestino;
            transform.GetComponent<Unit>().CalculateWay();
        }
        else
        {
            Debug.Log("A dormir uma cesta!!!");
            if (takeTime > 10.0f)
            {
                takeTime = 0.0f;
                posDestino = RandomPos();
            }
            else
                takeTime += Time.deltaTime;
        }
    }

    Vector3 RandomPos()
    {
        float x = Random.Range(transform.position.x - 20, transform.position.x + 20);
        float z = Random.Range(transform.position.z - 20, transform.position.z + 20);
        return (new Vector3(x, 1f, z));
    }
}
