public class SkillManager
{
    private BaseEntityController _controller;
    private BaseSkillBehaviour _currentSkill;

    public SkillManager(BaseEntityController controller)
    {
        _controller = controller;
    }
    public void Initialize()
    {
        _controller.ComChannel.OnSkillUsed += PreparingSkill;
        _controller.UnscribeEventsOnDisable += OnDisable;
    }

    private void PreparingSkill(SkillData skillData)
    {
        _currentSkill = skillData.CreateInstance();
        _currentSkill.Controller = _controller;
        _currentSkill.PreparingSkill();
        _controller.ThisTurnChangeChannel.RaiseHideUIOnEndActions();
    }
    public void SkillEnd()
    {
        _currentSkill?.OnSkillEnd();
    }
    private void OnDisable()
    {
        _controller.ComChannel.OnSkillUsed -= PreparingSkill;
        _controller.UnscribeEventsOnDisable -= OnDisable;
    }
}
