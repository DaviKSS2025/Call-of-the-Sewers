using UnityEngine;

public class PlayerDeadState : PlayerBaseState
{
    public PlayerDeadState(PlayerControllerExploration context) : base(context) { }

    public override void OnEnter()
    {
        // Player death animation
        player.PlayerAnimatorControllerExplo.PlayDeath();
    }

    public override void OnUpdate()
    {
        // Player can't move.
    }

    public override void OnExit()
    {
        // Final state.
    }

    public override void HandleAnimationEvent(string eventName) { }
}
