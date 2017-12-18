using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private static GameController _instance;

    public GameObject[] PlayerPrefabs;
    public Text[] PlayerNameList;
    private PlayerControl[] playersList;

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
        playersList = new PlayerControl[GameSparksManager.Instance().SessionInformation.PlayersList.Count];

        for (int player = 0; player < GameSparksManager.Instance().SessionInformation.PlayersList.Count; player++)
        {
            for (int spawnPoint = 0; spawnPoint < spawnPoints.Length; spawnPoint++)
            {
                if (spawnPoints[spawnPoint].PlayerPeerId == GameSparksManager.Instance().SessionInformation.PlayersList[player].PeerId)
                {
                    GameObject newPlayer = Instantiate(PlayerPrefabs[player], spawnPoints[spawnPoint].gameObject.transform.position, spawnPoints[spawnPoint].gameObject.transform.rotation);

                    newPlayer.name = GameSparksManager.Instance().SessionInformation.PlayersList[player].PeerId.ToString();
                    newPlayer.transform.SetParent(transform);

                    if (GameSparksManager.Instance().SessionInformation.PlayersList[player].PeerId ==
                        GameSparksManager.Instance().GameSparksRtUnity.PeerId)
                    {
                        //newPlayer.GetComponent<PlayerControl>().
                    }

                    playersList[player] = newPlayer.GetComponent<PlayerControl>();
                    PlayerNameList[player].text = GameSparksManager.Instance().SessionInformation.PlayersList[player].DisplayName;
                    break;
                }
            }
        }
    }
}
