using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class CombatController
{
    private BaseEntityController _lastEntityActed;
    private BaseEntityController _lastTargetedEntity;
    private AttackData _lastAttackUsed;
    private CombatChannel _combatChannel;
    private SelectionChannel _selectionChannel;
    private TurnChangeChannel _turnChangeChannel;
    private List<BaseEntityController> _entityList;
    private SkillData _lastSkillUsed;

    public CombatController(CombatChannel combatChannel, SelectionChannel selection, TurnChangeChannel turnChangeChannel, List<BaseEntityController> entityList)
    {
        _combatChannel = combatChannel;
        _selectionChannel = selection;
        _turnChangeChannel = turnChangeChannel;
        _entityList = entityList;
    }

    public void Initialize()
    {
        _combatChannel.UpdateLastAttackUsed += UpdateLastAttackUsed;
        _turnChangeChannel.UpdateCurrentTurnUser += UpdateLastEntityActed;
        _selectionChannel.NewTargetSelected += UpdateLastTargetedEntity;
        _combatChannel.AttackRequested += OnAttackRequested;
        _combatChannel.RandomAttackRequested += OnRandomAttackRequested;
        _combatChannel.PlayerRunResult += OnPlayerRunResult;
        _turnChangeChannel.OnTurnOrderChanged += UpdateTurnOrder;
        _combatChannel.OnGlobalStatusEffectUsed += ApplyGlobalStatusEffect;
        _combatChannel.TargetAttackSkillRequested += OnTargetAttackSkillRequested;
        _combatChannel.OnSkillUsed += UpdateLastSkillUsed;
        _combatChannel.RandomTargetRequested += OnRandomTargetSkillRequested;
    }
    public void OnDisable()
    {
        _combatChannel.UpdateLastAttackUsed -= UpdateLastAttackUsed;
        _turnChangeChannel.UpdateCurrentTurnUser -= UpdateLastEntityActed;
        _selectionChannel.NewTargetSelected -= UpdateLastTargetedEntity;
        _combatChannel.AttackRequested -= OnAttackRequested;
        _combatChannel.RandomAttackRequested -= OnRandomAttackRequested;
        _combatChannel.PlayerRunResult -= OnPlayerRunResult;
        _turnChangeChannel.OnTurnOrderChanged -= UpdateTurnOrder;
        _combatChannel.OnGlobalStatusEffectUsed -= ApplyGlobalStatusEffect;
        _combatChannel.TargetAttackSkillRequested -= OnTargetAttackSkillRequested;
        _combatChannel.OnSkillUsed -= UpdateLastSkillUsed;
        _combatChannel.RandomTargetRequested -= OnRandomTargetSkillRequested;
    }
    private void UpdateLastEntityActed(BaseEntityController lastEntityActed)
    {
        _lastEntityActed = lastEntityActed;
    }
    private void UpdateLastTargetedEntity(BaseEntityController lastTargetedEntity)
    {
        _lastTargetedEntity = lastTargetedEntity;
    }
    private void UpdateLastAttackUsed(AttackData lastAttackUsed)
    {
        _lastAttackUsed = lastAttackUsed;
    }
    private void UpdateLastSkillUsed(SkillData skill)
    {
        _lastSkillUsed = skill;
    }
    private void OnAttackRequested()
    {
        _combatChannel.RaiseShowAttackText(_lastAttackUsed.AttackName, _lastEntityActed.EntityNameString, _lastTargetedEntity.EntityNameString);
        _lastTargetedEntity.Stats.OnSufferingAttack(_lastAttackUsed.Damage, _lastEntityActed.AttackController.AttackMultiplier, _lastEntityActed.AttackController.CriticalChanceMultiplier, _lastAttackUsed.StatusList, _lastAttackUsed.CriticalChance);
    }
    private void OnTargetAttackSkillRequested(int damage, List<StatusEffectEntry> statusList, int criticalChance)
    {
        _combatChannel.RaiseShowTargetSkillText(_lastSkillUsed.Name, _lastEntityActed.EntityNameString, _lastTargetedEntity.EntityNameString);
        _lastTargetedEntity.Stats.OnSufferingAttack(damage, _lastEntityActed.AttackController.AttackMultiplier, _lastEntityActed.AttackController.CriticalChanceMultiplier,statusList, criticalChance);
    }
    private void OnRandomAttackRequested(TargetType entityType)
    {
        BaseEntityController randomTarget = RollRandomTarget(entityType);
        _combatChannel.RaiseShowAttackText(_lastAttackUsed.AttackName, _lastEntityActed.EntityNameString, randomTarget.EntityNameString);
        _lastTargetedEntity.Stats.OnSufferingAttack(_lastAttackUsed.Damage, _lastEntityActed.AttackController.AttackMultiplier, _lastEntityActed.AttackController.CriticalChanceMultiplier, _lastAttackUsed.StatusList, _lastAttackUsed.CriticalChance);
    }
    private void OnRandomTargetSkillRequested(TargetType entityType, int damage, List<StatusEffectEntry> statusList, int criticalChance)
    {
        BaseEntityController randomTarget = RollRandomTarget(entityType);
        _combatChannel.RaiseShowTargetSkillText(_lastSkillUsed.Name, _lastEntityActed.EntityNameString, _lastTargetedEntity.EntityNameString);
        _lastTargetedEntity.Stats.OnSufferingAttack(damage, _lastEntityActed.AttackController.AttackMultiplier, _lastEntityActed.AttackController.CriticalChanceMultiplier, statusList, criticalChance);

    }
    private BaseEntityController RollRandomTarget(TargetType attackerType)
    {
        List<TargetType> validTargets = attackerType switch
        {
            TargetType.Player => new List<TargetType> { TargetType.Enemy },
            TargetType.NPC => new List<TargetType> { TargetType.Enemy },
            TargetType.Enemy => new List<TargetType> { TargetType.Player, TargetType.NPC },
            _ => null
        };

        if (validTargets == null)
            return null;

        var targets = _entityList
            .Where(e => validTargets.Contains(e.EntityType))
            .ToList();

        if (targets.Count == 0)
            return null;

        var randomTarget = targets[Random.Range(0, targets.Count)];

        UpdateLastTargetedEntity(randomTarget);

        return randomTarget;
    }
    private void OnPlayerRunResult(bool result, string playerName)
    {
        if (result)
        {
            _combatChannel.RaiseShowPlayerRunResult($"By a whisker, <color=red>{playerName}</color> manages to escape!");
        }
        else
        {
            foreach (BaseEntityController enemy in _entityList)
            {
                if (enemy.EntityType == TargetType.Enemy)
                {
                    _combatChannel.RaiseShowPlayerRunResult($"<color=red>{enemy.EntityNameString}</color> interrupts <color=red>{playerName}</color> during his escape!");
                    break;
                }
            }
        }
    }
    private void UpdateTurnOrder(List<BaseEntityController> turnOrder)
    {
        _entityList = turnOrder;
    }
    private void ApplyGlobalStatusEffect(TargetType targets, StatusEffectData statusEffect, int statusChance, int duration)
    {
        foreach (BaseEntityController entity in _entityList)
        {
            if (entity.EntityType == targets)
            {
                entity.StatusManager.ApplyEffect(statusEffect, statusChance, duration);
            }
        }
    }
}
