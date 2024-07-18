using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionButton : MonoBehaviour
{
    [SerializeField] private Image iconImage;

    private CharacterSelectionDisplay characterSelect;
    private Character character;
    public bool IsDisabled { get; private set; }

    public void SetCharacter(CharacterSelectionDisplay characterSelect, Character character)
    {
        iconImage.sprite = character.Icon;

        this.characterSelect = characterSelect;
        this.character = character;
    }

    public void SelectCharacter()
    {
        characterSelect.Select(character);
    }
}
