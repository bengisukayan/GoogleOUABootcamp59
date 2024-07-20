using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character List", menuName = "Characters/List")]
public class CharacterList : ScriptableObject
{
    [SerializeField] private Character[] characters = new Character[2];

    public Character[] GetAllCharacters() => characters;

    public Character GetCharacterById(int id)
    {
        foreach (var character in characters)
        {
            if (character.Id == id)
            {
                return character;
            }
        }
        return null;
    }

    public bool IsValidCharacterId(int id)
    {
        return characters.Any(x => x.Id == id);
    }
}
