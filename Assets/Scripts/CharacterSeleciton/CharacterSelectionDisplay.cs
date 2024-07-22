using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Grpc.Core;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionDisplay : NetworkBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject characterInfoPanel;
    [SerializeField] private TMP_Text characterNameText;
    [SerializeField] private CharacterList characters;
    [SerializeField] private Transform charactersHolder;
    [SerializeField] private CharacterSelectionButton characterSelectionButton;
    [SerializeField] private Players[] playerCards;
    [SerializeField] private Transform introSpawnPoint;
    [SerializeField] private Button lockInButton;
    [SerializeField] private TMP_Text joinCodeText;

    public NetworkList<CharacterSelectionState> players;
    private GameObject introInstance;
    private List<CharacterSelectionButton> characterButtons = new List<CharacterSelectionButton>();

    private void Awake()
    {
        players = new NetworkList<CharacterSelectionState>();
    }

    public override void OnNetworkSpawn()
    {
        if (IsClient)
        {
            Character[] allCharacters = characters.GetAllCharacters();

            foreach (var character in allCharacters)
            {
                var selectbuttonInstance = Instantiate(characterSelectionButton, charactersHolder);
                selectbuttonInstance.SetCharacter(this, character);
                characterButtons.Add(selectbuttonInstance);
            }

            players.OnListChanged += HandlePlayersStateChanged;
        }

        if (IsServer)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += HandleClientConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback += HandleClientDisconnected;

            foreach (NetworkClient client in NetworkManager.Singleton.ConnectedClientsList)
            {
                HandleClientConnected(client.ClientId);
            }
            joinCodeText.text = ServerManager.Instance.JoinCode;
        }
    }

    public override void OnNetworkDespawn()
    {
        if (IsClient)
        {
            players.OnListChanged -= HandlePlayersStateChanged;
        }

        if (IsServer)
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= HandleClientConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback -= HandleClientDisconnected;
        }
    }

    private void HandleClientConnected(ulong clientId)
    {
        players.Add(new CharacterSelectionState(clientId));
    }

    private void HandleClientDisconnected(ulong clientId)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].ClientId != clientId) { continue; }

            players.RemoveAt(i);
            break;
        }
    }

    public void Select(Character character)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].ClientId != NetworkManager.Singleton.LocalClientId) { continue; }
            if (players[i].IsLockedIn) { return; }
            if (players[i].CharacterId == character.Id) { return; }
            if (IsCharacterTaken(character.Id, false)) { return; }
        }

        characterNameText.text = character.DisplayName;
        characterInfoPanel.SetActive(true);

        if (introInstance != null)
        {
            Destroy(introInstance);
        }

        introInstance = Instantiate(character.IntroPrefab, introSpawnPoint);
        SelectServerRpc(character.Id);
    }

    [ServerRpc(RequireOwnership = false)]
    private void SelectServerRpc(int characterId, ServerRpcParams serverRpcParams = default)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].ClientId != serverRpcParams.Receive.SenderClientId) { continue; }
            if (!characters.IsValidCharacterId(characterId)) { return; }
            if (IsCharacterTaken(characterId, true)) { return; }

            players[i] = new CharacterSelectionState(players[i].ClientId, characterId, players[i].IsLockedIn);
        }
    }

    public void LockIn()
    {
        LockInServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    private void LockInServerRpc(ServerRpcParams serverRpcParams = default)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].ClientId != serverRpcParams.Receive.SenderClientId) { continue; }
            if (!characters.IsValidCharacterId(players[i].CharacterId)) { return; }
            if (IsCharacterTaken(players[i].CharacterId, true)) { return; }
            
            players[i] = new CharacterSelectionState(players[i].ClientId, players[i].CharacterId, true);
        }

        foreach (var player in players)
        {
            if (!player.IsLockedIn) { return; }
        }

        foreach (var player in players)
        {
            ServerManager.Instance.SetCharacter(player.ClientId, player.CharacterId);
        }

        if (players.Count == 2)
        {
            ServerManager.Instance.StartGame();
        }
    }

    private void HandlePlayersStateChanged(NetworkListEvent<CharacterSelectionState> changeEvent)
    {
        for (int i = 0; i < playerCards.Length; i++)
        {
            if (players.Count > i)
            {
                playerCards[i].UpdateDisplay(players[i]);
            }
            else
            {
                playerCards[i].DisableDisplay();
            }
        }

        foreach (var button in characterButtons)
        {
            if (button.IsDisabled) { continue; }
            if (IsCharacterTaken(button.Character.Id, false))
            {
                button.SetDisabled();
            }
        }

        foreach (var player in players)
        {
            if (player.ClientId != NetworkManager.Singleton.LocalClientId) { continue; }
            if (player.IsLockedIn)
            {
                lockInButton.interactable = false;
                break;
            }
            if (IsCharacterTaken(player.CharacterId, false))
            {
                lockInButton.interactable = false;
                break;
            }

            lockInButton.interactable = true;
            break;
        }
    }

    private bool IsCharacterTaken(int characterId, bool checkAll)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (!checkAll)
            {
                if (players[i].ClientId == NetworkManager.Singleton.LocalClientId) { continue; }
            }
            if (players[i].IsLockedIn && players[i].CharacterId == characterId)
            {
                return true;
            }
        }
        return false;
    }
}
