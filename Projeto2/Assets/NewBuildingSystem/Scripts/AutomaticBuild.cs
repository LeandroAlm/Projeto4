using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticBuild : MonoBehaviour {


    public GameObject Part1;
    public GameObject Part2;
    public GameObject Part3;
    public GameObject Part4;
    public GameObject Part5;
    public GameObject Part6;
    public GameObject Part7;
    public GameObject Part8;
    public GameObject Part12;
    public GameObject Part13;
    public GameObject Part14;
    public GameObject Part15;
    public GameObject Part16;
    public GameObject Part17;


    public static Stack<GameObject> HouseParts = new Stack<GameObject>();

    float timeLeft = 7.0f;

    bool buttonDown;


    void Start ()
    {
        //houseParts = new GameObject[18];

        HouseParts.Push(Part17);
        HouseParts.Push(Part16);
        HouseParts.Push(Part15);
        HouseParts.Push(Part14);
        HouseParts.Push(Part13);
        HouseParts.Push(Part12);
        HouseParts.Push(Part8);
        HouseParts.Push(Part7);
        HouseParts.Push(Part6);
        HouseParts.Push(Part5);
        HouseParts.Push(Part4);
        HouseParts.Push(Part3);
        HouseParts.Push(Part2);
        HouseParts.Push(Part1);
      


        buttonDown = false;

         
    }

    // Update is called once per frame
    void Update ()
    {

       

        if (InstaceHouse.isPlaced)
        {
            for (int i = 0; i < HouseParts.Count; i++)
            {
                timeLeft -= Time.deltaTime;

                if (timeLeft < 0)
                {
                    HouseParts.Pop().gameObject.SetActive(true);

                    //Debug.Log(houseParts[i].gameObject.name);

                    timeLeft = 7.0f;

                    Debug.Log(HouseParts.Count);
                        
                }
            }



        }

            
    }
   

}

