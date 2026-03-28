using UnityEngine;

public interface IEnemyStrategy
{
    void ChooseStrategy();
    bool RollIdleTurnChance(int idleTurnChance);
    void PrepareAttack();
    void ExecuteIdleTurn();
}
