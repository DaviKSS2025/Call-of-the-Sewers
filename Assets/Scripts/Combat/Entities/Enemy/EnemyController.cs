using UnityEngine;

public abstract class EnemyController : BaseEntityController 
{
    [SerializeField] protected IdleChance _idleChance;
    protected IEnemyStrategy _enemyStrategy;

    public IdleChance IdleChancePercentage
    {
        get => _idleChance;
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
