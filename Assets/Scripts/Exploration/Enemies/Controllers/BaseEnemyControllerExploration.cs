using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class BaseEnemyControllerExploration : MonoBehaviour, IBaseControllers
{

    #region Base Variables

    protected Animator _animator;

    [Header("Base Stats")]
    [SerializeField] protected MovementSpeed _speed;
    protected GameObject _currentTarget;
    protected NavMeshAgent _agent;
    protected BoxCollider2D _boxCollider;

    protected BaseMovementController _movementController;
    protected BaseAnimatorController _animatorController;
    protected BaseEnemyStateMachineController _stateController;
    protected BaseDetectionController _detectionController;

    [SerializeField] protected Transform[] _patrolTargets;
    [SerializeField] protected Transform _player;
    [SerializeField] protected GameStateChannel _gameStateChannel;
    [SerializeField] protected CutsceneChannel _cutsceneChannel;
    [SerializeField] protected DetectionData _detectionData;
    [SerializeField] protected EnemiesExplorationData _enemyTriggerContext;

    protected float _timeToTriggerCombat = 3f;
    #endregion

    #region Properties to permit dependencies access
    public BaseAnimatorController AnimatorController => _animatorController;
    public BaseMovementController MovementController => _movementController;
    public BaseDetectionController DetectionController => _detectionController;
    public IStateChange StateController => _stateController;

    #endregion
    #region Monobehaviour life cicle methods
    private void Awake()
    {
        InitializeInspectorComponents();
    }
    private void Start()
    {
        if (!IsDead())
        {
            InitializeControllerDependencies();
            SubscribeEvents();
        }
        else
        {
            KillEnemy();
        }
    }
    void Update()
    {
        if (_timeToTriggerCombat > 0)
        {
            _timeToTriggerCombat -= Time.deltaTime;
        }
        _stateController.UseStateUpdate();
    }
    private void InitializeInspectorComponents()
    {
        _animator = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _agent = GetComponent<NavMeshAgent>();
    }
    #endregion

    //Instance specific controllers of the enemy, like movement, animator and stats.
    public virtual void InitializeControllerDependencies()
    {
        _animatorController = new BaseAnimatorController(_animator);
        _movementController = new BaseMovementController(_speed, transform, _agent);
        _detectionController = new BaseDetectionController(_player, _movementController, _detectionData);
        _stateController = new BaseEnemyStateMachineController(this, _patrolTargets, _player, _gameStateChannel);
        _stateController.Initialize();
    }
    private void SubscribeEvents()
    {
        _cutsceneChannel.OnCombatTransitionCutscene -= UpdatePositionSavedOnTransitions;
        _cutsceneChannel.OnCombatTransitionCutscene += UpdatePositionSavedOnTransitions;
    }
    private void OnDisable()
    {
        _cutsceneChannel.OnCombatTransitionCutscene -= UpdatePositionSavedOnTransitions;
    }
    private bool IsDead()
    {
        if (MapDataController.Instance.RuntimeExplorationData.EnemyExplorationInfo.TryGetValue(_enemyTriggerContext.Id, out EnemiesExplorationData enemyData))
        {
            _animatorController = new BaseAnimatorController(_animator);
            _movementController = new BaseMovementController(_speed, transform, _agent);
            _movementController.Teleport(new Vector2(enemyData.WorldPosX, enemyData.WorldPosY));
            return enemyData.Dead;
        }
        else
        {
            UpdatePositionSavedOnTransitions();
            return false;
        }
    }
    private void KillEnemy()
    {
        _stateController = new BaseEnemyStateMachineController(this, _patrolTargets, _player, _gameStateChannel);
        _stateController.ChangeState(EnemyExplorationStates.Dead);
        _boxCollider.enabled = false;
        _agent.enabled = false;
        enabled = false;
    }

    public void OnAnimationEvent(string eventName)
    {
        _stateController?.HandleAnimationEvent(eventName);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform == _player && _timeToTriggerCombat <= 0)
        {
            StartCombatTransition();
        }
    }
    private void StartCombatTransition()
    {
        MapDataController.Instance.EnemyCombatTriggered(_enemyTriggerContext.Id);
        _cutsceneChannel.RaiseHalfBlackoutRequested();
        _agent.enabled = false;
        enabled = false;
    }
    private void UpdatePositionSavedOnTransitions()
    {
        _enemyTriggerContext.WorldPosX = transform.position.x;
        _enemyTriggerContext.WorldPosY = transform.position.y;
        MapDataController.Instance.UpdateEnemyPositions(_enemyTriggerContext);
    }
}
public interface IBaseControllers
{
    public BaseAnimatorController AnimatorController { get; }
    public BaseMovementController MovementController { get; }
    public BaseDetectionController DetectionController { get; }
    public IStateChange StateController { get; }
}