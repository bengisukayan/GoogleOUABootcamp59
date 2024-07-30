using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class BoxInteract : NetworkBehaviour, IInteractable
{
    public void Interact()
    {
        if (IsServer)
        {
            ChangeColorClientRpc(Color.red);
        }
        else
        {
            ChangeColorServerRpc(Color.red);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void ChangeColorServerRpc(Color color)
    {
        ChangeColorClientRpc(color);
    }

    [ClientRpc]
    private void ChangeColorClientRpc(Color color)
    {
        gameObject.GetComponent<Renderer>().material.color = color;
        Debug.Log("Color changed to red for all clients");
    }
}