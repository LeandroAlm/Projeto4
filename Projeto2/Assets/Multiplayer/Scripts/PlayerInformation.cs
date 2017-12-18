using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInformation : MonoBehaviour
{
    public string DisplayName { get; set; }
    public string Id { get; set; }
    public int PeerId { get; set; }
    public bool IsOnline { get; set; }

    public PlayerInformation(string displayName, string id, int peerId)
    {
        DisplayName = displayName;
        Id = id;
        PeerId = peerId;
    }
}
