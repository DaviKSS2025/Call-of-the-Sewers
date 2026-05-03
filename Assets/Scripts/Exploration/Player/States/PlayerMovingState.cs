using UnityEngine;

public class PlayerMovingState : PlayerBaseState
{
    // Construtor
    public PlayerMovingState(PlayerControllerExploration context) : base(context) { }
    private bool _wasFacingLeft;
    public override void OnEnter()
    {
    }

    public override void OnUpdate()
    {
        //(Walk/Idle animations based on Input)
        ManageMovement();
    }

    public override void OnExit()
    {
    }

    public override void HandleAnimationEvent(string eventName)
    {
    }
    private void ManageMovement()
    {
        Vector2 input = player.CurrentMoveInput;

        if (input.sqrMagnitude < 0.01f)
        {
            player.PlayerMovementScript.StartIdle();
            player.PlayerAnimatorControllerExplo.PlayIdle();
            return;
        }

        player.PlayerMovementScript.ApplyMovement(input);
        player.PlayerAnimatorControllerExplo.PlayWalk(input);
    }
}
