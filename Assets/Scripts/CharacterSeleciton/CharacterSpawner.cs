using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class CharacterSpawner : NetworkBehaviour
{
    [Header("References")]
    [SerializeField] private CharacterList characters;

    public override void OnNetworkSpawn()
    {
        if (!IsServer) { return; }

        foreach (var client in ServerManager.Instance.ClientData)
        {
            var character = characters.GetCharacterById(client.Value.characterId);
            if (character != null)
            {
                if (client.Value.characterId == 1)
                {
                    var spawnPos = new Vector3(-3f, 0.49f, 0f); //change spawnpoint
                    var characterInstance = Instantiate(character.GameplayPrefab, spawnPos, Quaternion.identity);
                    characterInstance.SpawnAsPlayerObject(client.Value.clientId);
                }
                else if (client.Value.characterId == 2)
                {
                    var spawnPos = new Vector3(3f, 0.4f, 0f); //change spawnpoint
                    var characterInstance = Instantiate(character.GameplayPrefab, spawnPos, Quaternion.identity);
                    characterInstance.SpawnAsPlayerObject(client.Value.clientId);
                }
            }
        }
    }
}
