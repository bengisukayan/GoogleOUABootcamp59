using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class CharacterSpawner : NetworkBehaviour
{
    [Header("References")]
    [SerializeField] private CharacterList characters;

    private IEnumerator WaitAndSpawnCoroutine()
    {
        Debug.Log("Starting wait coroutine");
        yield return new WaitForSeconds(3);
        Debug.Log("Wait coroutine finished, spawning characters");
        SpawnCharacters();
    }

    public override void OnNetworkSpawn()
    {
        if (!IsServer) { return; }

        Debug.Log("OnNetworkSpawn called, starting coroutine");
        StartCoroutine(WaitAndSpawnCoroutine());
    }

    private void SpawnCharacters()
    {
        Debug.Log("Spawning characters");
        foreach (var client in ServerManager.Instance.ClientData)
        {
            var character = characters.GetCharacterById(client.Value.characterId);
            if (character != null)
            {
                Vector3 spawnPos = Vector3.zero;
                if (client.Value.characterId == 1)
                {
                    spawnPos = new Vector3(-13f, 0.6f, -56f); // Change spawnpoint for characterId 1
                }
                else if (client.Value.characterId == 2)
                {
                    spawnPos = new Vector3(-9f, 0.6f, -56f); // Change spawnpoint for characterId 2
                }

                var characterInstance = Instantiate(character.GameplayPrefab, spawnPos, Quaternion.identity);
                Debug.Log($"Instantiated character for client {client.Value.clientId} at position {spawnPos}");
                characterInstance.SpawnAsPlayerObject(client.Value.clientId);
            }
            else
            {
                Debug.LogWarning($"Character not found for client {client.Value.clientId} with characterId {client.Value.characterId}");
            }
        }
    }
}