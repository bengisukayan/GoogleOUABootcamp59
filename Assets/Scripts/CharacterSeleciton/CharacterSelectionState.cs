using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public struct CharacterSelectionState : INetworkSerializable, IEquatable<CharacterSelectionState>
{
    public ulong ClientId;
    public int CharacterId;
    public bool IsLockedIn;

    public CharacterSelectionState(ulong clientId, int characterId = -1, bool isLockedIn = false)
    {
        ClientId = clientId;
        CharacterId = characterId;
        IsLockedIn = isLockedIn;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref ClientId);
        serializer.SerializeValue(ref CharacterId);
        serializer.SerializeValue(ref IsLockedIn);
    }

    public bool Equals(CharacterSelectionState other)
    {
        return ClientId == other.ClientId && CharacterId == other.CharacterId && IsLockedIn == other.IsLockedIn;
    }
}
