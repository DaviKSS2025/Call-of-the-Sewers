using UnityEngine;

public class DJonesNPCController : NPCController
{
    [SerializeField] private SkillData _darkHold;
    [SerializeField] private SkillData _darkFire;
    [SerializeField] private SkillData _darkHealing;
    [SerializeField] private SkillData _darkness;
    [Header("Sound effects")]
    [SerializeField] private SimpleSFXEvent _knifeSFX;
    [SerializeField] private SimpleSFXEvent _damageSFX;
    [SerializeField] private SimpleSFXEvent _spellSFX;
    protected override void SetupSkillManager()
    {
        AssignSkillManager(new DJonesSkillManager(this));
    }
    protected override void SetupStrategy()
    {
        AssignStrategy(new DJonesStrategy(_animatorStateController, _combatChannel, _darkHold, _darkFire, _darkHealing, _darkness, _entityListHandler));
    }
    public override void OnAnimationEvent(string eventName)
    {
        if (eventName == "StartDamage")
        {
            _attackController.LaunchRandomAttack();
            _SFXChannel.RaiseEvent(_knifeSFX);
        }
        else if (eventName == "AttackEnd")
        {
            NeutralTurnEnd();
        }
        else if (eventName == "SkillEnd")
        {
            _skillManager.OnDisable();
            NeutralTurnEnd();
        }
        else if (eventName == "PrepareEnd")
        {
            _attackController.ChooseRandomAttack();
        }
        else if (eventName == "DeathEnd")
        {
            _animatorStateController.PlayDeath();
        }
        else if (eventName == "DamageTaken")
        {
            _SFXChannel.RaiseEvent(_damageSFX);
        }
        else if (eventName == "SpellDamage")
        {
            _SFXChannel.RaiseEvent(_spellSFX);
        }
    }
}
