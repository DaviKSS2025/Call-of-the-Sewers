using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class CharacterData
{
    public string PlayerName;
    public Armors CurrentArmor;
    public Weapons CurrentWeapon;
    public int CurrentHealth = 100;
    public int CurrentMana = 100;
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

    public static SaveFile CreateNewGame(Weapons playerStartingWeapon,Armors playerStartingArmor)
    {
        return new SaveFile
        {
            PlayerData = CreateDefaultPlayer(playerStartingWeapon, playerStartingArmor),
            NPCData = new List<AllyNPC>(),
            Items = new List<ConsumableItemData>(),
            WorldPosition = Vector2.zero,
            CurrentMapName = SceneNames.Sewers,
            ChoosedNickName = false
        };
    }

    private static CharacterData CreateDefaultPlayer(Weapons playerStartingWeapon, Armors playerStartingArmor)
    {
        return new CharacterData
        {
            PlayerName = null,
            CurrentHealth = 100,
            CurrentMana = 100,
            CurrentWeapon = playerStartingWeapon,
            CurrentArmor = playerStartingArmor,
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
