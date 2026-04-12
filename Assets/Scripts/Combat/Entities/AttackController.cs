using UnityEngine;

public class AttackController
{
    private BaseEntityController _entity;
    private float _attackMultiplier = 1f;
    private float _criticalChanceMultiplier = 1f;
    private AttackData _selectedAttack;
    public float AttackMultiplier
    {
        get => _attackMultiplier;
        set => _attackMultiplier = Mathf.Max(0, value);
    }
    public float CriticalChanceMultiplier
    {
        get => _criticalChanceMultiplier;
        set => _criticalChanceMultiplier = Mathf.Max(0, value);
    }
    public AttackController(BaseEntityController entity)
    {
        _entity = entity;
    }
    public void ChooseRandomAttack()
    {
        int roll = Random.Range(0, 101);
        int cumulative = 0;

        foreach (var attack in _entity.AttackList)
        {
            cumulative += attack.AttackChance;

            if (roll < cumulative)
            {
                _selectedAttack = attack;
                StartAttackAnimation(_selectedAttack);
                return;
            }
        }
    }
    public void StartAttackAnimation(AttackData currentAttack)
    {
        _selectedAttack = currentAttack;
        for (int i = 0; i < _entity.AttackList.Length; i++)
        {
            if (currentAttack == _entity.AttackList[i])
            {
                _entity.AnimHandler.PlayAttackStart(i);
                break;
            }
        }
    }
    public void LaunchAttack()
    {
        _entity.ComChannel.RaiseAttackRequested(_selectedAttack);
    }
    public void LaunchRandomAttack()
    {
        _entity.ComChannel.RaiseRandomAttackRequested(_selectedAttack, _entity.EntityType);
    }
}
