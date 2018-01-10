using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using GameSparks.RT;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private static GameController _instance;

    public GameObject[] PlayerPrefabs;
    //public Text[] PlayerNameList;
    private PLayerControl[] playersList;


    private int _timeDelta;
    private int _latency;
    private int _roundTrip;
    private DateTime _serverClock;
    private bool _clockStarted;
    private DateTime _endTime;


    public static GameController Instance()
    {
        return _instance;
    }

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        SpawnPoint[] spawnPoints = FindObjectsOfType(typeof(SpawnPoint)) as SpawnPoint[];

        Debug.Log("Sessions info: " + GameSparksManager.Instance().SessionInformation);
        Debug.Log("Player List Count: " + GameSparksManager.Instance().SessionInformation.PlayersList.Count);

        playersList = new PLayerControl[GameSparksManager.Instance().SessionInformation.PlayersList.Count];

        for (int playerIndex = 0; playerIndex < GameSparksManager.Instance().SessionInformation.PlayersList.Count; playerIndex++)
        {
            for (int spawnIndex = 0; spawnIndex < spawnPoints.Length; spawnIndex++)
            {
                if (spawnPoints[spawnIndex].PlayerPeerId == GameSparksManager.Instance().SessionInformation.PlayersList[playerIndex].PeerId)
                {
                    GameObject newPlayer = Instantiate(PlayerPrefabs[playerIndex], spawnPoints[spawnIndex].transform.position, spawnPoints[spawnIndex].transform.rotation);

                    newPlayer.name = GameSparksManager.Instance().SessionInformation.PlayersList[playerIndex].PeerId.ToString();
                    newPlayer.transform.SetParent(transform);

                    if (GameSparksManager.Instance().SessionInformation.PlayersList[playerIndex].PeerId == GameSparksManager.Instance().GameSparksRtUnity.PeerId)
                    {
                        Debug.Log("Entrou!");
                        Debug.Log("Spawn Index: " + spawnIndex);
                        newPlayer.GetComponent<PLayerControl>().SetupPlayer(spawnPoints[spawnIndex].gameObject.transform, true);
                    }

                    else
                    {
                        newPlayer.GetComponent<PLayerControl>().SetupPlayer(spawnPoints[spawnIndex].gameObject.transform, false);
                    }

                    playersList[playerIndex] = newPlayer.GetComponent<PLayerControl>();
                    //PlayerNameList[playerIndex].text = GameSparksManager.Instance().SessionInformation.PlayersList[playerIndex].DisplayName;
                    break;
                }
            }
        }

        StartCoroutine(SendTimeStamp());
    }

    private IEnumerator SendTimeStamp()
    {
        using (RTData data = RTData.Get())
        {
            data.SetLong(1, (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds);
            GameSparksManager.Instance().GameSparksRtUnity
                .SendData(101, GameSparksRT.DeliveryIntent.UNRELIABLE, data, 0);
        }

        yield return new WaitForSeconds(5f);
        StartCoroutine(SendTimeStamp());
    }

    public void CalculateTimeDelta(RTPacket rtPacket)
    {
        _roundTrip = (int)((long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds - rtPacket.Data.GetLong(1).Value);
        _latency = _roundTrip / 2;
        int serverDelta = (int)(rtPacket.Data.GetLong(2).Value - (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds);
        _timeDelta = serverDelta + _latency;
    }

    public void SyncClock(RTPacket rtPacket)
    {
        DateTime dateTimeNow = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        _serverClock = dateTimeNow.AddMilliseconds(rtPacket.Data.GetLong(1).Value + _timeDelta).ToLocalTime();

        if (!_clockStarted)
        {
            _endTime = _serverClock.AddMilliseconds(60000 + _timeDelta);
        }

        if ((_endTime - _serverClock).TotalMilliseconds <= 0)
        {
            GameSparksManager.Instance().GameSparksRtUnity.Disconnect();
        }
    }

    private int _packetSizeIncomming;
    private int _packetSizeSent;

    public void PacketReceived(int packetSize)
    {
        _packetSizeIncomming = packetSize;
    }
    public void SendRTData(int opCode, GameSparksRT.DeliveryIntent intent, RTData data, int[] targetPeers)
    {
        _packetSizeSent = GameSparksManager.Instance().GameSparksRtUnity.SendData(opCode, intent, data, targetPeers);
    }
    public void SendRTData(int opCode, GameSparksRT.DeliveryIntent intent, RTData data)
    {
        _packetSizeSent = GameSparksManager.Instance().GameSparksRtUnity.SendData(opCode, intent, data);
    }

    public void UpdatePartnerMovement(RTPacket rtPacket)
    {
        for (int i = 0; i < playersList.Length; i++)
        {
            if (playersList[i].name == rtPacket.Sender.ToString())
            {
                Debug.Log("Entrou no UpdatePartnerMovement");

                playersList[i].GoToPosition = new Vector4(rtPacket.Data.GetVector4(1).Value.x, rtPacket.Data.GetVector4(1).Value.y,
                    rtPacket.Data.GetVector4(1).Value.z, rtPacket.Data.GetVector4(1).Value.w);
                playersList[i].GoToRotation = rtPacket.Data.GetFloat(2).Value;
            }
        }
    }

    public void RegisterPartnerCollision(RTPacket rtPacket)
    {
        for (int i = 0; i < playersList.Length; i++)
        {
            if (playersList[i].name == rtPacket.Data.GetString(1))
            {

            }

            if (rtPacket.Sender != GameSparksManager.Instance().GameSparksRtUnity.PeerId && playersList[i].name == rtPacket.Data.GetString(2))
            {

            }
        }
    }
}