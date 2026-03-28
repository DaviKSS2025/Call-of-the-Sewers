using UnityEngine;

public class PlayerAnimatorController : AnimatorStateController, IAnimationHandler
{
    public PlayerAnimatorController(Animator animator) : base(animator)
    {
    }


    public override void PlayRun()
    {
        _animator.SetTrigger(Run);
    }
}
