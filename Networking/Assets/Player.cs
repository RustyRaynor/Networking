using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    public static Player localPlayer;

    NetworkMatchChecker matcher;

    [SyncVar] public string matchID;
    [SyncVar] public string playerName;

    private void Start()
    {
        if (isLocalPlayer)
        {
            localPlayer = this;
        }

        matcher = GetComponent<NetworkMatchChecker>();
    }

    public void HostGame(string name)
    {
        string matchID = Matchmaking.instance.GetMatchId();
        playerName = name;
        this.matchID = matchID;
        CmdHostGame(matchID, name);
    }

    [Command]
    void CmdHostGame(string matchID, string name)
    {
        playerName = name;
        this.matchID = matchID;
        Matchmaking.instance.HostGame(matchID, gameObject);
        matcher.matchId = Matchmaking.instance.ToGuid(matchID);
        TargetHostGame(matchID);
    }

    [TargetRpc]
    void TargetHostGame(string matchID)
    {
        UILobby.instance.HostSuccess(matchID);
    }

    public void JoinGame(string matchID, string name)
    {
        playerName = name;
        this.matchID = matchID;
        CmdJoinGame(matchID, name);
    }

    [Command]
    void CmdJoinGame(string matchID, string name)
    {
        playerName = name;
        this.matchID = matchID;
        if (Matchmaking.instance.JoinGame(matchID, gameObject))
        {
            matcher.matchId = Matchmaking.instance.ToGuid(matchID);
            TargetJoinGame(true, matchID);
        }
        else
        {
            Debug.Log("ID not found");
            TargetJoinGame(false, matchID);
        }
    }

    [TargetRpc]
    void TargetJoinGame(bool success, string matchID)
    {
        UILobby.instance.JoinSuccess(success);
    }
}