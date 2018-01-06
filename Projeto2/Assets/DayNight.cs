using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{

    public float time;

    public Light sunLight;

    public Light MoonLight;

    private Light towerLight;

    private Light spotLight;


    void Start()
    {
        time = 0;



    }

    void Update()
    {


        time += Time.deltaTime;

        transform.RotateAround(Vector3.zero, Vector3.right, 1 * Time.deltaTime);
        transform.LookAt(Vector3.zero);

        if (MoonLight.transform.position.y > 130) // 40 segundos
        {
 

            

            if (sunLight.intensity >= 0)
            {
                sunLight.intensity -= 0.04f;
            }

            if (MoonLight.intensity < 0.7f)
            {
                MoonLight.intensity += 0.001f;
            }

            if (RenderSettings.ambientIntensity > 0)
            {
                RenderSettings.ambientIntensity -= 0.004f;
            }
            //RenderSettings.ambientSkyColor = Color.red;

            

            if (RenderSettings.reflectionIntensity > 0)
            {
                RenderSettings.reflectionIntensity -= 0.004f;

            }
           
        }
        else
        {

            //isNight = false;

            if (sunLight.intensity < 0.5f)
            {
                sunLight.intensity += 0.0013f;      //sunLight.intensity = 0.5f;
            }
            if (MoonLight.intensity > 0.0f)
            {
                MoonLight.intensity -= 0.025f;
            }
            if (RenderSettings.ambientIntensity < 1)
            {
                RenderSettings.ambientIntensity += 0.003f;
            }

            if (RenderSettings.reflectionIntensity < 0.746f)
            {
                RenderSettings.reflectionIntensity += 0.003f;

            }

        }

        if (time >= 180)
        {
            time = 0;
        }

    }
}
