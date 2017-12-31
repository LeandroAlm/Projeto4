using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public Vector3 offset;

    public float zoomSpeed = 4f;
    public float minZoom = 5f;
    public float maxZoom = 15f;

    public float pitch = 2f;

    public float yawSpeed = 75f;

    private float currentZoom = 10f;
    private float currentYaw = 0f;
    private float currentPitch = 0f;

    float q = 0;

    RaycastHit hit;

    Ray cameraRay;

    private Camera mainCamera;

    RaycastHit oldHit;

    GameObject lastTree;


    public List<GameObject> treesList;

    private void Start()
    {

        treesList = new List<GameObject>();
        mainCamera = FindObjectOfType<Camera>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            q = q + 0.5f;
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            q = 0;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            q = q - 0.5f;
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            q = 0;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
            currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
        }

        currentYaw -= q * yawSpeed * Time.deltaTime;
        currentPitch -= Input.GetAxis("Vertical") * yawSpeed * Time.deltaTime;

        currentPitch = Mathf.Clamp(currentPitch, -25, 40);


        transform.position = target.position - offset * currentZoom;
        transform.LookAt(target.position + Vector3.up * pitch);

        transform.RotateAround(target.position, Vector3.up, currentYaw);
        //transform.RotateAround(target.position, Vector3.right, currentPitch);

        cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);

        XRay();

        Debug.Log(treesList.Count);
    }



    private void XRay()
    {

        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, fwd, out hit))
        {
            if (hit.collider.tag == "Tree")
            {
                // Add transparence
                //SetMaterialTransparent(hit.collider.gameObject);
                iTween.FadeTo(hit.collider.gameObject, 0, 0.2f);

                lastTree = hit.collider.gameObject;

            }
            else
            {

                //treesList.Add(tree);

                iTween.FadeTo(lastTree, 1, 1);
                //StartCoroutine(MyFunction(1f, lastTree));
            }
       
        }
       

    }


    IEnumerator MyFunction(float delayTime, GameObject obj)
    {
        yield return new WaitForSeconds(delayTime);

        foreach (Material m in obj.GetComponent<Renderer>().materials)
        {
            m.SetFloat("_Mode", 2);

            m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);

            m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);

            m.SetInt("_ZWrite", 0);

            m.DisableKeyword("_ALPHATEST_ON");

            m.EnableKeyword("_ALPHABLEND_ON");

            m.DisableKeyword("_ALPHAPREMULTIPLY_ON");

            m.renderQueue = 3000;
        }
    }



    private void SetMaterialOpaque(GameObject obj)
    {
        foreach (Material m in obj.GetComponent<Renderer>().materials)
        {
            m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);

            m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);

            m.SetInt("_ZWrite", 1);

            m.DisableKeyword("_ALPHATEST_ON");

            m.DisableKeyword("_ALPHABLEND_ON");

            m.DisableKeyword("_ALPHAPREMULTIPLY_ON");

            m.renderQueue = -1;
        }
    }
}
