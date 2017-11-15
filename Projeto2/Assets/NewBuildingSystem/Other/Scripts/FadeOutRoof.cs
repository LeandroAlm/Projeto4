using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutRoof : MonoBehaviour {

    public GameObject Roof = null;
    public GameObject Roof2 = null;


    public static bool isInside = false;

    void OnTriggerEnter(Collider collider)
    {
        if (IsCharacter(collider))
        {
            isInside = true;

            SetMaterialTransparent(Roof);
            SetMaterialTransparent(Roof2);
            iTween.FadeTo(Roof, 0, 1);
            iTween.FadeTo(Roof2, 0, 1);
        }
    }
    
    private bool IsCharacter(Collider collider)
    {
        // Implement you logic here if it is your player that is the collider

        return true;
    }
    

    private void SetMaterialTransparent(GameObject RoofObject)
    {
        foreach (Material m in RoofObject.GetComponent<Renderer>().materials)
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
    

    private void SetMaterialOpaque()
    {
        foreach (Material m in Roof.GetComponent<Renderer>().materials)
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


    void OnTriggerExit(Collider collider)
    {  
        if (IsCharacter(collider))
        {
            isInside = false;


            // Set material to opaque
            iTween.FadeTo(Roof, 1, 1);
            iTween.FadeTo(Roof2, 1, 1);
            Invoke("SetMaterialOpaque", 1.0f);
        }
    }
}