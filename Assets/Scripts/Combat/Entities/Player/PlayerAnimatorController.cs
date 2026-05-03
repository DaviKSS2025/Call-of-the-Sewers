using System.Diagnostics;
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
    public override void PlaySkill()
    {
        StackTrace trace = new StackTrace();
        UnityEngine.Debug.Log(trace);
        _animator.SetTrigger(Skill);
    }
}
