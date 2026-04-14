using UnityEngine;

public class GuardianStrategy : BaseStrategy, IEnemyStrategy
{
    private int _idleTurnChance;
    private string _name;
    public GuardianStrategy(AnimatorStateController enemyAnimatorController, int idleTurnChance, CombatChannel combatChannel, string name) : base(enemyAnimatorController, combatChannel)
    { 
        _animatorStateController = enemyAnimatorController;
        _idleTurnChance = idleTurnChance;
        _combatChannel = combatChannel;
        _name = name;
    }
    public override void ChooseStrategy()
    {
        if (RollIdleTurnChance(_idleTurnChance))
        {
            ExecuteIdleTurn();
        }
        else
        {
            PrepareAttack();
        }
    }
    public bool RollIdleTurnChance(int idleTurnChance)
    {
        return Random.Range(0, 101) <= idleTurnChance;
    }
    public void PrepareAttack()
    {
        _animatorStateController.PlayPreparing();
    }
    public void ExecuteIdleTurn()
    {
        _animatorStateController.PlayIdleTurn();
        _combatChannel.RaiseIdleTurn(_name);
    }
}
