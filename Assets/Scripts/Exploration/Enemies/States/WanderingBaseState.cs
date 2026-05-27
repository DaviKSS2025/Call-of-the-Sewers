using UnityEngine;
public class WanderingBaseState : EnemyBaseState
{
    private Transform[] _patrolTargets;
    private Transform _currentTarget;
    private float _distanceTolerance = 0.1f;
    private int _targetIndex;
    public WanderingBaseState(IBaseControllers controller, Transform[] patrolTargets) : base(controller) 
    {
        _patrolTargets = patrolTargets;
    }
    public override void OnEnter()
    {
        ChooseNewTarget();
    }
    public override void OnUpdate()
    {
        if (_controller.DetectionController.CanSeePlayer())
        {
            _controller.StateController.ChangeState(EnemyExplorationStates.Following);
        }
        else
        {
            if (IsCloseToTarget())
            {
                ChooseNewTarget();
            }
            else
            {
                _controller.MovementController.MoveToTarget(_controller.MovementController.GetCardinalFromVector(_controller.MovementController.Agent.desiredVelocity));
            }
        }
    }
    private void ChooseNewTarget()
    {
        _targetIndex = (_targetIndex + 1) % _patrolTargets.Length;
        _currentTarget = _patrolTargets[_targetIndex];

        _controller.MovementController.Agent.SetDestination(_currentTarget.transform.position);
    }
    private bool IsCloseToTarget()
    {
        return Vector2.Distance(_controller.MovementController.Transform.position, _currentTarget.position) < _distanceTolerance;
    }
}
