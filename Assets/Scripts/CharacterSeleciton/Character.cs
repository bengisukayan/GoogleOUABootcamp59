using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Characters/Character")]
public class Character : ScriptableObject
{
    [SerializeField] private int id = -1;
    [SerializeField] private string displayName = "Karakter Adý";
    [SerializeField] private Sprite icon;

    public int Id => id;
    public string DisplayName => displayName;
    public Sprite Icon => icon;
}
