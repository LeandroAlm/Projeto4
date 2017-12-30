using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image hp;
    public Image Armor;
    public Transform player;

	void Start ()
    {
		
	}
	

	void Update ()
    {
        hp.fillAmount = player.transform.GetComponent<PlayerStatus>().HP / 100f;
        Armor.fillAmount = player.transform.GetComponent<PlayerStatus>().armor / 100f;
    }
}
