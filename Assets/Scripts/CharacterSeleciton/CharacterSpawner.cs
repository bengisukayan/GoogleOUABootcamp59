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
                var spawnPos = new Vector3(Random.Range(-3f, 3f), 0f, Random.Range(-3f, 3f)); //change spawnpoint
                var characterInstance = Instantiate(character.GameplayPrefab, spawnPos, Quaternion.identity);
                characterInstance.SpawnAsPlayerObject(client.Value.clientId);
            }
        }
    }
}
