using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public struct CharacterSelectionState : INetworkSerializable, IEquatable<CharacterSelectionState>
{
    public ulong ClientId;
    public int CharacterId;

    public CharacterSelectionState(ulong clientId, int characterId = -1)
    {
        ClientId = clientId;
        CharacterId = characterId;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref ClientId);
        serializer.SerializeValue(ref CharacterId);
    }

    public bool Equals(CharacterSelectionState other)
    {
        return ClientId == other.ClientId && CharacterId == other.CharacterId;
    }
}
