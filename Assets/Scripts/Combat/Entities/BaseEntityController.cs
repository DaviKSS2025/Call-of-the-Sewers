using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Animator))]
public abstract class BaseEntityController : MonoBehaviour
{
    protected string _name;

    [Header("Channels")]
    [SerializeField] protected TurnChangeChannel _turnChannel;
    [SerializeField] protected CombatChannel _combatChannel;
    [SerializeField] protected SFXEventChannel _SFXChannel;
    [SerializeField] protected SelectionChannel _selectionChannel;
    [SerializeField] protected InputChannel _inputChannel;
    protected Animator _animator;
    protected AnimatorStateController _animatorStateController;
    protected AttackController _attackController;
    public IAttackHandler AttackHandler 
    { 
        get; 
        protected set; 
    }
    public IAnimationHandler AnimHandler 
    { 
        get; 
        protected set; 
    }

    protected StatsController _stats;
    protected SelectableEntity _selectableEntity;
    protected StatusEffectManager _statusEffectManager;
    [SerializeField] protected TargetType _targetType;
    [SerializeField] protected AttackData[] _attackList;

    [Header("Status effect settings")]
    [SerializeField] protected Image[] _statusEffectSlots;
    [SerializeField] protected TextMeshProUGUI[] _statusEffectDurationTMPro;

    [Header("Defense Settings")]
    [SerializeField] protected SurvivalStats _survivalStats;
    
    public Action UnscribeEventsOnDisable;
    #region Properties

    public string EntityNameString
    {
        get => _name;
    }
    public AttackController AttackController
    {
        get => _attackController;
    }
    public StatsController Stats
    {
        get => _stats;
    }
    public TargetType EntityType
    {
        get => _targetType;
    }
    public AnimatorStateController AnimatorStateController
    {
        get => _animatorStateController;
    }
    public SelectionChannel SelectionChannel
    {
        get => _selectionChannel;
    }
    public AttackData[] AttackList
    {
        get => _attackList;
    }
    public CombatChannel ComChannel
    {
        get => _combatChannel;
    }
    public SurvivalStats SurvStats
    {
        get => _survivalStats;
    }
    public TurnChangeChannel ThisTurnChangeChannel
    {
        get => _turnChannel;
    }
    public StatusEffectManager StatusManager
    {
        get => _statusEffectManager;
    }
    public Image[] StatusEffectSlotImages
    {
        get => _statusEffectSlots;
    }
    public TextMeshProUGUI[] StatusEffectDurationTMPro
    {
        get => _statusEffectDurationTMPro;
    }
    public InputChannel ThisInputChannel
    {
        get => _inputChannel;
    }
    #endregion

    #region Monobehaviour
    public virtual void Awake()
    {
        _animator = GetComponent<Animator>();
        SetupAnimationController();
        _attackController = new AttackController(this);
    }
    public virtual void OnEnable()
    {
        _turnChannel.UpdateCurrentTurnUser += OnTurnStart;
    }
    public virtual void OnDisable()
    {
        UnscribeEventsOnDisable?.Invoke();
    }
    public virtual void Start() 
    {
        _selectableEntity = new SelectableEntity(this);
        _selectableEntity.Subscribe();
        _statusEffectManager = new StatusEffectManager(this);
        SetupStatsController();
    }
    #endregion

    #region GenericComponents;
    protected abstract void SetupAnimationController();
    protected abstract void SetupStatsController();

    protected void AssignAnimationController<T>(T controller) where T : AnimatorStateController, IAnimationHandler
    {
        _animatorStateController = controller;
        AnimHandler = controller;
    }
    protected void AssignStatsController<T>(T statsController) where T : StatsController
    {
        _stats = statsController;
    }
    #endregion
    #region TurnMethods
    public virtual void OnTurnStart(BaseEntityController entity)
    {
        if (entity == this)
        {
            
            if (!_statusEffectManager.ExecuteStatusEffectsAndSkipTurnIfStunned())
            {
                ExecuteTurnStart();
            }
            else
            {
                ComChannel.RaiseShowSkipTurnOnStun(_name);
                NeutralTurnEnd();
            }
        }
    }
    public virtual void OnTurnEnd()
    {
        _turnChannel.RaiseEndCurrentTurn(this);
    }
    public virtual void NeutralTurnEnd()
    {
        _animatorStateController.PlayIdle();
        OnTurnEnd();
    }
    public virtual void ExecuteTurnStart()
    {
    }

    #endregion
    public virtual void OnAnimationEvent(string eventName)
    {
    }
    public virtual void DeathClears()
    {
        foreach (Image statusImage in _statusEffectSlots)
        {
            statusImage.enabled = false;
        }
        foreach (TextMeshProUGUI statusText in _statusEffectDurationTMPro)
        {
            statusText.enabled = false;
        }
    }
}
