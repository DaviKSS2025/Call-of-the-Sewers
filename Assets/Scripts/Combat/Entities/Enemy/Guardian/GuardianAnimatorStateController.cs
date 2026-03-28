using UnityEngine;

public class GuardianAnimatorStateController : AnimatorStateController, IAnimationHandler
{
    public GuardianAnimatorStateController(Animator animator) : base(animator)
    {
    }

    public override void PlayPreparing()
    {
        _animator.SetTrigger(Preparing);
    }
}
