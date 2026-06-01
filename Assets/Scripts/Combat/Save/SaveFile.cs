using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class CharacterData
{
    public string PlayerName;
    public ArmorType CurrentArmor = ArmorType.Shirt;
    public WeaponType CurrentWeapon = WeaponType.Knife;
    public int CurrentHealth = 100;
    public int CurrentMana = 100;
    public List<SkillType> SkillList;
    public TorchData TorchData = new TorchData(0, 0);
}

[Serializable]
public class TorchData 
{
    private float _duration = 0;
    private float _intensity = 0;
    public TorchData(float duration, float intensity)
    {
        _duration = duration;
        _intensity = intensity;
    }

    public float RemainingDuration => _duration;
    public float Intensity => _intensity;
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
   public Dictionary<string, EnemiesExplorationData> EnemyExplorationInfo = new Dictionary<string, EnemiesExplorationData>();
   public Dictionary<string, bool> CollectedItems = new Dictionary<string, bool>();
   public bool UsedSacrificePlace;
   public float WorldPosX = -10.782f;
   public float WorldPosY = -54.74f;   
}
[Serializable]
public struct EnemiesExplorationData
{
    [SerializeField] private string _id;
    public EnemyType _enemyType;
    private bool _dead;
    private float _worldPosX;
    private float _worldPosY;
    public string Id
    {
        get => _id;
    }
    public bool Dead
    {
        get => _dead;
        set => _dead = value;
    }
    public float WorldPosX
    {
        get => _worldPosX;
        set => _worldPosX = value;
    }
    public float WorldPosY
    {
        get => _worldPosY;
        set => _worldPosY = value;
    }
}

[Serializable]
public class SaveFile
{
    public CharacterData PlayerData;
    public List<AllyNPC> NPCData = new List<AllyNPC>();
    public List<NPCType> AlreadyRecruitedNPCs = new List<NPCType>();
    public bool ChoosedNickName;
    public List<ItemType> Items;
    public List<string> KeyIds;
    public MapExplorationData ExplorationData;
    public static SaveFile CreateNewGame()
    {
        return new SaveFile
        {
            PlayerData = CreateDefaultPlayer(),
            NPCData = new List<AllyNPC>(),
            Items = new List<ItemType>()
            {
                ItemType.Medicine,
                ItemType.Medicine,
                ItemType.Medicine,
                ItemType.Neurostimulants,
                ItemType.Neurostimulants,
                ItemType.Neurostimulants,
                ItemType.Torch,
                ItemType.Torch,
                ItemType.Torch
            },
            KeyIds = new List<string>(),
            ChoosedNickName = false,
            ExplorationData = ResetExploration(),
        };
    }

    private static CharacterData CreateDefaultPlayer()
    {
        return new CharacterData
        {
            PlayerName = null,
            CurrentHealth = 100,
            CurrentMana = 100,
            CurrentWeapon = WeaponType.Knife,
            CurrentArmor = ArmorType.Shirt,
            SkillList = new List<SkillType>()
            {
                SkillType.Darkness,
                SkillType.DarkFire,
                SkillType.DarkHold
            },
            TorchData = new TorchData(0, 0)
        };
    }

    private static MapExplorationData ResetExploration()
    {
        return new MapExplorationData
        {
            OpenedDoors = new Dictionary<string, bool>(),
            EnemyExplorationInfo = new Dictionary<string, EnemiesExplorationData>(),
            CollectedItems = new Dictionary<string, bool>(),
            UsedSacrificePlace = false,
            WorldPosX = -10.782f,
            WorldPosY = -54.74f,
        };
    }
}
public enum SceneNames
{
    MainMenu,
    Sewers,
    Combat,
    ChangeName,
    Endgame
}
