using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WerewolfAnimController : MonoBehaviour {


    private Animator animator;

    float timer = 1;


    void Start ()
    {
        animator = GetComponent<Animator>();

    }

    void Update ()
    {
       
    }

    void Death()
    {
        animator.SetBool("death", true);
        
    }

    void Roar()
    {
        animator.SetBool("roar", true);

    }

    void Walk()
    {
        animator.SetBool("walk", true);

    }

    void Run()
    {
        animator.SetBool("run", true);

    }

    void WalkAttack()
    {
        animator.SetBool("walkAttack", true);

    }

    void WallDestroy()
    {
        animator.SetBool("peNaPorta", true);

    }

    void Attack()
    {
        float attackType;

        if (timer <= 0)
        {
            attackType = Random.Range(0.1f, 0.9f);

            animator.SetBool("attack", true);

            animator.SetFloat("attackFloat", attackType, 0.1f, Time.deltaTime);

            timer = 1;
            
        }
        timer -= Time.deltaTime;

        
    }
}
