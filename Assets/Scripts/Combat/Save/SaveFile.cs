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
public class MapExplorationData
{
   public Dictionary<string, bool> OpenedDoors = new Dictionary<string, bool>();
   public Dictionary<string, bool> DeadEnemies = new Dictionary<string, bool>();
   public Dictionary<string, bool> CollectedItems = new Dictionary<string, bool>();
   public bool UsedSacrificePlace;
   public float WorldPosX = 0;
   public float WorldPosY = 0;   
   public SceneNames CurrentMapName = SceneNames.Sewers;
}

[Serializable]
public class SaveFile
{
    public CharacterData PlayerData;
    public List<AllyNPC> NPCData = new List<AllyNPC>();
    public List<NPCType> AlreadyRecruitedNPCs = new List<NPCType>();
    public bool ChoosedNickName;
    public List<ConsumableItemData> Items;
    public List<string> KeyIds;
    public MapExplorationData ExplorationData;
    public static SaveFile CreateNewGame()
    {
        return new SaveFile
        {
            PlayerData = CreateDefaultPlayer(),
            NPCData = new List<AllyNPC>(),
            Items = new List<ConsumableItemData>(),
            KeyIds = new List<string>(),
            ChoosedNickName = false,
            ExplorationData = ResetExploration()
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
                SkillType.Darkness,
                SkillType.DarkFire,
                SkillType.DarkHold
            }
        };
    }

    private static MapExplorationData ResetExploration()
    {
        return new MapExplorationData
        {
            OpenedDoors = new Dictionary<string, bool>(),
            DeadEnemies = new Dictionary<string, bool>(),
            CollectedItems = new Dictionary<string, bool>(),
            UsedSacrificePlace = false,
            WorldPosX = 0,
            WorldPosY = 0,
            CurrentMapName = SceneNames.Sewers
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
