using System;
using UnityEngine;

[Serializable]
public class CharacterData
{
    public EntityName PlayerName;
    public int CurrentHealth = 100;
    public int CurrentMana = 100;
}
[Serializable]
public class AllyNPC
{
    public int CurrentHealth = 100;
    public int CurrentMana = 100;
    public EntityName NPCname;
}

[Serializable]
public class SaveFile
{
    public CharacterData PlayerData;
    public AllyNPC NPCData;
    public Vector2 WorldPosition;
    public CurrentMapName CurrentMapName = CurrentMapName.Sewers;
}
public enum CurrentMapName
{
    Sewers,
    Dungeons
}
