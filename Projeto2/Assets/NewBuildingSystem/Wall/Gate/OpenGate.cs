using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGate : MonoBehaviour {

    Animator animator;

    void Start ()
    {
        animator = GetComponent<Animator>();
    }

   
    void Update () {
		


    }
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            animator.SetBool("open", true);
            animator.SetBool("close", false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            animator.SetBool("close", true);
            animator.SetBool("open", false);

        }
    }
}
