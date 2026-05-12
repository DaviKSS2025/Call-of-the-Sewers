using UnityEngine;


public interface IAttackHandler
{
    void ExecuteAttack(); 
}

public interface IAnimationHandler
{
    void PlayRun();
    void PlaySelected();
    void PlayUnselected();
    void PlayDeath();
    void PlayIdle();
    void PlayTakingDamage();
    void PlayAttackStart(int attackIndex);
    void PlayAttackEnd();
    void PlayPreparing();

    void PlayIdleTurn();
}

