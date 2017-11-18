using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstaceHouse : MonoBehaviour
{

    public float mousePosX;
    public float mousePosY;

    public static bool isPlaced;

    bool CanBuild = false;

    public Material GreenMaterial;
    public Material redMaterial;

    public GameObject housePrefab;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (!isPlaced)
        //{
        //BuildingManager.isBuilding = true;
        Vector3 pos = transform.position;


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            this.transform.position = new Vector3(hit.point.x, 0, hit.point.z);

        }
        //}

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            Debug.Log("tou");
            this.transform.Rotate(Vector3.up * 250 * Time.deltaTime, Space.World);
        }

        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            Debug.Log("tou");
            this.transform.Rotate(Vector3.up * -250 * Time.deltaTime, Space.World);
        }


        if (CanBuild)
        {
            if (Input.GetMouseButtonDown(0))
            {

                isPlaced = true;
                BuildingManager.isBuilding = false;


                Instantiate(housePrefab, this.transform.position, this.transform.rotation);

                this.gameObject.SetActive(false);

            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        CanBuild = false;
        for (int i = 0; i < transform.childCount; i++)
        {
            Renderer rend = transform.GetChild(i).GetComponent<Renderer>();

            rend.material = redMaterial;

        }

    }


    private void OnCollisionExit(Collision collision)
    {
        CanBuild = true;

        for (int i = 0; i < transform.childCount; i++)
        {
            Renderer rend = transform.GetChild(i).GetComponent<Renderer>();

            rend.material = GreenMaterial;

        }

    }
}
