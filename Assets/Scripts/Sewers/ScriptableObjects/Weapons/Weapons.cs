using UnityEngine;

[CreateAssetMenu(fileName = "Weapons", menuName = "Player/Weapons")]
public class Weapons : ScriptableObject
{
    [SerializeField] private float _damageMultiplier;
    [SerializeField] private string _name;
    [SerializeField] private Sprite _weaponSprite;

    public float DamageMultiplier
    {
        get => _damageMultiplier;
    }
    public string Name
    {
        get => _name;
    }
    public Sprite WeaponSprite
    {
        get => _weaponSprite;
    }
}
