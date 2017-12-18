using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameSparks.Api.Messages;
using GameSparks.Api.Responses;
using GameSparks.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    public GameObject readyButton, exitButton, cancelButton, searchingPlayersText, sliderGameObject, playerNameInstruction, passwordInstruction;
    public InputField playerNameInputField, passwordInputField;
    public Slider loadingSlider;
    private SessionInformation sessionInformation;

	void Start ()
	{
	    GS.GameSparksAvailable += (isAvailable) =>
	    {
	        if (isAvailable)
	        {
	            Debug.Log("GameParks Connected");
	        }

	        else
	        {
	            Debug.Log("GameSparks Disconnected");
	        }
	    };

	    searchingPlayersText.GetComponent<Text>();
	    playerNameInstruction.GetComponent<Text>();
	    passwordInstruction.GetComponent<Text>();
	    readyButton.GetComponent<Button>().onClick.AddListener(PlayerReady);
	    exitButton.GetComponent<Button>().onClick.AddListener(ExitScene);
	    cancelButton.GetComponent<Button>().onClick.AddListener(CancelScene);

        searchingPlayersText.SetActive(false);

	    sliderGameObject.SetActive(false);

        cancelButton.SetActive(false);
        
        playerNameInstruction.SetActive(true);
        playerNameInputField.ActivateInputField();
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
        playerNameInputField.DeactivateInputField();
        passwordInputField.DeactivateInputField();

        readyButton.SetActive(false);
        exitButton.SetActive(false);
        playerNameInstruction.SetActive(false);
        passwordInstruction.SetActive(false);
        cancelButton.SetActive(true);
        searchingPlayersText.SetActive(true);
        sliderGameObject.SetActive(true);

        GameSparksManager.Instance().AuthenticateUser(playerNameInputField.text,passwordInputField.text, OnRegistration, OnAuthentication);

        SceneManager.LoadScene("Cena");       
    }

    public void ExitScene()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void CancelScene()
    {
        SceneManager.LoadScene("MultiplayerScene");
    }

    private void OnRegistration(RegistrationResponse registrationResponse)
    {
        Debug.Log("User ID: " + registrationResponse.UserId);
        Debug.Log("New User Registered.");
    }

    private void OnAuthentication(AuthenticationResponse authenticationResponse)
    {
        Debug.Log("User ID: " + authenticationResponse.UserId);
        Debug.Log("User Authenticated.");
    }

    private void OnMatchFound(MatchFoundMessage matchFoundMessage)
    {
        Debug.Log("Match Found!");
        Debug.Log("Host URL: " + matchFoundMessage.Host);
        Debug.Log("Port: " + matchFoundMessage.Port);
        Debug.Log("Access Token: " + matchFoundMessage.AccessToken);
        Debug.Log("Match Id: " + matchFoundMessage.MatchId);
        Debug.Log("Opponents: " + matchFoundMessage.Participants.Count());

        foreach (MatchFoundMessage._Participant participant in matchFoundMessage.Participants)
        {
            Debug.Log("Player: " + participant.PeerId);
        }

        sessionInformation = new SessionInformation(matchFoundMessage);
    }
}
