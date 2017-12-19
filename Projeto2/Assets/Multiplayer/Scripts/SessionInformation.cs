using System.Collections;
using System.Collections.Generic;
using GameSparks.Api.Messages;
using UnityEngine;

public class SessionInformation : MonoBehaviour
{
    public string HostUrl { get; set; }
    public string AccessToken { get; set; }
    public int PortId { get; set; }
    public string MatchId { get; set; }
    public List<PlayerInformation> PlayersList { get; set; }

    public SessionInformation(MatchFoundMessage matchFoundMessage)
    {
        PortId = (int)matchFoundMessage.Port;
        HostUrl = matchFoundMessage.Host;
        AccessToken = matchFoundMessage.AccessToken;
        MatchId = matchFoundMessage.MatchId;

        PlayersList = new List<PlayerInformation>();

        foreach (MatchFoundMessage._Participant participant in matchFoundMessage.Participants)
        {
            PlayersList.Add(new PlayerInformation(participant.DisplayName, participant.Id, (int)participant.PeerId));  
        }
    }
}
