using UnityEngine;

public class PlayerFreezeState : PlayerBaseState
{
    public PlayerFreezeState(PlayerControllerExploration context) : base(context) { }

    public override void OnEnter()
    {
        player.PlayerMovementScript.CantMove();
    }

    public override void OnUpdate()
    {
    }

    public override void OnExit()
    {
    }

}
