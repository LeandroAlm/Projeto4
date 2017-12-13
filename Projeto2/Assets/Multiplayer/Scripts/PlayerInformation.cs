using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInformation : MonoBehaviour
{
    public string Id { get; set; }
    public int PeerId { get; set; }
    public bool IsOnline { get; set; }

    public PlayerInformation(string id, int peerId)
    {
        Id = id;
        PeerId = peerId;
    }
}
