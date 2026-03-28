using UnityEngine;

public abstract class AnimatorStateController : IAnimationHandler
{
    protected Animator _animator;

    // Cached animator parameter hashes to avoid string lookups at runtime
    protected static readonly int Death = Animator.StringToHash("Death");
    protected static readonly int TakingDamage = Animator.StringToHash("TakingDamage");
    protected static readonly int Idle = Animator.StringToHash("Idle");
    protected static readonly int Unselected = Animator.StringToHash("Unselected");
    protected static readonly int IsSelected = Animator.StringToHash("IsSelected");
    protected static readonly int Attacking = Animator.StringToHash("Attacking");
    protected static readonly int AttackIndex = Animator.StringToHash("AttackIndex");
    protected static readonly int Run = Animator.StringToHash("Run");
    protected static readonly int Preparing = Animator.StringToHash("Preparing");

    public AnimatorStateController(Animator animator)
    {
        _animator = animator;
    }

    #region UniversalAnimations
    public virtual void PlaySelected()
    {
        _animator.SetBool(IsSelected, true);
    }
    public virtual void PlayUnselected()
    {
        _animator.SetBool(IsSelected, false);
    }
    public virtual void PlayDeath()
    {
        _animator.SetTrigger(Death);
    }
    public virtual void PlayIdle()
    {
        _animator.SetBool(Attacking, false);
        _animator.SetTrigger(Idle);
    }
    public virtual void PlayTakingDamage()
    {
        _animator.SetTrigger(TakingDamage);
    }

    public virtual void PlayAttackStart(int attackIndex)
    {
        _animator.SetBool(Attacking, true);
        _animator.SetInteger(AttackIndex, attackIndex);
    }
    public virtual void PlayAttackEnd()
    {
        _animator.SetBool(Attacking, false);
        PlayIdle();
    }
    #endregion
    public virtual void PlayRun()
    {
    }
    public virtual void PlayPreparing()
    {
    }
}
