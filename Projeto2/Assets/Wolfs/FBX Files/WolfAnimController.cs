using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAnimController : MonoBehaviour {

    private Animator animator;

    public AudioClip howlSound;

    private AudioSource audioSource;

    public Transform Mouth;

    public GameObject Meat;

    public int Bite;
    public float HP;

    void Start ()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            IdleAttack();
        }
    }

    void Run()
    {
        animator.SetBool("run", true);
    }

    void IdleAttack()
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

    void RunAttack()
    {
        animator.SetBool("runBite", true);
    }

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

    public void GetDamage(int DamageAmount)
    {
        HP -= DamageAmount;
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
