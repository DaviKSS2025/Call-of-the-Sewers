using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ArmorDatabase", menuName = "Databases/ArmorDatabase")]
public class ArmorDatabase : ScriptableObject
{
    [Serializable]
    public struct ArmorEntry
    {
        public ArmorType type;
        public Armors armor;
    }

    [SerializeField] private ArmorEntry[] _armors;

    public Armors GetArmorScriptableObject(ArmorType type)
    {
        foreach (var a in _armors)
        {
            if (a.type == type)
                return a.armor;
        }
        return null;
    }
}
public enum ArmorType
{
    Shirt,
    Jacket
}