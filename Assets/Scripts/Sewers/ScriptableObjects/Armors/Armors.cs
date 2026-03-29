using UnityEngine;

[CreateAssetMenu(fileName = "Armors", menuName = "Player/Armors")]
public class Armors : ScriptableObject
{
    [SerializeField] private float _defenseMultiplier;
    [SerializeField] private string _name;
    [SerializeField] private Sprite _armorSprite;

    public float DefenseMultiplier
    {
        get => _defenseMultiplier;
    }
    public string Name
    {
        get => _name;
    }
    public Sprite ArmorSprite
    {
        get => _armorSprite;
    }
}
