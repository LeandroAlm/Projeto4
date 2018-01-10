using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Image HP;

    public GameObject enemy;
    // Update is called once per frame

    void Update()
    {
        HP.fillAmount = enemy.gameObject.GetComponent<WolfAnimController>().HP / 100;

    }
}
