using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class CharacterData
{
    public string PlayerName;
    public ArmorType CurrentArmor = ArmorType.Shirt;
    public WeaponType CurrentWeapon = WeaponType.Pistol;
    public int CurrentHealth = 100;
    public int CurrentMana = 100;
    public List<SkillType> SkillList;
}
[Serializable]
public class AllyNPC
{
    public int CurrentHealth = 100;
    public NPCType NPCInfo;
}

[Serializable]
public class SaveFile
{
    public CharacterData PlayerData;
    public List<AllyNPC> NPCData = new List<AllyNPC>();
    public Vector2 WorldPosition = Vector2.zero;
    public SceneNames CurrentMapName = SceneNames.Sewers;
    public bool ChoosedNickName;
    public List<ConsumableItemData> Items;

    public static SaveFile CreateNewGame()
    {
        return new SaveFile
        {
            PlayerData = CreateDefaultPlayer(),
            NPCData = new List<AllyNPC>(),
            Items = new List<ConsumableItemData>(),
            WorldPosition = Vector2.zero,
            CurrentMapName = SceneNames.Sewers,
            ChoosedNickName = false,
        };
    }

    private static CharacterData CreateDefaultPlayer()
    {
        return new CharacterData
        {
            PlayerName = null,
            CurrentHealth = 100,
            CurrentMana = 100,
            CurrentWeapon = WeaponType.Pistol,
            CurrentArmor = ArmorType.Shirt,
            SkillList = new List<SkillType>()
            {
                SkillType.Darkness
            }
        };
    }
}
public enum SceneNames
{
    MainMenu,
    Sewers,
    Dungeons,
    Combat,
    ChangeName
}
