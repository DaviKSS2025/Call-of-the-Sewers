public abstract class StatusEffectInstance
{
    public StatusEffectData Data { get; }
    protected BaseEntityController _target;
    protected int _remainingTurns;

    public int RemainingTurns
    {
        get => _remainingTurns;
    }
    public StatusEffectInstance(StatusEffectData data, BaseEntityController target, int duration)
    {
        Data = data;
        _target = target;
        _remainingTurns = duration;
    }

    public virtual void OnApply() { }
    public virtual void OnTurn() { }
    public virtual void OnEnd() { }

    public bool Tick()
    {
        OnTurn();
        return ReduceTurn();
    }
    public bool ReduceTurn()
    {
        _remainingTurns--;
        return _remainingTurns <= 0;
    }
}
