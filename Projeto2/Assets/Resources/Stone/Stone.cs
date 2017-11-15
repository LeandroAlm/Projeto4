using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public int hp;
    private int Amount;
    
    public int GetAmount()
    {
        Amount = Random.Range(1, 3);
        return Amount;
    }

    public void Damage()
    {
        hp--;
        if (hp == 0)
        {
            Destroy(gameObject);
        }
    }

}
