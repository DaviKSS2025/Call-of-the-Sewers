public class DeadBaseState : EnemyBaseState
{
    public DeadBaseState(IBaseControllers controller) : base(controller) { }

    public override void OnEnter()
    {
        _controller.AnimatorController.PlayDeath();
        _controller.MovementController.CantMove();
        _controller.MovementController.AdjustQuartenion();
    }
}
