using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionButton : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private GameObject disabledOverlay;
    [SerializeField] private Button button;

    private CharacterSelectionDisplay characterSelect;

    public Character Character { get; private set; }
    public bool IsDisabled { get; private set; }

    public void SetCharacter(CharacterSelectionDisplay characterSelect, Character character)
    {
        iconImage.sprite = character.Icon;
        this.characterSelect = characterSelect;
        Character = character;
    }

    public void SelectCharacter()
    {
        characterSelect.Select(Character);
    }

    public void SetDisabled()
    {
        IsDisabled = true;
        disabledOverlay.SetActive(true);
        button.interactable = false;
    }
}
