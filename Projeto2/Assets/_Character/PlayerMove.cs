using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    private Animator animator;
    private Rigidbody rb;
    public float Speed;
    Vector3 LookPos;

    private Transform cam;
    Vector3 camForward, move, moveInput;
    float forwarAmount;
    float turnAmount;

    public Transform Axe;

    private Camera mainCamera;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetupAnimator();
        cam = Camera.main.transform;

    
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (cam != null)
        {
            camForward = Vector3.Scale(cam.up, new Vector3(1, 0, 1)).normalized;
            move = vertical * camForward + horizontal * cam.right;

        }
        else
        {
            move = vertical * Vector3.forward + horizontal * Vector3.right;
        }

        if (move.magnitude > 1)
        {
            move.Normalize();
        }

        Move(move);

        Vector3 movement = new Vector3(horizontal, 0, vertical);

        rb.AddForce((movement * Speed / Time.deltaTime) / 5);

        // FARMAR!!!
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Farming");
        }

    }

    void Move(Vector3 move)
    {
        if (move.magnitude > 1)
        {
            move.Normalize();
        }
        this.moveInput = move;
        ConvertMoveInput();
        UpdateAnimator();
    }

    void ConvertMoveInput()
    {
        Vector3 localMove = transform.InverseTransformDirection(moveInput);
        turnAmount = localMove.x;
        forwarAmount = localMove.z;
    }


    void UpdateAnimator()
    {
        if (animator.GetBool("Gun") == true)
        {
            animator.SetBool("Sword", false);
            animator.SetFloat("Forward", forwarAmount, 0.1f, Time.deltaTime);
            animator.SetFloat("Turn", turnAmount, 0.1f, Time.deltaTime);
        }
        if (animator.GetBool("Sword") == true)
        {
            animator.SetBool("Gun", false);
            animator.SetFloat("ForwardS", forwarAmount, 0.1f, Time.deltaTime);
            animator.SetFloat("TurnS", turnAmount, 0.1f, Time.deltaTime);
        }
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 100))
        {
            LookPos = hit.point;
        }

        Vector3 lookDir = LookPos - transform.position;
        lookDir.y = 0;

        transform.LookAt(transform.position + lookDir, Vector3.up);
        
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

}
