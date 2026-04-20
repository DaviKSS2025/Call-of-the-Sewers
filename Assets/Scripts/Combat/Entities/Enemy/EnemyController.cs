using UnityEngine;

public abstract class EnemyController : BaseEntityController 
{
    [SerializeField] protected EntityName _entityName;
    [SerializeField] protected IdleChance _idleChance;
    [SerializeField] protected InitialManaDamage _initialManaDamage;

    protected IEnemyStrategy _enemyStrategy;

    public IdleChance IdleChancePercentage
    {
        get => _idleChance;
    }
    public InitialManaDamage ManaDamageInitial
    {
        get => _initialManaDamage;
    }
    public override void Awake()
    {
        _name = _entityName.Name;
        base.Awake();
    }
    public override void Start()
    {
        base.Start();
        SetupStrategy();
    }
    protected abstract void SetupStrategy();
    protected void AssignStrategy<T>(T strategy) where T : IEnemyStrategy
    {
        _enemyStrategy = strategy;
    }
}
