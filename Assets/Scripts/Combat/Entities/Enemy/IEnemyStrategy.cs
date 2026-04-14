public interface IEnemyStrategy
{
    void ChooseStrategy();
    bool RollIdleTurnChance(int idleTurnChance);
    void PrepareAttack();
    void ExecuteIdleTurn();
}
public interface INPCStrategy
{
    void ChooseStrategy();
    void UseSkill();
    void PrepareAttack();
}
