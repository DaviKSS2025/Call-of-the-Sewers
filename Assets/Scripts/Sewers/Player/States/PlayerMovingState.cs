using UnityEngine;

public class PlayerMovingState : PlayerBaseState
{
    // Construtor
    public PlayerMovingState(PlayerControllerExploration context) : base(context) { }

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
        Vector2 input = new Vector2(player.PlayerInputReader.Horizontal, player.PlayerInputReader.Vertical);

        if (input == Vector2.zero)
        {
            player.PlayerAnimatorControllerExplo.PlayIdle();
            player.PlayerMovementScript.StartIdle(player.PlayerInputReader.WasLeftlastDirection);
            return;
        }

        Vector2 direction;

        if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
        {
            direction = new Vector2(Mathf.Sign(input.x), 0);
            player.PlayerAnimatorControllerExplo.PlayWalk(0); // horizontal
        }
        else
        {
            direction = new Vector2(0, Mathf.Sign(input.y));

            int anim = input.y > 0 ? 1 : 2;
            player.PlayerAnimatorControllerExplo.PlayWalk(anim);
        }

        player.PlayerMovementScript.ApplyMovement(direction, player.PlayerInputReader.WasLeftlastDirection);
    }
    /*private bool IsPlayerMoving()
    {
        return player.PlayerInputReader.Horizontal != 0 || player.PlayerInputReader.Vertical != 0;
    }
    private void DetectLastMovement()
    {
        if (player.PlayerInputReader.LastDirection == LastDirectionMoved.Horizontal)
        {
            player.PlayerMovementScript.ApplyHorizontalMovement();
            player.PlayerAnimatorControllerExplo.PlayWalk(0);
        }
        else
        {
            player.PlayerMovementScript.ApplyVerticalMovement();
            int isPlayerGoingUp = player.PlayerInputReader.Vertical > 0 ? 1 : 2;
            player.PlayerAnimatorControllerExplo.PlayWalk(isPlayerGoingUp);
        }
    }*/
}
