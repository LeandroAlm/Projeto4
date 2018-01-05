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
    bool TimeOnOff;
    public Transform GOTarget;

    
    //##############################################CODIGO SO PARA O COVIL########################################


    void Start ()
    {
        //covilpos = Vector3.zero;
        Rotate = 1;
        etapa = 0;
        posDestino = transform.position;
        takeTime = 11.0f;
        WaitThisTime = 0.0f;
        TimeOnOff = true;

        AddToLists();
    }
    
	void Update ()
    {
        distance = Vector3.Distance(transform.position, player.position);
        DesicionRotation();
    }

    void AddToLists()
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
                    //Debug.Log("Enemy 1 going");
                    MoveRandom(bichos[positions[0]]);
                }),
                new DTAction(() =>
                {
                    // se nao é pq ja esta na pos random e tem de voltar ao covil
                    if (transform.GetChild(0).position != covilpos)
                    {
                        //Debug.Log("Enemy 1 coming back...");
                        BackCovil(bichos[positions[0]]);
                    }
                    else
                    {
                        Rotate++;
                        etapa = 0;
                        TimeOnOff = true;
                    }
                })
            ),
            new DTBinaryDecision(
            () => { return Rotate == 2 || Rotate == 3; },
                new DTBinaryDecision(
                () => { return Rotate == 2; },
                    new DTBinaryDecision(
                    () => { return etapa == 0 || etapa == 1; },
                        new DTAction(() =>
                        {
                            // bicho 2 mover
                            //Debug.Log("Enemy 2 going");
                            MoveRandom(bichos[positions[1]]);
                        }),
                        new DTAction(() =>
                        {
                            // se nao é pq ja esta na pos random e tem de voltar ao covil
                            if (transform.GetChild(1).position != covilpos)
                            {
                                //Debug.Log("Enemy 2 coming back...");
                                BackCovil(bichos[positions[1]]);
                            }
                            else
                            {
                                Rotate++;
                                etapa = 0;
                                TimeOnOff = true;
                            }
                        })
                    ),
                    new DTBinaryDecision(
                    () => { return etapa == 0 || etapa == 1; },
                        new DTAction(() =>
                        {
                            // bicho 3 mover
                            //Debug.Log("Enemy 3 going");
                            MoveRandom(bichos[positions[2]]);
                        }),
                        new DTAction(() =>
                        {
                            // se nao é pq ja esta na pos random e tem de voltar ao covil
                            if (transform.GetChild(2).position != covilpos)
                            {
                                //Debug.Log("Enemy 3 coming back...");
                                BackCovil(bichos[positions[2]]);
                            }
                            else
                            {
                                Rotate = 1;
                                etapa = 0;
                                TimeOnOff = true;
                            }
                        })
                    )
                ),
                new DTAction(() =>
                {
                    // SE NAO FOR 2 || 3
                    //Debug.Log("ERROR");
                })
            )
        );
        tree.MakeDecision().Run();
    }

    void MoveRandom(Transform enemy)
    {
        if (enemy.position != posDestino)
        {
            //float speed = 1f;
            //float step = speed * Time.deltaTime;
            //enemy.position = Vector3.MoveTowards(enemy.position, posDestino, step);
            //// Correção de olhar
            //enemy.LookAt(posDestino);
            transform.GetComponent<Unit>().target.position = posDestino;
            transform.GetComponent<Unit>().playerPos = posDestino;
        }
        else
        {
            if (etapa < 2)
                etapa++;

            posDestino = RandomPos();
        }   
    }

    void BackCovil(Transform enemy)
    {
        if (!TimeOnOff)
        {
            // Voltar ao covil
            if (enemy.position != covilpos && covilpos != Vector3.zero)
            {
                // andamento para o covil!!!!
                float speed = 1f;
                float step = speed * Time.deltaTime;
                enemy.position = Vector3.MoveTowards(enemy.position, covilpos, step);
                // Correção de olhar
                enemy.LookAt(covilpos);
            }
        }
        else
        {
            if (WaitThisTime > 5.0f)
            {
                WaitThisTime = 0.0f;
                TimeOnOff = false;

                if (enemy == transform.GetChild(0))
                    covilpos = positions[0];
                else if (enemy == transform.GetChild(1))
                    covilpos = positions[1];
                else if (enemy == transform.GetChild(2))
                    covilpos = positions[2];
            }
            else
            {
                WaitThisTime += Time.deltaTime;
                covilpos = Vector3.zero;
            }
        }
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
            //Debug.Log("Pos aleatoria");
            float x = Random.Range(transform.position.x + 7, transform.position.x + 20);
            float z = Random.Range(transform.position.z - 7, transform.position.z + 7);
            return (new Vector3(x, 1f, z));
        }
    }
}
