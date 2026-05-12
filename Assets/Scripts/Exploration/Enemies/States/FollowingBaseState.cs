using UnityEngine;

public class FollowingBaseState : EnemyBaseState
{
    private Transform _currentTarget;
    private float _recalculateTimer = 0.2f;
    private float _timer;
    private float _maxTimeToStopTrackingPlayer = 5f;
    private float _currentTimeToStopTrackingPlayer;
    public FollowingBaseState(IBaseControllers controller, Transform target) : base(controller) 
    { 
        _currentTarget = target;
    }
    public override void OnEnter()
    {
        _controller.AnimatorController.PlayFollowing();
        _currentTimeToStopTrackingPlayer = _maxTimeToStopTrackingPlayer;
    }
    public override void OnUpdate()
    {
        TracePath();
        CheckPlayerVision();
    }
    private void TracePath()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            _controller.MovementController.Agent.SetDestination(_currentTarget.position);
            _timer = _recalculateTimer;
        }
        _controller.MovementController.MoveToTarget(_controller.MovementController.GetCardinalFromVector(_controller.MovementController.Agent.desiredVelocity));
    }
    private void CheckPlayerVision()
    {
        if (!_controller.DetectionController.CanSeePlayer())
        {
            if (_currentTimeToStopTrackingPlayer > 0)
            {
                _currentTimeToStopTrackingPlayer -= Time.deltaTime;
            }
            else
            {
                _controller.StateController.ChangeState(EnemyExplorationStates.Wandering);
            }
        }
        else
        {
            _currentTimeToStopTrackingPlayer = _maxTimeToStopTrackingPlayer;
        }
    }
}
