using System;
using System.Collections;
using System.Collections.Generic;
using GameSparks.RT;
using Org.BouncyCastle.Math.Field;
using UnityEngine;
using UnityEngine.UI;

public class PLayerControl : MonoBehaviour
{
    public PlayerStatus PlayerStatus;
    private Animator animator;
    private Rigidbody rb;
    public Transform Axe;
    public Transform Gun;
    private Transform _spawnPosition;
    public GameObject posHandL;
    private Camera mainCamera;

    private Vector3 moveVelocity;
    private Vector3 position, previousPosition;
    private Vector3 camForward, move, moveInput;
    public Vector3 GoToPosition;
    public float GoToRotation;

    private float updateRate = 0.1f;
    private float forwardAmount;
    public float turnAmount;
    public float moveSpeed;

    public static bool usingAxe = false, usingGun = false;
    public bool canMove;
    public bool onlineVersion;
    public bool IsMyPlayer;
    public bool isAttacking;
    private string enemyTag = "Enemy";
    private int meeleeDamage = 20;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetupAnimator();
        //cam = Camera.main.transform;
        animator = GetComponent<Animator>();
        mainCamera = FindObjectOfType<Camera>();
        PlayerStatus = GetComponent<PlayerStatus>();
        canMove = true;
        onlineVersion = false;
        previousPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (onlineVersion)
        {
            if (IsMyPlayer)
            {
                if (canMove)
                {
                    //rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);
                    MovementTopDown();
                    //GunsMovementController();
                    //ConvertMoveInput();
                }
            }

            else
            {
                transform.position = Vector2.Lerp(transform.position, GoToPosition, Time.deltaTime / updateRate);
                transform.eulerAngles = new Vector3(0, 0, Mathf.LerpAngle(transform.eulerAngles.z, GoToRotation, Time.deltaTime / updateRate));
            }
        }
        else
        {
            if (canMove)
            {
                rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);
                MovementTopDown();
                GunsMovementController();
                ConvertMoveInput();
            }
        }

        position = transform.position;
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
            position = transform.position;

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

            previousPosition = position;
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

            //if (Farm.axeSwing)
            //{
            //    animator.SetFloat("ForwardS", 0, 0.1f, Time.deltaTime);
            //}

        }
        if (Shot.shooting && forwardAmount == 0)
        {
            animator.SetBool("isShooting", true);
           
        }
        else
        {
            animator.SetBool("isShooting", false);

        }

        //switch weapons
        if (Input.GetKeyDown("1"))
        {
            usingAxe = true;
            usingGun = false;
            animator.SetBool("Axe", true);
            animator.SetBool("Gun", false);
            Axe.gameObject.SetActive(true);
            Gun.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown("2"))
        {
            usingAxe = false;
            usingGun = true;
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

    //void AxeAttack()
    //{
    //    if (Input.GetMouseButtonDown(0) && Axe.gameObject.activeSelf)
    //    {
    //        isAttacking = true;
    //        animator.SetTrigger("Farming");
    //    }
    //    else if (Input.GetMouseButtonDown(1) && Axe.gameObject.activeSelf)
    //    {
    //        isAttacking = true;
    //        animator.SetTrigger("attack2");
    //    }

    //    isAttacking = false;
    //}
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == enemyTag)
        {
            if (isAttacking)
            {
                Debug.Log("Meelee on Wolf");
                collision.gameObject.GetComponent<WolfAnimController>().GetDamage(meeleeDamage);
            }
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

    public void SetupPlayer(Transform spawnPosition, bool ismyplayer)
    {
        onlineVersion = true;
        IsMyPlayer = ismyplayer;
        _spawnPosition = spawnPosition;

        if (IsMyPlayer)
        {
            previousPosition = position;
            StartCoroutine(SendPlayerMovement());
        }
        else
        {
            GoToPosition = position;
            GoToRotation = transform.eulerAngles.z;
        }
    }

    private IEnumerator SendPlayerMovement()
    {
        if (position != previousPosition || Mathf.Abs(Input.GetAxis("Vertical")) > 0 || Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
        {
            using(RTData data = RTData.Get())
            {
                Debug.Log("Entrou no SendPlayerMovement");

                data.SetVector3(1, new Vector3(position.x, position.y, position.z));
                data.SetFloat(2, transform.eulerAngles.x);
                data.SetFloat(3, transform.eulerAngles.y);
                data.SetFloat(4, transform.eulerAngles.z);

                GameSparksManager.Instance().GameSparksRtUnity.SendData(2, GameSparksRT.DeliveryIntent.UNRELIABLE_SEQUENCED, data);
            }

            previousPosition = position;
        }

        yield return new WaitForSeconds(updateRate);
        StartCoroutine(SendPlayerMovement());
    }

    //REGO
    //public void DistanceToEnemy()
    //{
    //    float distance = 0;
    //    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

    //    for (int enemy = 0; enemy < enemies.Length; enemy++)
    //    {
    //        distance = Vector3.Distance(transform.position, enemies[enemy].transform.position);
    //    }

    //    if (distance > 2f)
    //    {
    //        PlayerStatus.distanceToRecover = true;
    //    }

    //    else
    //    {
    //        PlayerStatus.distanceToRecover = false;
    //    }

    //    Debug.Log("Distance to enemy: " + distance);
    //}
}
