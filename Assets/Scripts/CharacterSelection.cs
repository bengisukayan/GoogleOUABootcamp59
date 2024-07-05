using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using JetBrains.Annotations;
using FishNet.Connection;

public class CharacterSelection : NetworkBehaviour
{
    [SerializeField] private List<GameObject> characters = new List<GameObject>();
    [SerializeField] private GameObject characterSelectorPanel;
    [SerializeField] private GameObject canvasObject;

    public override void OnStartClient()
    {
        base.OnStartClient();

        if(!base.IsOwner)
            canvasObject.SetActive(false);
    }

    public void SpawnPlayer1() 
    {
        characterSelectorPanel.SetActive(false);
        Spawn(0, LocalConnection);
    }
    public void SpawnPlayer2()
    {
        characterSelectorPanel.SetActive(false);
        Spawn(1, LocalConnection);
    }

    [ServerRpc(RequireOwnership = false)]
    void Spawn(int swapnIndex, NetworkConnection con)
    {
        GameObject player = Instantiate(characters[swapnIndex], SpawnPoint.instance.transform.position, Quaternion.identity);
        Spawn(player, con);
    }

}
