using UnityEngine;

[CreateAssetMenu(fileName = "Weapons", menuName = "Player/Weapons")]
public class Weapons : ScriptableObject
{
    [SerializeField] private float _damageMultiplier;
    [SerializeField] private string _name;
    [SerializeField] private WeaponType _weaponType;
    public float DamageMultiplier
    {
        get => _damageMultiplier;
    }
    public string Name
    {
        get => _name;
    }
    public WeaponType ThisWeaponType
    {
        get => _weaponType;
    }
}
