public abstract class BaseSkillBehaviour
{
    public SkillData Data { get; }
    protected BaseEntityController _controller;
    protected string _stringToShow;
    protected string _target;
    public BaseEntityController Controller
    {
        set => _controller = value;
    }
    public BaseSkillBehaviour(SkillData data)
    {
        Data = data;
    }
    public virtual void PreparingSkill()
    {
        _stringToShow = $"<color=red>{_controller.EntityNameString}</color> cast <color=red>{Data.Name}</color> on <color=red>{_target}</color>";
    }
    public virtual void UsingSkill()
    {
        _controller.Stats.UseMana(Data.ManaCost);
        _controller.AnimatorStateController.PlaySkill();
    }
    public virtual void OnSkillEnd()
    {
        _controller.ThisInputChannel.OnUICancel -= CancelingUse;
    }
    public virtual void CancelingUse()
    {
        _controller.ThisInputChannel.OnUICancel -= CancelingUse;
    }
}
