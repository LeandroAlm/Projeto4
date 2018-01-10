using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public int hp;
    public static int Amount;

    AudioSource audio;

    private void Start()
    {
        audio = transform.GetComponent<AudioSource>();

    }

    public int GetAmount()
    {
        Amount = Random.Range(3, 10);
        return Amount;
    }

    public void Damage()
    {

        audio.Play();
        hp--;
        if (hp == 0)
        {
            Destroy(gameObject, 0.5f);
        }
    }

}
