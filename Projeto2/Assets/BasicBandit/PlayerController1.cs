using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rb;
    public float Speed;
    Vector3 LookPos;

    Transform cam;
    Vector3 camForward, move, moveInput;
    float forwarAmount;
    float turnAmount;

    bool isWalking, isFarming;


    void Start ()
    {


        rb = GetComponent<Rigidbody>();

        anim = GetComponent<Animator>();

        cam = Camera.main.transform;

        isFarming = false;
        isWalking = false;
    }

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

        AnimationController();
    }

    void Move(Vector3 move)
    {
        isWalking = true;

        if (move.magnitude > 1)
            move.Normalize();
        this.moveInput = move;
        ConvertMoveInput();
        //UpdateAnimator();
    }

    void ConvertMoveInput()
    {
        Vector3 localMove = transform.InverseTransformDirection(moveInput);
        turnAmount = localMove.x;
        forwarAmount = localMove.z;
    }

    void AnimationController()
    {
        if(isWalking)
        {
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("Walking", false);
        }
    }
}
