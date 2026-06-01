using UnityEngine;

public class PlayerAnimatorControllerExploration
{
    private readonly Animator _animator;

    private static readonly int Walk = Animator.StringToHash("Walk");
    private static readonly int WalkDirection = Animator.StringToHash("WalkDirection");


    //Class to ONLY affect player animations and Animator Parameters
    public PlayerAnimatorControllerExploration(Animator animator)
    {
        _animator = animator;
    }
    public void PlayWalk(Vector2 input)
    {
        _animator.SetBool(Walk, true);

        int direction;

        if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
        {
            direction = input.x > 0 ? 1 : 2; // right : left
        }
        else
        {
            direction = input.y > 0 ? 3 : 4; // up : down
        }

        _animator.SetInteger(WalkDirection, direction);
    }
    public void PlayIdle()
    {
        _animator.SetBool(Walk, false);
    }
}