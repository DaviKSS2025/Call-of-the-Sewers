using UnityEngine;

public abstract class NPCController : BaseEntityController
{
    [SerializeField] private EntityName _entityName;
    protected NPCType _type;
    protected NPCStatusUI _statusUI;
    protected INPCStrategy _strategy;

    public NPCStatusUI StatusUI
    {
        set => _statusUI = value;
    }
    protected override void SetupAnimationController()
    {
        AssignAnimationController(new DJonesAnimatorController(_animator));
    }
    protected override void SetupStatsController()
    {
        AssignStatsController(new DJonesStats(this, _statusUI));
    }
    protected void AssignStrategy<T>(T strategy) where T : INPCStrategy
    {
        _strategy = strategy;
    }
    protected void SetupStrategy()
    {
        AssignStrategy(new DJonesStrategy(_animatorStateController, _combatChannel));
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
    public override void ExecuteTurnStart()
    {
        base.ExecuteTurnStart();
        _strategy.ChooseStrategy();
    }
    public override void OnAnimationEvent(string eventName)
    {
        /* if (eventName == "StartDamage")
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
         }*/
    }
}
