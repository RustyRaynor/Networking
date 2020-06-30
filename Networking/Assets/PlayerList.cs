using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class PlayerList : MonoBehaviour
{
    public Text[] playerNames;
    [SerializeField] Text ID;

   public void ResetNames()
   {
        Debug.Log("Reset");

        for (int i = 0; i < playerNames.Length; i++)
       {
           playerNames[i].text = "Waiting for Player";
       }
   
     //  for (int i = 0; i < Matchmaking.instance.playerNames.Count; i++)
     //  {
     //      if(Matchmaking.instance.playerNames[i].matchID == Player.localPlayer.matchID)
     //       {
     //           Debug.Log("Player Amount: " + Matchmaking.instance.playerNames[i].playerNames.Count);
     //           for (int x = 0; x < Matchmaking.instance.playerNames[i].playerNames.Count; x++)
     //           {
     //               playerNames[x].text = Matchmaking.instance.playerNames[i].playerNames[x];
     //           }
     //       }
     //  }
   
       ID.text = "Match ID " + Player.localPlayer.matchID;
   }
}
