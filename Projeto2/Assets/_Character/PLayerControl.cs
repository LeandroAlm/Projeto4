using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;

    Vector3 camForward, move, moveInput;
    float forwarAmount;
    float turnAmount;

    public Transform Axe;
    public Transform Gun;
    public GameObject posHandL;


    private Vector3 moveVelocity;
    public float moveSpeed;


    private Camera mainCamera;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetupAnimator();
        //cam = Camera.main.transform;
        animator = GetComponent<Animator>();
        mainCamera = FindObjectOfType<Camera>();


    }


    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector3(moveVelocity.x,rb.velocity.y,moveVelocity.z);
        MovementTopDown();
        UpdateAnimator();
        ConvertMoveInput();

        // FARMAR!!!
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Farming");
        }

    }

    void ConvertMoveInput()
    {
        Vector3 localMove = transform.InverseTransformDirection(moveInput);
        turnAmount = localMove.x;
        forwarAmount = localMove.z;
    }

    void MovementTopDown()
    {
        {
            moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

            moveInput = transform.TransformDirection(moveInput);

            moveVelocity = moveInput * moveSpeed;         

            Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);

            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

            float rayLenght;
           
            if (groundPlane.Raycast(cameraRay, out rayLenght))
            {
                Vector3 pointToLook = cameraRay.GetPoint(rayLenght);
                Debug.DrawLine(cameraRay.origin, pointToLook, Color.red);

                transform.LookAt(new Vector3 (pointToLook.x, transform.position.y, pointToLook.z));
                //transform.TransformDirection(moveVelocity);
            }
        }
    }
   
    void UpdateAnimator()
    {
        //animator.SetBool("Sword", true);
        //animator.SetBool("Gun", false);
        //Axe.gameObject.SetActive(true);

        if (animator.GetBool("Gun") == true)
        {
            animator.SetBool("Sword", false);
            animator.SetFloat("Forward", forwarAmount, 0.1f, Time.deltaTime);
            animator.SetFloat("Turn", turnAmount, 0.1f, Time.deltaTime);
        }

        if (animator.GetBool("Sword") == true)
        {
            animator.SetBool("Gun", false);

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                forwarAmount = 1;
                animator.SetFloat("ForwardS", forwarAmount, 0.1f, Time.deltaTime);
            }

            else
            {
                forwarAmount = 0;
                animator.SetFloat("ForwardS", forwarAmount, 0.1f, Time.deltaTime);
            }

            //animator.SetFloat("ForwardS", forwarAmount, 0.1f, Time.deltaTime);
            animator.SetFloat("TurnS", turnAmount, 0.1f, Time.deltaTime);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            animator.SetBool("Sword", true);
            animator.SetBool("Gun", false);
            Axe.gameObject.SetActive(true);
            Gun.gameObject.SetActive(false);
        }
        if (Input.GetKeyDown("2"))
        {
            animator.SetBool("Gun", true);
            animator.SetBool("Sword", false);
            Axe.gameObject.SetActive(false);
            Gun.gameObject.SetActive(true);
        }
    }


    // POSICIONAR A LEFT HAND!!!!!
    //public void OnAnimatorIK(int layerIndex)
    //{
    //    if (Gun.gameObject.activeSelf)
    //    {
    //        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
    //        animator.SetIKPosition(AvatarIKGoal.LeftHand, posHandL.transform.position);
    //        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
    //        animator.SetIKRotation(AvatarIKGoal.LeftHand, Quaternion.Euler(0, 0, 0));
    //    }
    //}

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

    void SetupPlayer(Transform spawnPosition)
    {
        
    }

    /*private IEnumerator SendPlayerMovement()
    {
        
    }*/
}
