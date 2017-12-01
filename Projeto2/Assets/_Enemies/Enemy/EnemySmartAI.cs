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
    private Dictionary<Vector3, Transform> bichos;
    private List<Vector3> positions;
    private int Rotate, etapa;
    bool can;

    void Start ()
    {
        covilpos = Vector3.zero;
        Rotate = 1;
        etapa = 0;
        posDestino = transform.position;
        takeTime = 11.0f;
        WaitThisTime = 0.0f;
        can = false;

        if (transform.tag == "Enemy2")
        {
            bichos = new Dictionary<Vector3, Transform>();
            bichos.Add(transform.GetChild(0).position, transform.GetChild(0));
            bichos.Add(transform.GetChild(1).position, transform.GetChild(1));
            bichos.Add(transform.GetChild(2).position, transform.GetChild(2));
            positions = new List<Vector3>();
            positions.Add(transform.GetChild(0).position);
            positions.Add(transform.GetChild(1).position);
            positions.Add(transform.GetChild(2).position);
        }
    }
    

	void Update ()
    {
        distance = Vector3.Distance(transform.position, player.position);
        if (transform.tag == "Enemy")
            DecisionIdle();
        if (transform.tag == "Enemy2")
            DesicionRotation();

        Debug.Log("Pos aleatoria: " + posDestino);
    }

    void DecisionIdle()
    {
        // Inimigo branco

        DTBinaryDecision tree = new DTBinaryDecision(
                () => { return distance > 10; },
                new DTAction(() =>
                {
                    // Mover aleatoriamente de x em x segundos
                    //Debug.Log("Move random!");
                    MoveRandom(transform);
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
        // COVIL
        DTBinaryDecision tree = new DTBinaryDecision(
                () => { return Rotate == 1; },
                new DTBinaryDecision(
                    () => { return etapa == 0 || etapa == 1; },
                    new DTAction(() =>
                    {
                        // bicho 1 mover
                        Debug.Log("Enemy 1 going");
                        MoveRandom(bichos[positions[0]]);
                    }),
                    new DTAction(() =>
                    {
                        // se nao é pq ja esta na pos random e tem de voltar ao covil
                        if (transform.GetChild(0).position != covilpos)
                        {
                            Debug.Log("Enemy 1 coming back...");
                            BackCovil(bichos[positions[0]]);
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
                    () => { return etapa == 0 || etapa == 1; },
                        new DTAction(() =>
                        {
                            // bicho 2 mover
                            Debug.Log("Enemy 2 going");
                            MoveRandom(bichos[positions[1]]);
                        }),
                        new DTAction(() =>
                        {
                            // se nao é pq ja esta na pos random e tem de voltar ao covil
                            if (transform.GetChild(0).position != covilpos)
                            {
                                Debug.Log("Enemy 2 coming back...");
                                BackCovil(bichos[positions[0]]);
                            }
                            else
                            {
                                Rotate++;
                                etapa = 0;
                            }
                        })
                    ),
                    //new DTBinaryDecision(
                    //() => { return Rotate == 3; },
                        new DTBinaryDecision(
                        () => { return etapa == 0 || etapa == 1; },
                            new DTAction(() =>
                            {
                                // bicho 2 mover
                                Debug.Log("Enemy 2 going");
                                MoveRandom(bichos[positions[1]]);
                            }),
                            new DTAction(() =>
                            {
                                // se nao é pq ja esta na pos random e tem de voltar ao covil
                                if (transform.GetChild(0).position != covilpos)
                                {
                                    Debug.Log("Enemy 2 coming back...");
                                    BackCovil(bichos[positions[0]]);
                                }
                                else
                                {
                                    Rotate++;
                                    etapa = 0;
                                }
                            })
                        )
                    //)
                )
            );
        tree.MakeDecision().Run();
    }

    void MoveRandom(Transform enemy)
    {
        if (transform.tag != "Enemy2")
        {
            if (enemy.position != posDestino)
            {
                float speed = 1f;
                float step = speed * Time.deltaTime;
                enemy.position = Vector3.MoveTowards(enemy.position, posDestino, step);
                // Correção de olhar
                enemy.LookAt(posDestino);
            }
            else
            {
                if (takeTime > 10.0f)
                {
                    takeTime = 0.0f;
                    posDestino = RandomPos();
                }
                else
                    takeTime += Time.deltaTime;
            }
        }
        else
        {
            if (enemy.position != posDestino)
            {
                float speed = 1f;
                float step = speed * Time.deltaTime;
                enemy.position = Vector3.MoveTowards(enemy.position, posDestino, step);
                // Correção de olhar
                enemy.LookAt(posDestino);
            }
            else
            {
                etapa++;
                posDestino = RandomPos();
            }
        }
    }

    void BackCovil(Transform enemy)
    {
        // Voltar ao covil
       

        if (enemy.position != covilpos && covilpos != Vector3.zero)
        {
            // andamento para o covil!!!!
            Debug.Log("Voltando");
            float speed = 1f;
            float step = speed * Time.deltaTime;
            enemy.position = Vector3.MoveTowards(enemy.position, covilpos, step);
            // Correção de olhar
            enemy.LookAt(covilpos);
            WaitThisTime = 0.0f;
        }
        else
        {
            if (WaitThisTime > 5.0f)
            {
                WaitThisTime = 0.0f;

                if (enemy == transform.GetChild(0))
                    covilpos = positions[0];
                else if (enemy == transform.GetChild(1))
                    covilpos = positions[1];
                else if (enemy == transform.GetChild(2))
                    covilpos = positions[2];
            }
            else
                WaitThisTime += Time.deltaTime;
        }
    }

    void GoToPlayer()
    {
        // AQUI IRA TER UMA ARVORE DE DECISAO, PARA SIMPLIFICAR ESTA A ANDAR SO ATRAZ DO PLAYER
        // decisao de se estiver longe continua a andar atraz dele, se ja estiver perto ataca!

        float speed = 1f;
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, player.position + Vector3.up / 2, step);
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
            Debug.Log("Pos aleatoria");
            float x = Random.Range(transform.position.x + 7, transform.position.x + 20);
            float z = Random.Range(transform.position.z - 7, transform.position.z + 7);
            return (new Vector3(x, 1f, z));
        }
    }
}
