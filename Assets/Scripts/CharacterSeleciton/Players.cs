using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Players : MonoBehaviour
{
    [SerializeField] private CharacterList characters;
    [SerializeField] private GameObject elements;
    [SerializeField] private Image characterIconImage;
    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private TMP_Text characterNameText;

    public void UpdateDisplay(CharacterSelectionState state)
    {
        if (state.CharacterId != -1)
        {
            var character = characters.GetCharacterById(state.CharacterId);
            characterIconImage.sprite = character.Icon;
            characterIconImage.enabled = true;
            characterNameText.text = character.DisplayName;
        }
        else
        {
            characterIconImage.enabled = false;
        }

        playerNameText.text = state.IsLockedIn ? $"{state.ClientId + 1}. Oyuncu" : $"{state.ClientId + 1}. Oyuncu (Seçiyor...)";
        elements.SetActive(true);
    }

    public void DisableDisplay()
    {
        elements.SetActive(false);
    }
}
