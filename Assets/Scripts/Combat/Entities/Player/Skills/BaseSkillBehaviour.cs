public abstract class BaseSkillBehaviour
{
    protected SkillData _data;
    protected string _stringToShow;
    protected string _target;
    protected ISkillUser _user;

    public SkillData Data
    {
        get => _data;
    }
    public BaseSkillBehaviour(SkillData data, ISkillUser user)
    {
        _data = data;
        _user = user;
    }
    public virtual void PreparingSkill()
    {
        _stringToShow = $"<color=red>{_user.ControllerName}</color> cast <color=red>{Data.Name}</color> on <color=red>{_target}</color>";
    }
    public abstract void UsingSkill();
    public virtual void OnSkillEnd()
    {
        _user.OnSkillEnd();
    }
    public virtual void CancelingUse()
    {
        _user.CancelingUse();
    }
}
