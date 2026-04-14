public class DJonesStrategy : BaseStrategy,INPCStrategy
{
    public DJonesStrategy(AnimatorStateController animatorController, CombatChannel combatChannel) : base(animatorController, combatChannel)
    {
    }

    public override void ChooseStrategy()
    {
        PrepareAttack();
    }
    public void UseSkill()
    {

    }
    public void PrepareAttack()
    {
        _animatorStateController.PlayPreparing();
    }
}
