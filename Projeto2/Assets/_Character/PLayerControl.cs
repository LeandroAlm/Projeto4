using System.Collections;
using System.Collections.Generic;
using GameSparks.RT;
using Org.BouncyCastle.Math.Field;
using UnityEngine;
using UnityEngine.UI;

public class PLayerControl : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;

    Vector3 camForward, move, moveInput;
    float forwardAmount;
    public float turnAmount;

    public Transform Axe;
    public Transform Gun;
    public GameObject posHandL;

    private Vector3 moveVelocity;
    public float moveSpeed;

    private Camera mainCamera;
    private bool usingAxe = true, usingGun = false;

    public PlayerStatus PlayerStatus;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetupAnimator();
        //cam = Camera.main.transform;
        animator = GetComponent<Animator>();
        mainCamera = FindObjectOfType<Camera>();
        PlayerStatus = GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector3(moveVelocity.x,rb.velocity.y,moveVelocity.z);
        MovementTopDown();
        GunsMovementController();
        ConvertMoveInput();
    }

    void Update()
    {
        DistanceToEnemy();
    }

    void ConvertMoveInput()
    {
        Vector3 localMove = transform.InverseTransformDirection(moveInput);
        turnAmount = localMove.x;
        forwardAmount = localMove.z;
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
   
    void GunsMovementController()
    {
        animator.SetBool("Axe", true);
        //animator.SetBool("Gun", false);
        //Axe.gameObject.SetActive(true);

        if (animator.GetBool("Gun") == true)
        {
            animator.SetBool("Axe", false);
            animator.SetFloat("Forward", forwardAmount, 0.1f, Time.deltaTime);
            animator.SetFloat("Turn", turnAmount, 0.1f, Time.deltaTime);
        }

        if (animator.GetBool("Axe") == true)
        {
            animator.SetBool("Gun", false);

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                //forwardAmount = 1;
                animator.SetFloat("ForwardS", forwardAmount, 0.1f, Time.deltaTime);
            }

            else
            {
                //forwardAmount = 0;
                animator.SetFloat("ForwardS", forwardAmount, 0.1f, Time.deltaTime);
            }

            animator.SetFloat("ForwardS", forwardAmount, 0.1f, Time.deltaTime);
            animator.SetFloat("TurnS", turnAmount, 0.1f, Time.deltaTime);
        }

        if (Input.GetKeyDown("1"))
        {
            animator.SetBool("Axe", true);
            animator.SetBool("Gun", false);
            Axe.gameObject.SetActive(true);
            Gun.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown("2"))
        {
            animator.SetBool("Gun", true);
            animator.SetBool("Axe", false);
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

    public void SetupPlayer(Vector3 spawnPosition)
    {
        transform.position = spawnPosition;
    }

    /*private IEnumerator SendPlayerMovement()
    {
        
    }*/

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            PlayerStatus.hp -= 30;
            Debug.Log("Player Life: " + PlayerStatus.hp);
        }
    }

    public void DistanceToEnemy()
    {
        float distance = 0;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int enemy = 0; enemy < enemies.Length; enemy++)
        {
            distance = Vector3.Distance(transform.position, enemies[enemy].transform.position);
        }

        if (distance > 2f)
        {
            PlayerStatus.distanceToRecover = true;
        }

        else
        {
            PlayerStatus.distanceToRecover = false;
        }

        Debug.Log("Distance to enemy: " + distance);
    }
}
