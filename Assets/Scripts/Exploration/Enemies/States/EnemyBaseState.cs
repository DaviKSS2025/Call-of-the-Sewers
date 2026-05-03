public abstract class EnemyBaseState : IEnemyState
{
    protected IBaseControllers _controller;

    public EnemyBaseState(IBaseControllers context)
    {
        _controller = context;
    }
    public virtual void OnEnter()
    {

    }
    public virtual void OnUpdate()
    {

    }
    public virtual void OnExit()
    {

    }

    public virtual void HandleAnimationEvent(string eventName)
    {
    }
}
