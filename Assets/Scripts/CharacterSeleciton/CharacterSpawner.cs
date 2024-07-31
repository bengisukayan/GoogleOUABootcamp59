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
        yield return new WaitForSeconds(5);
        SpawnCharacters();
    }

    public override void OnNetworkSpawn()
    {
        if (!IsServer) { return; }

        StartCoroutine(WaitAndSpawnCoroutine());
    }

    private void SpawnCharacters()
    {
        foreach (var client in ServerManager.Instance.ClientData)
        {
            var character = characters.GetCharacterById(client.Value.characterId);
            if (character != null)
            {
                Vector3 spawnPos = Vector3.zero;
                if (client.Value.characterId == 1)
                {
                    spawnPos = new Vector3(-13f, 0.6f, -56f); // Change spawnpoint for characterId 1
                    Debug.Log("arslan spawned at " + spawnPos);
                }
                else if (client.Value.characterId == 2)
                {
                    spawnPos = new Vector3(-9f, 0.6f, -56f); // Change spawnpoint for characterId 2
                    Debug.Log("suzan spawned at " + spawnPos);
                }

                var characterInstance = Instantiate(character.GameplayPrefab, spawnPos, Quaternion.identity);
                characterInstance.SpawnAsPlayerObject(client.Value.clientId);
            }
        }
    }
}