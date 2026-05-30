using UnityEngine;
public class SkillManager : ISkillUser
{
    protected BaseEntityController _controller;
    protected BaseSkillBehaviour _currentSkill;
    public string ControllerName
    {
        get => _controller.EntityNameString;
    }
    public int CurrentMana
    {
        get => _controller.Stats.CurrentMana;
    }
    public int MaxMana
    {
        get => _controller.SurvStats.MaxMana;
    }
    public SkillManager(BaseEntityController controller)
    {
        _controller = controller;
    }
    public void Initialize()
    {
        _controller.UnscribeEventsOnDisable += OnDisable;
    }
    public void PrepareToListenEvents()
    {
        _controller.ComChannel.OnSkillUsed -= PreparingSkill;
        _controller.ComChannel.OnSkillUsed += PreparingSkill;
    }
    public virtual void PreparingSkill(SkillData skillData)
    {
        _currentSkill = skillData.CreateInstance(this);
        _currentSkill.PreparingSkill();
        _controller.ThisTurnChangeChannel.RaiseHideUIOnEndActions();
    }
    public void SkillEnd()
    {
        _currentSkill?.OnSkillEnd();
    }
    public void OnDisable()
    {
        _controller.ComChannel.OnSkillUsed -= PreparingSkill;
        _controller.ThisInputChannel.OnUICancel -= CancelingUse;
        _controller.ThisInputChannel.OnSubmit -= UsingTargetSkill;
        _controller.UnscribeEventsOnDisable -= OnDisable;
    }
    public virtual void PrepareTargetSkill()
    {
        _controller.SelectionChannel.RaiseSelectionStarted(TargetType.Enemy);
        _controller.ThisInputChannel.OnUICancel += CancelingUse;
        _controller.ThisInputChannel.OnSubmit += UsingTargetSkill;
    }
    public virtual void CancelingUse()
    {
        _controller.ThisInputChannel.OnUICancel -= CancelingUse;
        _controller.ThisInputChannel.OnSubmit -= UsingTargetSkill;
        _controller.ComChannel.RaiseCancelChoosingSkill();
    }
    public virtual void UsingTargetSkill()
    {
        _controller.ThisInputChannel.OnUICancel -= CancelingUse;
        _controller.ThisInputChannel.OnSubmit -= UsingTargetSkill;

        BaseTargetAttackSkillBehaviour targetSkill = _currentSkill as BaseTargetAttackSkillBehaviour;

        _controller.ComChannel.RaiseTargetAttackSkillRequested(targetSkill.GetDamage(), targetSkill.GetStatusList(), targetSkill.TargetData.CriticalChance);
        UseManaAndPlayAnimation();
        _controller.SelectionChannel.RaiseSelectionConfirmed();
    }
    public virtual void OnSkillEnd()
    {
        OnDisable();
    }
    public void UseGlobalStatusEffectSkill(StatusEffectData statusEffect, int statusChance, string stringToShow, int duration)
    {
        UseManaAndPlayAnimation();
        _controller.ComChannel.RaiseGlobalStatusEffectUsed(TargetType.Enemy, statusEffect, statusChance, stringToShow, duration);
    }
    protected virtual void UseManaAndPlayAnimation()
    {
        _controller.Stats.UseMana(_currentSkill.Data.GetManaCost(_controller));
        _controller.AnimatorStateController.PlaySkill();
    }
    public void UseGlobalHealingSkill(int healAmount)
    {
        _controller.ComChannel.RaiseGlobalHealRequested(_controller.EntityType, healAmount);
        UseManaAndPlayAnimation();
    }
}

public interface ISkillUser
{
    public string ControllerName { get; }
    public int CurrentMana { get; }
    public int MaxMana { get; }
    public void PrepareTargetSkill();
    public void CancelingUse();
    public void UsingTargetSkill();
    public void OnSkillEnd();
    public void UseGlobalStatusEffectSkill(StatusEffectData statusEffect, int statusChance, string stringToShow, int duration);
    public void UseGlobalHealingSkill(int healAmount);
}
