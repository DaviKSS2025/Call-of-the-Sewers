using UnityEngine;

[CreateAssetMenu(fileName = "CharacterIndex", menuName = "Character Names/CharacterIndex")]
public class CharacterIndex : ScriptableObject
{
    [SerializeField] private int _index;

    public int Index
    {
        get => _index;
    }
}
