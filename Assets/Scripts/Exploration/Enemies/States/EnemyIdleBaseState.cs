using UnityEngine;

public class EnemyFreezeBaseState : EnemyBaseState
{
    public EnemyFreezeBaseState(IBaseControllers controller) : base(controller) { }

    public override void OnEnter()
    {
        _controller.MovementController.CantMove();
    }
}
