using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAnimController : MonoBehaviour {

    private Animator animator;

    public AudioClip howlSound;

    private AudioSource audioSource;


    void Start ()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
 
    void Update ()
    {
    }

    void Run()
    {
        animator.SetBool("run", true);
    }

    void IdleAttack()
    {
        animator.SetBool("standbite", true);    
    }

    void RunAttack()
    {
        animator.SetBool("runBite", true);
    }

    void Death()
    {
        animator.SetBool("dead", true);
    }

    void Gethit()
    {
        animator.SetBool("gethit", true);
    }

    void HowlSound()
    {
        audioSource.Play();

    }
}
