using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{
    public void StartSinglePlayer()
    {
        SceneManager.LoadScene("Cena");
    }

    public void StartMultiplayer()
    {
        SceneManager.LoadScene("MultiplayerScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
