using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CombatChannel", menuName = "Channels/CombatChannel")]
public class CombatChannel : ScriptableObject
{
    public Action <AttackData> UpdateLastAttackUsed;
    public Action AttackRequested;
    public Action<TargetType> RandomAttackRequested;
    public Action<string, string, string> ShowAttackText;
    public Action<string> MonsterEncounter;
    public Action<float, int, BaseEntityController> ShowDamageText;
    public Action<string> PlayerRunStarted;
    public Action<bool, string> PlayerRunResult;
    public Action<string> ShowPlayerRunResult;
    public Action<string> IdleTurn;
    public Action<string> OnEntityKilled;
    public Action<string> ShowSkipTurnOnStun;
    public Action<SkillData> OnSkillUsed;
    public Action<TargetType, StatusEffectData, int, int> OnGlobalStatusEffectUsed;
    public Action<string> ShowGlobalStatusEffectUsed;
    public Action SkillEnd;
    public Action ChoosingSkill;
    public Action CancelChoosingSkill;
    public Action<string, string, string> ShowSkillText;
    public Action<int, List<StatusEffectEntry>, int> TargetAttackSkillRequested;
    public Action HideSkillUI;
    public Action<TargetType, int, List<StatusEffectEntry>, int> RandomTargetRequested;
    public void RaiseAttackRequested(AttackData attack)
    {
        UpdateLastAttackUsed?.Invoke(attack);
        AttackRequested?.Invoke();
    }
    public void RaiseRandomAttackRequested(AttackData attack, TargetType entity)
    {
        UpdateLastAttackUsed?.Invoke(attack);
        RandomAttackRequested?.Invoke(entity);
    }
    public void RaiseShowAttackText(string attackName, string attackerName, string targetName)
    {
        ShowAttackText?.Invoke(attackName, attackerName, targetName);
    }
    public void RaiseMonsterAppearing(string monsterName)
    {
        MonsterEncounter?.Invoke(monsterName);
    }
    public void RaiseShowDamageText(float damagePercentual, int damageApplied, BaseEntityController target)
    {
        ShowDamageText?.Invoke(damagePercentual, damageApplied, target);
    }
    public void RaisePlayerRunStarted(string playerName)
    {
        PlayerRunStarted?.Invoke(playerName);
    }
    public void RaisePlayerRunResult(bool result, string playerName)
    {
        PlayerRunResult?.Invoke(result, playerName);
    }
    public void RaiseShowPlayerRunResult(string runText)
    {
        ShowPlayerRunResult?.Invoke(runText);
    }
    public void RaiseIdleTurn(string enemyName)
    {
        IdleTurn?.Invoke(enemyName);
    }
    public void RaiseEntityKilled(string deadName)
    {
        OnEntityKilled?.Invoke(deadName);
    }
    public void RaiseShowSkipTurnOnStun(string entityName)
    {
        ShowSkipTurnOnStun?.Invoke(entityName);
    }
    public void RaiseSkillUsed(SkillData skill)
    {
        OnSkillUsed?.Invoke(skill);
        HideSkillUI?.Invoke();
    }
    public void RaiseGlobalStatusEffectUsed(TargetType targets, StatusEffectData statusEffect, int statusChance, string globalStatusMessage, int duration)
    {
        OnGlobalStatusEffectUsed?.Invoke(targets, statusEffect, statusChance, duration);
        ShowGlobalStatusEffectUsed?.Invoke(globalStatusMessage);
    }
    public void RaiseSkillEnd()
    {
        SkillEnd?.Invoke();
    }
    public void RaiseChoosingSkill()
    {
        ChoosingSkill?.Invoke();
    }
    public void RaiseCancelChoosingSkill()
    {
        CancelChoosingSkill?.Invoke();
    }
    public void RaiseTargetAttackSkillRequested(int damage, List<StatusEffectEntry> statusList, int criticalChance)
    {
        TargetAttackSkillRequested?.Invoke(damage, statusList, criticalChance);
    }
    public void RaiseShowTargetSkillText(string skillName, string attackerName, string targetName)
    {
        ShowSkillText?.Invoke(skillName, attackerName, targetName);
    }
    public void RaiseRandomTargetAttackSkillRequested(TargetType entityType, int damage, List<StatusEffectEntry> statusList, int criticalChance)
    {
        RandomTargetRequested?.Invoke(entityType, damage, statusList, criticalChance);
    }
}
