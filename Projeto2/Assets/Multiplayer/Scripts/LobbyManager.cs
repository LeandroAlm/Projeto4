using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    public GameObject readyButton, exitButton, cancelButton, searchingPlayersText, sliderGameObject;
    public Slider loadingSlider;

	void Start ()
	{
	    searchingPlayersText.GetComponent<Text>();
	    readyButton.GetComponent<Button>().onClick.AddListener(PlayerReady);
	    exitButton.GetComponent<Button>().onClick.AddListener(ExitScene);
	    cancelButton.GetComponent<Button>().onClick.AddListener(CancelScene);

        searchingPlayersText.SetActive(false);

	    sliderGameObject.SetActive(false);

        cancelButton.SetActive(false);
        
	}
	
	void Update ()
    {
        loadingSlider.value += 0.01f;

        if (loadingSlider.value >= 1)
        {
            loadingSlider.value = 0;
        }
    }

    public void PlayerReady()
    {
        readyButton.SetActive(false);
        exitButton.SetActive(false);
        cancelButton.SetActive(true);
        searchingPlayersText.SetActive(true);
        sliderGameObject.SetActive(true);
    }

    public void ExitScene()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void CancelScene()
    {
        SceneManager.LoadScene("MultiplayerScene");
    }
}
