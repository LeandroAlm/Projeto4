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
    public Text[] PlayerNameList;
    private PLayerControl[] playersList;

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
        //SpawnPoint[] spawnPoints = FindObjectsOfType(typeof(SpawnPoint)) as SpawnPoint[];

        playersList = new PLayerControl[GameSparksManager.Instance().SessionInformation.PlayersList.Count];

        for (int player = 0; player < GameSparksManager.Instance().SessionInformation.PlayersList.Count; player++)
        {
            GameObject newPlayer = Instantiate(PlayerPrefabs[player], PlayerPrefabs[player].transform.position, PlayerPrefabs[player].transform.rotation);

            newPlayer.name = GameSparksManager.Instance().SessionInformation.PlayersList[player].PeerId.ToString();
            newPlayer.transform.SetParent(transform);

            if (GameSparksManager.Instance().SessionInformation.PlayersList[player].PeerId == GameSparksManager.Instance().GameSparksRtUnity.PeerId)
            {
                Debug.Log("Entrou caralho!");
                newPlayer.GetComponent<PLayerControl>().SetupPlayer(new Vector3(70, 0, 90));
            }

            playersList[player] = newPlayer.GetComponent<PLayerControl>();
            PlayerNameList[player].text = GameSparksManager.Instance().SessionInformation.PlayersList[player].DisplayName;
            break;

            /*for (int spawnPoint = 0; spawnPoint < spawnPoints.Length; spawnPoint++)
            {
                if (spawnPoints[spawnPoint].PlayerPeerId == GameSparksManager.Instance().SessionInformation.PlayersList[player].PeerId)
                {
                }
            }*/
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
                playersList[i].transform.position = new Vector3(rtPacket.Data.GetVector4(1).Value.x, rtPacket.Data.GetVector4(1).Value.y, rtPacket.Data.GetVector4(1).Value.z);
                playersList[i].turnAmount = rtPacket.Data.GetFloat(2).Value;
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
