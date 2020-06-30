using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AutoConnect : MonoBehaviour
{
    [SerializeField] NetworkManager network;
    // Start is called before the first frame update
    void Start()
    {
        if (!Application.isBatchMode)
        {
            network.StartClient();
        }
        else
        {
            Debug.Log("Tis the server");
        }
    }

    public void JoinLocal()
    {
        network.networkAddress = "localhost";
        network.StartClient();
    }
}
