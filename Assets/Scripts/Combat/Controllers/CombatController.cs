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
    }
    public void OnDisable()
    {
        _combatChannel.UpdateLastAttackUsed -= UpdateLastAttackUsed;
        _turnChangeChannel.UpdateCurrentTurnUser -= UpdateLastEntityActed;
        _selectionChannel.NewTargetSelected -= UpdateLastTargetedEntity;
        _combatChannel.AttackRequested -= OnAttackRequested;
        _combatChannel.RandomAttackRequested -= OnRandomAttackRequested;
        _combatChannel.PlayerRunResult -= OnPlayerRunResult;
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
    private void OnAttackRequested()
    {
        _combatChannel.RaiseShowAttackText(_lastAttackUsed.AttackName, _lastEntityActed.EntityNameString, _lastTargetedEntity.EntityNameString);
        _lastTargetedEntity.Stats.OnSufferingAttack(_lastAttackUsed, _lastEntityActed.AttackController.AttackMultiplier, _lastEntityActed.AttackController.CriticalChanceMultiplier);
    }
    private void OnRandomAttackRequested(TargetType entityType)
    {
        BaseEntityController randomTarget = RollRandomTarget(entityType);
        _combatChannel.RaiseShowAttackText(_lastAttackUsed.AttackName, _lastEntityActed.EntityNameString, randomTarget.EntityNameString);
        _lastTargetedEntity.Stats.OnSufferingAttack(_lastAttackUsed, _lastEntityActed.AttackController.AttackMultiplier, _lastEntityActed.AttackController.CriticalChanceMultiplier);
    }
    private BaseEntityController RollRandomTarget(TargetType entityType)
    {
        TargetType targetType = entityType == TargetType.Enemy
            ? TargetType.Player
            : TargetType.Enemy;

        var targets = _entityList
        .Where(e => e.EntityType != entityType)
        .ToList();

        if (targets.Count == 0)
        {
            return null;
        }

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
}
