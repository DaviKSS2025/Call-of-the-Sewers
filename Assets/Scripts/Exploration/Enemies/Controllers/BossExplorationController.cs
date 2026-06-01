using UnityEngine;

public class BossExplorationController : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] protected EnemiesExplorationData _enemyTriggerContext;
    [SerializeField] protected CutsceneChannel _cutsceneChannel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform == _player)
        {
            StartCombatTransition();
        }
    }
    private void StartCombatTransition()
    {
        MapDataController.Instance.EnemyCombatTriggered(_enemyTriggerContext.Id);
        _cutsceneChannel.RaiseHalfBlackoutRequested();
        enabled = false;
    }

    private void Start()
    {
        IsDead();
    }

    private bool IsDead()
    {
        if (MapDataController.Instance.RuntimeExplorationData.EnemyExplorationInfo.TryGetValue(_enemyTriggerContext.Id, out EnemiesExplorationData enemyData))
        {
            return enemyData.Dead;
        }
        else
        {
            UpdatePositionSavedOnTransitions();
            return false;
        }
    }
    private void UpdatePositionSavedOnTransitions()
    {
        _enemyTriggerContext.WorldPosX = transform.position.x;
        _enemyTriggerContext.WorldPosY = transform.position.y;
        MapDataController.Instance.UpdateEnemyPositions(_enemyTriggerContext);
    }
}
