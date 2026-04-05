using UnityEngine;

public class GuardianController : EnemyController
{
    protected override void SetupAnimationController()
    {
        AssignAnimationController(new GuardianAnimatorStateController(_animator));
    }
    protected override void SetupStatsController()
    {
        AssignStatsController(new GuardianStats(this));
    }
    protected override void SetupStrategy()
    {
        AssignStrategy(new GuardianStrategy(_animatorStateController, _idleChance.IdleChancePercentage, _combatChannel, EntityNameString));
    }
    public override void ExecuteTurnStart()
    {
        base.ExecuteTurnStart();
        _enemyStrategy.ChooseStrategy();
    }
    public override void OnAnimationEvent(string eventName)
    {
        if (eventName == "StartDamage")
        {
            _attackController.LaunchRandomAttack();
        }
        else if (eventName == "AttackEnd")
        {
            NeutralTurnEnd();
        }
        else if (eventName == "PrepareEnd")
        {
            _attackController.ChooseRandomAttack();
        }
        else if (eventName == "DeathEnd")
        {
            _animatorStateController.PlayDeath();
        }
        else if (eventName == "IdleTurnEnd")
        {
            NeutralTurnEnd();
        }
    }
}
