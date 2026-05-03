using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillDatabase", menuName = "Databases/SkillDatabase")]
public class SkillDatabase : ScriptableObject
{
    [Serializable]
    public struct SkillEntry
    {
        public SkillType type;
        public SkillData skillData;
    }

    [SerializeField] private SkillEntry[] skills;

    public SkillData GetSkill(SkillType type)
    {
        foreach (var s in skills)
        {
            if (s.type == type)
                return s.skillData;
        }
        return null;
    }
}
public enum SkillType
{
    Darkness,
    DarkFire,
    DarkHold
}