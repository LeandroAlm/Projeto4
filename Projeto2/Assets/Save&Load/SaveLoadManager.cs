using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour {

    bool EscOn;
    Transform SaveBtt;
    Transform LoadBtt;

    private void Start()
    {
        EscOn = false;
        SaveBtt = transform.GetChild(0).GetChild(0);
        LoadBtt = transform.GetChild(0).GetChild(1);
        SaveBtt.gameObject.SetActive(false);
        LoadBtt.gameObject.SetActive(false);
    }

    void Update ()
    {
        Debug.Log("Player: " + GameData.PlayerName);

        if (!EscOn)
        { 
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SaveBtt.gameObject.SetActive(true);
                LoadBtt.gameObject.SetActive(true);
                EscOn = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape) && EscOn)
            {
                SaveBtt.gameObject.SetActive(false);
                LoadBtt.gameObject.SetActive(false);
                EscOn = false;
            }
        }
	}

    public void AskSave()
    {
        GameData.PlayerName = "CaldasKing10";
        SaveLoad.Save();
    }

    public void AskLoad()
    {
        SaveLoad.Load();
    }
}
