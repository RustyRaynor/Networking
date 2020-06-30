using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class UILobby : NetworkBehaviour
{
    public static UILobby instance;
    [SerializeField] InputField matchIDInput;
    [SerializeField] InputField nameInput;
    [SerializeField] Button joinButton;
    [SerializeField] Button hostButton;
    [SerializeField] Canvas lobby;

    PlayerList lobbyCode;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }

        lobbyCode = lobby.GetComponent<PlayerList>();
    }

    public void Host()
    {
        UIChange(false);
        Player.localPlayer.HostGame(nameInput.text);
    }

    // Update is called once per frame
    public void Join()
    {
        UIChange(false);
        Player.localPlayer.JoinGame(matchIDInput.text.ToUpper(), nameInput.text);
    }

    public void HostSuccess(string matchNo)
    {
        UIChange(false);
        lobby.enabled = true;
    }

    public void JoinSuccess(bool success)
    {
        if (success)
        {
            UIChange(false);
            lobby.enabled = true;
        }
        else
        {
            UIChange(true);
            matchIDInput.text = "Wrong ID";
        }
    }

    void UIChange(bool state)
    {
        joinButton.interactable = state;
        hostButton.interactable = state;
        matchIDInput.interactable = state;
    }

    public void UpdateLobby()
    {
        Debug.Log("wow");
        lobbyCode.ResetNames();
    }
}
