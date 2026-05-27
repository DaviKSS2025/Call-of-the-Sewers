using UnityEngine;
using UnityEngine.UI;

public class DJonesNPCController : NPCController
{
    [SerializeField] private SkillData _darkHold;
    [SerializeField] private SkillData _darkFire;
    [SerializeField] private SkillData _darkHealing;
    [SerializeField] private SkillData _darkness;
    [SerializeField] private Sprite[] _skillSprites;
    private CombatSpriteController _spriteController;
    private Image _image;

    public override void Awake()
    {
        base.Awake();
        _image = GetComponent<Image>();
    }
    public override void Start()
    {
        base.Start();
        _spriteController = new CombatSpriteController(_image, _skillSprites);
    }
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
        else if (eventName == "SkillStart")
        {
            _spriteController.SortRandomSkillSprite();
        }
    }
}
