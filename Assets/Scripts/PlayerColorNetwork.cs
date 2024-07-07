using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Connection;
using FishNet.Object;

public class PlayerColorNetwork : NetworkBehaviour
{
    public GameObject body;
    public Color endColor;
    public override void OnStartClient()
    {
        base.OnStartClient();
        if (base.IsOwner)
        {

        }
        else
        {
            GetComponent<PlayerColorNetwork>().enabled = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) 
        {
            ChangeColorServer(gameObject, endColor);
        }
    }

    [ServerRpc]
    public void ChangeColorServer(GameObject player, Color color)
    {
        ChangeColor(player, color);
    }

    [ObserversRpc]
    public void ChangeColor(GameObject player, Color color) 
    {
        player.GetComponent<PlayerColorNetwork>().body.GetComponent<Renderer>().material.color = color;
    }
}
