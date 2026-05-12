public abstract class BaseStrategy
{
    protected AnimatorStateController _animatorStateController;
    protected CombatChannel _combatChannel;

    public BaseStrategy(AnimatorStateController animatorController, CombatChannel combatChannel)
    {
        _animatorStateController = animatorController;
        _combatChannel = combatChannel;
    }
    public abstract void ChooseStrategy();

    public virtual void PrepareAttack()
    {
        _animatorStateController.PlayPreparing();
    }
}
