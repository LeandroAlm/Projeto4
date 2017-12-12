﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerClickMovement : MonoBehaviour
{
    private Camera camera;

    public LayerMask MovementMask;
    private NavMeshAgent agent;
    public Transform Axe;
    private Animator animator;
    private float moveSpeed;
    private bool usingAxe, usingGun;

    void Start ()
    {
		camera = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        usingAxe = true;
    }
	
	void Update ()
	{
	    RayCastClick();
	    GunsMovementController();
        SetupAnimator();
        UpdateAnimator();

	    moveSpeed = agent.velocity.magnitude / agent.speed;
	}

    public void RayCastClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, MovementMask))
            {
                //Move Player
                Debug.Log("Hit: " + hit.collider.name);
                MoveToPoint(hit.point);
            }
        }

        //Right click em cima de um objecto para farmar
        if (Input.GetMouseButtonDown(1))
        {          
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {             
                float distance = Vector3.Distance(transform.position, hit.collider.transform.position);

                Debug.Log("Hit: " + hit.collider.name + " at " + distance + " distance.");

                if (distance > 2)
                {
                    MoveToPoint(hit.point);
                }

                else
                {
                    animator.SetTrigger("Farming");

                    if (hit.collider.tag == "Stone")
                    {
                        transform.GetComponent<PlayerStatus>().StoneAmout(hit.collider.GetComponent<Stone>().GetAmount());
                        hit.collider.GetComponent<Stone>().Damage();
                    }

                    else if (hit.collider.tag == "Tree")
                    {
                        transform.GetComponent<PlayerStatus>().WoodAmout(hit.collider.GetComponent<Tree>().GetAmount());
                        hit.collider.GetComponent<Tree>().Damage();
                    }
                }               
            }
        }
    }

    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }

    void SetupAnimator()
    {
        animator = GetComponent<Animator>();

        foreach (var childAnimator in GetComponentsInChildren<Animator>())
        {
            if (childAnimator != animator)
            {
                animator.avatar = childAnimator.avatar;
                Destroy(childAnimator);
                break;
            }
        }
    }

    void UpdateAnimator()
    {
        if (animator.GetBool("Gun") == true)
        {
            animator.SetBool("Sword", false);
            animator.SetFloat("Forward", moveSpeed, 0.1f, Time.deltaTime);
            //animator.SetFloat("Turn", moveSpeed, 0.1f, Time.deltaTime);
        }

        if (animator.GetBool("Sword") == true)
        {
            animator.SetBool("Gun", false);
            animator.SetFloat("ForwardS", moveSpeed, 0.1f, Time.deltaTime);
            //animator.SetFloat("TurnS", moveSpeed, 0.1f, Time.deltaTime);
        }
    }

    public void GunsMovementController()
    {
        animator.SetBool("Sword", true);
        animator.SetBool("Gun", false);
        Axe.gameObject.SetActive(true);
        usingAxe = true;
        usingGun = false;

        if (Input.GetKeyDown("1") && usingAxe == false)
        {
            animator.SetBool("Sword", true);
            animator.SetBool("Gun", false);
            Axe.gameObject.SetActive(true);
            usingAxe = true;
            usingGun = false;
        }
        if (Input.GetKeyDown("2"))
        {
            usingAxe = false;
            usingGun = true;
            animator.SetBool("Gun", true);
            animator.SetBool("Sword", false);
            Axe.gameObject.SetActive(false);
        }
    }
}
