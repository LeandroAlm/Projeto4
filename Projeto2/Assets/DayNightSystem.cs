using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightSystem : MonoBehaviour
{

    float timeCounter = 0;

    float speed;
    float width;
    float height;

    Vector3 mapCenter;

    private Light light;

    void Start()
    {
        speed = 0.1f;
        width = 250;
        height = 250;

        mapCenter = new Vector3(0, 0,0);

        light = GetComponent<Light>();
    }

    void Update()
    {
        RenderSettings.ambientIntensity = 100;
        timeCounter += Time.deltaTime * speed;

        float x = Mathf.Cos(timeCounter) * width;
        float y = Mathf.Sin(timeCounter) * height;
        float z = 0;

        transform.position = new Vector3(x, y, z);
        transform.LookAt(mapCenter);

        if(this.transform.position.y <= 0)
        {
            light.intensity = 0;
        }
        else
        {
            light.intensity = 1;
        }
    }
}
