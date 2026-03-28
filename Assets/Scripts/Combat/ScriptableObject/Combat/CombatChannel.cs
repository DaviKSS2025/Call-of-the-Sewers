using System;
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
}
