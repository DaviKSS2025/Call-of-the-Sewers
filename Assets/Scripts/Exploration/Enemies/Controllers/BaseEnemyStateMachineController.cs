using UnityEngine;
public class BaseEnemyStateMachineController : IStateChange
{
    protected IEnemyState _currentState;
    protected readonly IBaseControllers _controllers;
    protected readonly Transform[] _patrolTargets;
    protected readonly Transform _player;
    private readonly GameStateChannel _gameStateChannel;
    private CurrentGameState _gameState;
    public BaseEnemyStateMachineController(IBaseControllers controllers, Transform[] patrolTargets, Transform player, GameStateChannel gameStateChannel)
    {
        _controllers = controllers;
        _patrolTargets = patrolTargets;
        _player = player;
        _gameStateChannel = gameStateChannel;
    }

    public void Initialize()
    {
        ChangeState(EnemyExplorationStates.Wandering);

        _gameStateChannel.OnGameStateChange -= OnGameStateChange;
        _gameStateChannel.OnGameStateChange += OnGameStateChange;
    }

    private void OnGameStateChange(CurrentGameState gameState)
    {
        _gameState = gameState;
        if (_gameState != CurrentGameState.Gameplay)
        {
            _controllers.MovementController.CantMove();
        }
    }

    public void ChangeState(EnemyExplorationStates nextState)
    {
        _currentState?.OnExit();

        _currentState = CreateState(nextState);

        _currentState.OnEnter();
    }
    protected virtual IEnemyState CreateState(EnemyExplorationStates state)
    {
        switch (state)
        {
            case EnemyExplorationStates.Dead:
                return new DeadBaseState(_controllers);
            case EnemyExplorationStates.Following:
                return new FollowingBaseState(_controllers, _player);
            default:
                return new WanderingBaseState(_controllers, _patrolTargets);
        }
    }

    public void UseStateUpdate()
    {
        if (_gameState == CurrentGameState.Gameplay)
        {
            _currentState?.OnUpdate();
        }
        else
        {
            _controllers.MovementController.AdjustQuartenion();
        }
    }

    public void HandleAnimationEvent(string eventName)
    {
        _currentState?.HandleAnimationEvent(eventName);
    }
}
public interface IStateChange
{
    void ChangeState(EnemyExplorationStates nextState);
}
public enum EnemyExplorationStates
{
    Wandering,
    Dead,
    Following
}