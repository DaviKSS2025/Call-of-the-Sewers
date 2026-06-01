using UnityEngine;

public class BaseAnimatorController
{
    protected Animator _animator;
    private static readonly int WalkingDirection = Animator.StringToHash("WalkingDirection");

    public BaseAnimatorController(Animator animator)
    {
        _animator = animator;
    }

    public virtual void PlayWandering(int walkingDirection)
    {
        _animator.SetInteger(WalkingDirection, walkingDirection);
    }
}
