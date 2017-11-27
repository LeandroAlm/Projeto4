using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IPCA.AI.DecisionTrees;

public class EnemySmartAI : MonoBehaviour
{
    float distance;
    public Transform player;
    Vector3 posDestino, covilpos;
    float takeTime, WaitThisTime;
    public Transform covil;
    private Dictionary<Transform, Vector3> bichos;
    private List<Vector3> positions;
    private int Rotate, etapa;

    void Start ()
    {
        covilpos = Vector3.zero;
        Rotate = 1;
        etapa = 0;
        posDestino = transform.position;
        takeTime = 11.0f;
        WaitThisTime = 0.0f;
        bichos = new Dictionary<Transform, Vector3>();
        bichos.Add(covil.GetChild(0), covil.GetChild(0).position);
        bichos.Add(covil.GetChild(1), covil.GetChild(1).position);
        bichos.Add(covil.GetChild(2), covil.GetChild(2).position);
        positions = new List<Vector3>();
        positions.Add(covil.GetChild(0).position);
        positions.Add(covil.GetChild(1).position);
        positions.Add(covil.GetChild(2).position);
    }
    

	void Update ()
    {
        distance = Vector3.Distance(transform.position, player.position);
        if (transform.tag == "Enemy")
            DecisionIdle();
        if (transform.tag == "Enemy2")
            DesicionRotation();

        //Debug.Log("Etapa: " + etapa);
    }

    void DecisionIdle()
    {
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

    void DesicionRotation()
    {
        DTBinaryDecision tree = new DTBinaryDecision(
                () => { return Rotate == 1; },
                new DTBinaryDecision(
                    () => { return etapa == 0 || etapa == 1; },
                    new DTAction(() =>
                    {
                        // bicho 1 mover
                        Debug.Log("Ai vai o bicho 1");
                        if (transform == covil.GetChild(0))
                        {
                            MoveRandom();
                        }
                    }),
                    new DTAction(() =>
                    {
                        // se nao é pq ja esta na pos random e tem de voltar ao covil
                        Debug.Log("Bicho 1 voltando...");
                        if (transform.position != covilpos)
                        {
                            BackCovil();
                        }
                        else
                        {
                            Rotate++;
                            etapa = 0;
                        }
                    })
                ),
                new DTBinaryDecision(
                    () => { return Rotate == 2; },
                    new DTBinaryDecision(
                    () => { return posDestino != positions[1]; },
                        new DTAction(() =>
                        {
                            // bicho 2 mover
                            Debug.Log("Ai vai o bicho 2");
                            MoveRandom();
                        }),
                        new DTAction(() =>
                        {
                            // se nao é pq ja esta na pos random e tem de voltar ao covil
                            if (transform.position == posDestino)
                            {
                                Debug.Log("Bicho 2 voltando...");
                            }
                        })
                    ),
                    new DTBinaryDecision(
                    () => { return posDestino != positions[2]; },
                        new DTAction(() =>
                        {
                            // bicho 3 mover
                            Debug.Log("Ai vai o bicho 3");
                            MoveRandom();
                        }),
                        new DTAction(() =>
                        {
                            // se nao é pq ja esta na pos random e tem de voltar ao covil
                            if (transform.position == posDestino)
                            {
                                Debug.Log("Bicho 3 voltando...");
                            }
                        })
                    )
                )
            );
        tree.MakeDecision().Run();
    }

    void MoveRandom()
    {
        if (transform.position != posDestino)
        {
            //Debug.Log("Moving to: " + posDestino);

            float speed = 1f;
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, posDestino, step);
            // Correção de olhar
            transform.LookAt(posDestino);
        }
        else
        {
            etapa++;
            if (takeTime > 10.0f)
            {
                takeTime = 0.0f;
                posDestino = RandomPos();
            }
            else
                takeTime += Time.deltaTime;
        }
    }

    void BackCovil()
    {
        
        // Voltar ao covil
       
            if (transform == covil.GetChild(0))
                covilpos = positions[0];
            else if (transform == covil.GetChild(1))
                covilpos = positions[1];
            else if (transform == covil.GetChild(2))
                covilpos = positions[2];
        if (WaitThisTime > 5.0f)
        {
            // andamento para o covil!!!!
            float speed = 1f;
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, covilpos, step);
            // Correção de olhar
            transform.LookAt(covilpos);
            WaitThisTime = 0.0f;
        }
        else
            WaitThisTime += Time.deltaTime;
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

    Vector3 RandomPos()
    {
        if (transform.tag == "Enemy")
        {
            float x = Random.Range(transform.position.x - 20, transform.position.x + 20);
            float z = Random.Range(transform.position.z - 20, transform.position.z + 20);
            return (new Vector3(x, 1f, z));
        }
        else
        {
            float x = Random.Range(transform.position.x + 7, transform.position.x + 20);
            float z = Random.Range(transform.position.z - 10, transform.position.z + 10);
            return (new Vector3(x, 1f, z));
        }
    }
}
