using UnityEngine;

public class BaseAnimatorController
{
    protected Animator _animator;

    private static readonly int Death = Animator.StringToHash("Death");
    private static readonly int TakingDamage = Animator.StringToHash("TakingDamage");
    private static readonly int Following = Animator.StringToHash("Following");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int AttackIndex = Animator.StringToHash("AttackIndex");
    private static readonly int WalkingDirection = Animator.StringToHash("WalkingDirection");

    public BaseAnimatorController(Animator animator)
    {
        _animator = animator;
    }

    public virtual void PlayDeath()
    {
        _animator.SetTrigger(Death);
    }
    public virtual void PlayTakingDamage()
    {
        _animator.SetTrigger(TakingDamage);
    }
    public virtual void PlayWandering(int walkingDirection)
    {
        _animator.SetInteger(WalkingDirection, walkingDirection);
    }
    public virtual void PlayAttack(int attackIndex)
    {
        _animator.SetTrigger(Attack);
        _animator.SetInteger(AttackIndex, attackIndex);
    }
    public virtual void PlayFollowing()
    {
        //_animator.SetTrigger(Following);
    }
    public virtual void PlayIdle()
    {
        _animator.SetTrigger(Idle);
    }
}
