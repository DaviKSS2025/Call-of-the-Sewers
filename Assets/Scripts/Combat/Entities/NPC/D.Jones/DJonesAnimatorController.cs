using UnityEngine;

public class DJonesAnimatorController : AnimatorStateController, IAnimationHandler
{
    public DJonesAnimatorController(Animator animator) : base(animator)
    {
    }
    public override void PlayPreparing()
    {
        _animator.SetTrigger(Preparing);
    }
    public override void PlaySkill()
    {
        _animator.SetTrigger(Skill);
    }
}
