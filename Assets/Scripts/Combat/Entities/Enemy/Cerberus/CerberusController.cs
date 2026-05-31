using UnityEngine;

public class CerberusController : EnemyController
{
    protected override void SetupAnimationController()
    {
        AssignAnimationController(new CerberusAnimatorStateController(_animator));
    }
    protected override void SetupStatsController()
    {
        AssignStatsController(new CerberusStats(this));
    }
    protected override void SetupStrategy()
    {
        AssignStrategy(new CerberusStrategy(_animatorStateController, _idleChance.IdleChancePercentage, _combatChannel, EntityNameString));
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
