using UnityEngine;

public class PlayerAnimatorControllerExploration
{
    private readonly Animator _animator;

    private static readonly int Death = Animator.StringToHash("Death");
    private static readonly int Walk = Animator.StringToHash("Walk");
    private static readonly int WalkDirection = Animator.StringToHash("WalkDirection");


    //Class to ONLY affect player animations and Animator Parameters
    public PlayerAnimatorControllerExploration(Animator animator)
    {
        _animator = animator;
    }
    public void PlayDeath()
    {
        _animator.SetTrigger(Death);
    }
    public void PlayWalk(int walkDirection)
    {
        _animator.SetBool(Walk, true);
        _animator.SetInteger(WalkDirection, walkDirection);
    }
    public void PlayIdle()
    {
        _animator.SetBool(Walk, false);
    }
}
