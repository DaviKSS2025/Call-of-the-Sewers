using System.Collections;
using UnityEngine;

public abstract class NPCController : BaseEntityController
{
    [SerializeField] private EntityName _entityName;
    [SerializeField] protected NPCType _type;
    protected NPCStatusUI _statusUI;
    protected INPCStrategy _strategy;
    protected SkillManager _skillManager;
    protected IEntityListHandler _entityListHandler;
    public NPCStatusUI StatusUI
    {
        set => _statusUI = value;
    }
    public NPCType NPCType
    {
        get => _type;
        set => _type = value;
    }
    public IEntityListHandler EntityListHandler
    {
        set => _entityListHandler = value;
    }
    protected override void SetupAnimationController()
    {
        AssignAnimationController(new DJonesAnimatorController(_animator));
    }
    protected override void SetupStatsController()
    {
        foreach (AllyNPC npc in NPCDataController.Instance.RuntimeData)
        {
            if (npc.NPCInfo == _type)
            {
                AssignStatsController(new NPCStatsController(this, _statusUI, npc.CurrentHealth));
                break;
            }
        }
    }
    protected void AssignStrategy<T>(T strategy) where T : INPCStrategy
    {
        _strategy = strategy;
    }
    protected abstract void SetupStrategy();
    protected void AssignSkillManager<T>(T skillManager) where T : SkillManager
    {
        _skillManager = skillManager;
    }
    protected abstract void SetupSkillManager();

    public override void Awake()
    {
        _name = _entityName.Name;
        base.Awake();
    }
    public override void Start()
    {
        _selectableEntity = new SelectableEntity(this);
        _selectableEntity.Subscribe();
        _statusEffectManager = new StatusEffectManager(this);
        StartCoroutine(NPCStart());
    }

    private IEnumerator NPCStart()
    {
        yield return new WaitUntil(() => _statusUI != null);
        SetupStatsController();
        SetupStrategy();
        SetupSkillManager();
    }

    public override void ExecuteTurnStart()
    {
        _skillManager.PrepareToListenEvents();
        StartCoroutine(NPCThinkTime());
    }
    public override void NeutralTurnEnd()
    {
        _skillManager.OnDisable();
        base.NeutralTurnEnd();
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
        else if (eventName == "SkillEnd")
        {
            _skillManager.OnDisable();
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
    }
    private IEnumerator NPCThinkTime()
    {
        yield return new WaitForSeconds(1f);
        _strategy.ChooseStrategy();
    }
}
