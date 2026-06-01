using UnityEngine;
using UnityEngine.AI;

public class BaseMovementController
{
    private MovementSpeed _movementSpeed;
    private readonly Transform _transform;
    private Vector2 _lastDirection = Vector2.down;
    private NavMeshAgent _agent;
    private BaseAnimatorController _animator;
    public Transform Transform
    {
        get => _transform;
    }
    public NavMeshAgent Agent
    {
        get => _agent;
        set => _agent = value;
    }
    public Vector2 LastDirection
    {
        get => _lastDirection;
    }
    public BaseMovementController(MovementSpeed movementSpeed, Transform transform, NavMeshAgent agent, BaseAnimatorController animator)
    {
        _movementSpeed = movementSpeed;
        _transform = transform;
        _agent = agent;
        _animator = animator;
    }

    public void MoveToTarget(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            _lastDirection = direction;
        }
        AdjustQuartenion();
        FlipToTarget();
        _agent.nextPosition = _transform.position;
        _agent.speed = _movementSpeed.WalkingSpeed;
    }
    public void FlipToTarget()
    {
        int direction = GetDirectionIndex(_lastDirection);
        _animator.PlayWandering(direction);
    }
    private int GetDirectionIndex(Vector2 direction)
    {
        if (Mathf.Abs(direction.y) > Mathf.Abs(direction.x))
        {
            if (direction.y > 0)
            {
                return 0; // Back
            }
            else
            {
                return 1; // Front
            }
        }
        else
        {
            if (direction.x < 0)
            {
                return 2; // Left
            }
            else
            {
                return 3; // Right
            }
        }
    }
    public void CantMove()
    {
        if (_agent != null)
        {
            _agent.speed = 0;
        }
    }
    public Vector2 GetCardinalFromVector(Vector3 velocity)
    {
        if (velocity.magnitude < 0.1f) return Vector2.zero;

        if (Mathf.Abs(velocity.x) > Mathf.Abs(velocity.y))
        {
            return new Vector2(Mathf.Sign(velocity.x), 0);
        }
        else
        {
            return new Vector2(0, Mathf.Sign(velocity.y));
        }
    }
    public void AdjustQuartenion()
    {
        _transform.rotation = Quaternion.identity;
    }

    public void Teleport(Vector2 position)
    {
        _transform.position = position;
    }
}
