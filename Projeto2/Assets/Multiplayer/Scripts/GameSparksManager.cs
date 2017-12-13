using System.Collections;
using System.Collections.Generic;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using GameSparks.Core;
using GameSparks.RT;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSparksManager : MonoBehaviour
{
    private static GameSparksManager instance;

    public static GameSparksManager Instance()
    {
        if (instance != null)
        {
            return instance;
        }

        else
        {
            Debug.Log("GameSparksmanager Not Initialized");
            return null;
        }
    }

    public GameSparksRTUnity GameSparksRtUnity { get; set; }
    public SessionInformation SessionInformation { get; set; }

    public delegate void AuthenticationCallback(AuthenticationResponse authenticationResponse);
    public delegate void RegistrationCallback(RegistrationResponse registrationResponse);

    public void AuthenticateUser(RegistrationCallback registrationCallback, AuthenticationCallback authenticationCallback)
    {
        new RegistrationRequest().Send((registrationResposnse) =>
        {
            if (!registrationResposnse.HasErrors)
            {
                Debug.Log("Registration Successful");
                registrationCallback(registrationResposnse);
            }
            else
            {
                if (registrationResposnse.NewPlayer == false)
                {
                    new AuthenticationRequest().Send((authenticationResponse) =>
                        {
                            if (!authenticationResponse.HasErrors)
                            {
                                Debug.Log("Authentication Successful");
                                authenticationCallback(authenticationResponse);
                            }
                            else
                            {
                                Debug.Log("Authentication Error " + authenticationResponse.Errors.JSON);
                            }
                        });
                }
                else
                {
                    Debug.Log("Authentication Error " + registrationResposnse.Errors.JSON);
                }
            }
        });
    }

    public void FindPlayers()
    {
        Debug.Log("Attempting Matchmaking");
        new MatchmakingRequest().SetMatchShortCode("MRLMatch").SetSkill(0)
            .Send((matchmakingResponse) =>
            {
                if (matchmakingResponse.HasErrors)
                {
                    Debug.Log("Matchmaking Error " + matchmakingResponse.Errors.JSON);
                }
            });
    }

    public void StartNewRtSession(SessionInformation rtSessionInfo)
    {
        Debug.Log("Creating New RT Session Instance");
        SessionInformation = rtSessionInfo;
        GameSparksRtUnity = gameObject.AddComponent<GameSparksRTUnity>();

        GSRequestData requestData = new GSRequestData().AddNumber("port", (double)rtSessionInfo.PortId).AddString("host", rtSessionInfo.HostUrl).AddString("accessToken", rtSessionInfo.AccessToken);

        Debug.Log((double)rtSessionInfo.PortId);
        Debug.Log(rtSessionInfo.HostUrl);
        Debug.Log(rtSessionInfo.AccessToken);

        FindMatchResponse findMatchResponse = new FindMatchResponse(requestData);

        GameSparksRtUnity.Configure(findMatchResponse,
            (peerId) =>
            {
                OnPlayerConnected(peerId);
            },
            (peerId) =>
            {
                OnPlayerDisconnected(peerId);
            },
            (ready) =>
            {
                OnRtReady(ready);
            },
            (packet) =>
            {
                //OnPacketReceived(packet);
            });
        GameSparksRtUnity.Connect();
    }

    private void OnPlayerConnected(int peerId)
    {
        Debug.Log("Player Connected " + peerId);
    }

    private void OnPlayerDisconnected(int peerId)
    {
        Debug.Log("Player Disconnected " + peerId);
    }

    private void OnRtReady(bool isReady)
    {
        if (isReady)
        {
            Debug.Log("RT Sessions Connected");
        }
    }

    /*private void OnPacketReceived(RTPacket rtPacket)
    {
        if (GameController.Instance() != null)
        {
            GameController.Instance().PacketReceived(rtPacket.PacketSize);
        }
        else
        {
            return;
        }

        switch (rtPacket.OpCode)
        {
            case 1:
                if (ChatManager == null)
                {
                    ChatManager = GameObject.Find("ChatManager").GetComponent<ChatManager>();
                }
                ChatManager.OnMessageReceived(rtPacket);
                break;
            case 2:
                GameController.Instance().UpdateOpponentShips(rtPacket);
                break;
            case 3:
                GameController.Instance().InstantiateOpponentBullet(rtPacket);
                break;
            case 4:
                GameController.Instance().UpdateOpponentBullets(rtPacket);
                break;
            case 5:
                GameController.Instance().RegisterOpponentCollision(rtPacket);
                break;
            case 100:
                SceneManager.LoadScene(1);
                break;
            case 101:
                GameController.Instance().CalculateTimeDelta(rtPacket);
                break;
            case 102:
                GameController.Instance().SyncClock(rtPacket);
                break;
        }
    }*/

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
