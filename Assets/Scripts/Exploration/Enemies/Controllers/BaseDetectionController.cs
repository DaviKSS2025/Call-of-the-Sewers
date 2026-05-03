using System;
using UnityEngine;

public class BaseDetectionController
{
    private Transform _target;
    private BaseMovementController _movementController;
    private DetectionData _detectionData;
    public BaseDetectionController(Transform target, BaseMovementController movementController, DetectionData detectionData)
    {
        _target = target;
        _movementController = movementController;
        _detectionData = detectionData;
    }

    public bool CanSeePlayer()
    {
        Vector2 toTarget = _target.position - _movementController.Transform.position;

        if (IsTargetInsideRange(toTarget))
        {
            if (IsTargetInsideAngle(toTarget))
            {
                return LaunchRaycast(toTarget);
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    private bool IsTargetInsideRange(Vector2 toTarget)
    {
        return toTarget.magnitude < _detectionData.ViewDistance;
    }
    private bool IsTargetInsideAngle(Vector2 toTarget)
    {
        if (_movementController.LastDirection == Vector2.zero)
        {
            return false;
        }
        return Vector2.Angle(_movementController.LastDirection, toTarget) < _detectionData.ViewAngle;
    }
    private bool LaunchRaycast(Vector2 toTarget)
    {
        RaycastHit2D hit = Physics2D.Raycast(_movementController.Transform.position, toTarget.normalized, _detectionData.ViewDistance, _detectionData.ObstacleLayer);

        return hit.collider != null && hit.transform == _target;
    }
}

[Serializable]
public struct DetectionData
{
    [SerializeField] private float _viewDistance;
    [SerializeField] private float _viewAngle;
    [SerializeField] private LayerMask _obstacleLayer;

    public float ViewDistance => _viewDistance;
    public float ViewAngle => _viewAngle;
    public LayerMask ObstacleLayer => _obstacleLayer;
}
