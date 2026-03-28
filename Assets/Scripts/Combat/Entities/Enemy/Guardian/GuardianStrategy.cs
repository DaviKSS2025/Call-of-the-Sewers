using UnityEngine;

public class GuardianStrategy : IEnemyStrategy
{
    private AnimatorStateController _animatorStateController;
    private int _idleTurnChance;
    private CombatChannel _combatChannel;
    private string _name;
    public GuardianStrategy(AnimatorStateController enemyAnimatorController, int idleTurnChance, CombatChannel combatChannel, string name) 
    { 
        _animatorStateController = enemyAnimatorController;
        _idleTurnChance = idleTurnChance;
        _combatChannel = combatChannel;
        _name = name;
    }
    public void ChooseStrategy()
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
        _combatChannel.RaiseIdleTurn(_name);
    }
}
