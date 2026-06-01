using UnityEngine;

public class CerberusAnimatorStateController : AnimatorStateController, IAnimationHandler
{
    public CerberusAnimatorStateController(Animator animator) : base(animator)
    {
    }

    public override void PlayPreparing()
    {
        _animator.SetTrigger(Preparing);
    }
    public override void PlayIdleTurn()
    {
        _animator.SetTrigger(IdleTurn);
    }
}
