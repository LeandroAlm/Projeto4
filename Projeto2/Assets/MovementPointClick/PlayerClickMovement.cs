using System.Collections;
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

    void Start ()
    {
		camera = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
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

        //Right click em cima de uma objecto para farmar
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                //Move Player
                Debug.Log("Hit: " + hit.collider.name);
                MoveToPoint(hit.point);
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
            animator.SetFloat("Turn", moveSpeed, 0.1f, Time.deltaTime);
        }

        if (animator.GetBool("Sword") == true)
        {
            animator.SetBool("Gun", false);
            animator.SetFloat("ForwardS", moveSpeed, 0.1f, Time.deltaTime);
            animator.SetFloat("TurnS", moveSpeed, 0.1f, Time.deltaTime);
        }
    }

    public void GunsMovementController()
    {
        if (Input.GetKeyDown("1"))
        {
            animator.SetBool("Sword", true);
            animator.SetBool("Gun", false);
            Axe.gameObject.SetActive(true);

        }
        if (Input.GetKeyDown("2"))
        {
            animator.SetBool("Gun", true);
            animator.SetBool("Sword", false);
            Axe.gameObject.SetActive(false);
        }
    }
}
