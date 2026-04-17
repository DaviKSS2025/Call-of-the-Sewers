using System;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDatabase", menuName = "Databases/WeaponDatabase")]
public class WeaponDatabase : ScriptableObject
{
    [Serializable]
    public struct WeaponEntry
    {
        public WeaponType type;
        public Weapons weapon;
    }

    [SerializeField] private WeaponEntry[] _weapons;

    public Weapons GetWeaponScriptableObject(WeaponType type)
    {
        foreach (var w in _weapons)
        {
            if (w.type == type)
                return w.weapon;
        }
        return null;
    }
}
public enum WeaponType
{
    Pistol,
    Shotgun,
    Dark_Blade
}