using System.Collections;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

//public class Match
//{
//    public string matchID;
//    public SyncListGameObjects players = new SyncListGameObjects();
//
//    public Match(string matchId, GameObject player)
//    {
//        this.matchID = matchId;
//        players.Add(player);
//    }
//
//    public Match()
//    {
//
//    }
//}

[System.Serializable]
public class MatchNames
{
    public string matchID;
    public SyncListString playerNames = new SyncListString();

    public MatchNames(string matchId, string name)
    {
        this.matchID = matchId;
        playerNames.Add(name);
    }

    public MatchNames()
    {

    }
}

public class SyncListGameObjects : SyncList<GameObject> { }

//public class SyncListMatch : SyncList<Match> { }

[System.Serializable]
public class SyncListMatchNames : SyncList<MatchNames> { }

public class Matchmaking : NetworkBehaviour
{
    public static Matchmaking instance;

    //public SyncListMatch matches = new SyncListMatch();
    public SyncListMatchNames playerMatch = new SyncListMatchNames();

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        OnClientStart();
    }

    [Client]
    public void OnClientStart()
    {
        Debug.Log("onStart");
        playerMatch.Callback += PlayerMatch_Callback;
    }

    private void PlayerMatch_Callback(SyncList<MatchNames>.Operation op, int itemIndex, MatchNames oldItem, MatchNames newItem)
    {
        Debug.Log("It printed yo");
        Debug.Log(newItem.matchID);
    }

    //private void PlayerNames_Callback(SyncList<MatchNames>.Operation op, int itemIndex, MatchNames oldItem, MatchNames newItem)
    //{
    //    Debug.Log("Callback");
    //    Debug.Log(newItem.matchID);
    //   //for (int i = 0; i < newItem.playerNames.Count; i++)
    //   //{
    //   //    Debug.Log(newItem.playerNames[i]);
    //   //}
    //   // switch (op)
    //   // {
    //   //     case SyncListItem.Operation.OP_ADD:
    //   //         // index is where it got added in the list
    //   //         // item is the new item
    //   //         break;
    //   //     case SyncListItem.Operation.OP_CLEAR:
    //   //         // list got cleared
    //   //         break;
    //   //     case SyncListItem.Operation.OP_INSERT:
    //   //         // index is where it got added in the list
    //   //         // item is the new item
    //   //         break;
    //   //     case SyncListItem.Operation.OP_REMOVEAT:
    //   //         // index is where it got removed in the list
    //   //         // item is the item that was removed
    //   //         break;
    //   //     case SyncListItem.Operation.OP_SET:
    //   //         // index is the index of the item that was updated
    //   //         // item is the previous item
    //   //         break;
    //   // }
    //    UILobby.instance.UpdateLobby();
    //}

    public string GetMatchId()
    {
        string matchID = string.Empty;
        bool chosen = false;

        while (!chosen)
        {
            for (int i = 0; i < 5; i++)
            {
                int random = UnityEngine.Random.Range(0, 36);
                if (random < 26)
                {
                    matchID += (char)(random + 65);
                }
                else
                {
                    matchID += (random - 26).ToString();
                }
            }

            for (int i = 0; i < playerMatch.Count; i++)
            {
                if (matchID == playerMatch[i].matchID)
                {
                    chosen = false;
                    break;
                }
            }

            chosen = true;
        }
        Debug.Log(matchID);
        return matchID;
    }

    public void HostGame(string matchID, GameObject player)
    {
        // matches.Add(new Match(matchID, player));
        playerMatch.Add(new MatchNames(matchID, player.GetComponent<Player>().playerName));
    }

    public bool JoinGame(string matchID, GameObject player)
    {
        bool found = false;
        for (int i = 0; i < playerMatch.Count; i++)
        {
            if(playerMatch[i].matchID == matchID)
            {
                // matches[i].players.Add(player);
                playerMatch[i].playerNames.Add(player.GetComponent<Player>().playerName);
                //playerMatch.Add(new MatchNames("asldkas;da", "nameyo"));
                found = true;
                break;
            }
        }
        return found;
    }

    public Guid ToGuid(string matchID)
    {
        MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
        byte[] inputBytes = Encoding.Default.GetBytes(matchID);
        byte[] hashBytes = provider.ComputeHash(inputBytes);

        return new Guid(hashBytes);
    }
}
