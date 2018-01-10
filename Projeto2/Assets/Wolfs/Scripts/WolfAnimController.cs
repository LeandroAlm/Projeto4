using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IPCA.AI.DecisionTrees;

public class WolfAnimController : MonoBehaviour
{

    private Animator animator;

    public AudioClip howlSound;

    private AudioSource audioSource;

    public Transform Mouth;

    public GameObject Meat;
    public GameObject Player;

    public int Bite;
    public float HP;
    float distance;

    public static bool canFollow;
    public static bool CloseToWall;
    public static bool HaveWall;

    Unit unitScript;

    public static List<GameObject> walls;

    float distance2;

    void Start ()
    {
        walls = new List<GameObject>();
        canFollow = true;
        canFollow = false;
        HaveWall = true;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        unitScript = GetComponent<Unit>();
    }

    void Update()
    {
        ////distance2
        //if (Vector3.Distance(this.transform.position,walls.transform.position) < 3)
        //{
        //    //encontrou uma muralha parou
        //    canFollow = false;
        //}
        //else
        //{
        //    unitScript.Move();

        //}
        Debug.Log("PLAYER POS: " + Player.transform.position);

        distance = Vector3.Distance(transform.position,Player.transform.position);
        
        Manager();
    }

    void Manager()
    {
        if(!CloseToWall)
        {
            if (distance < 1)
            {
                // Chegou ao inimigo
                Run(false);
                IdleAttack(true);
            }
            else
            {
                IdleAttack(false);
                Run(true);
            }
        }
        else
        {
            if (HaveWall)
            {
                // player cercado com walls
                RaycastHit hit;
                Ray ray = new Ray(transform.position, Player.transform.position);
                Vector3 dir = Player.transform.position - transform.position;

                Debug.DrawRay(transform.position, dir, Color.blue);

                if (Physics.Raycast(ray, out hit, 1000f, 1 << 12))
                {
                    Unit.SecondTarget = hit.collider.transform;
                }
                HaveWall = false;
            }
        }

        //DTBinaryDecision tree = new DTBinaryDecision(
        //        () => { return distance > 10; },
        //        new DTAction(() =>
        //        {
        //        }),
        //        new DTAction(() =>
        //        {

        //        })
        //    );
        //tree.MakeDecision().Run();
    }

    void Run(bool Aux)
    {
        if (Aux)
            animator.SetBool("run", true);
        else
            animator.SetBool("run", false);
    }

    void IdleAttack(bool Aux)
    {
        if (Aux)
        {
            animator.SetBool("standbite", true);
            // Dentada
            RaycastHit hit;
            Ray ray = new Ray(Mouth.position, transform.forward);
            if (Physics.Raycast(ray, out hit, 1))
            {
                if (hit.collider.tag == "Player")
                {
                    Debug.Log("Wolf bite Player!");

                    hit.collider.GetComponent<PlayerStatus>().GetDamage(Bite);
                }
            }
        }
        else
        {
            animator.SetBool("standbite", false);
        }
    }

    //void RunAttack()
    //{
    //    animator.SetBool("runBite", true);
    //}

    void Death()
    {
        animator.SetBool("dead", true);
        Destroy(gameObject, 1.5f);
        // Aparecer Carne!!!
        Instantiate(Meat, transform.position + Vector3.up, Quaternion.identity);
    }

    void Gethit()
    {
        // Sem grande importânica
        animator.SetBool("gethit", true);
    }

    void HowlSound()
    {
        audioSource.Play();
    }

    public void GetDamage(int damageAmount)
    {
        HP -= damageAmount;
        CheckDie();
    }

    void CheckDie()
    {
        if (HP <= 0)
        {
            Death();
        }
    }
}
