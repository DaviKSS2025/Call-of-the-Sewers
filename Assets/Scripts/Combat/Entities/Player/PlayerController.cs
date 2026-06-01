using System.Collections;
using UnityEngine;
public class PlayerController : BaseEntityController
{
    [SerializeField] private RunChance _runChance;
    [SerializeField] private SceneChangeChannel _sceneChangeChannel;
    [SerializeField] private PlayerStatsUI _playerStatsUI;
    [SerializeField] private ArmorDatabase _armorDatabase;
    [SerializeField] private WeaponDatabase _weaponDatabase;
    private RunManager _runManager;
    private SkillManager _skillManager;

    [Header("Sound effects")]
    [SerializeField] private SimpleSFXEvent _knifeSFX;
    [SerializeField] private SimpleSFXEvent _damageSFX;
    [SerializeField] private SimpleSFXEvent _spellSFX;

    public RunManager RunManager
    {
        get => _runManager;
    }
    protected override void SetupAnimationController()
    {
        AssignAnimationController(new PlayerAnimatorController(_animator));
    }
    protected override void SetupStatsController()
    {
        AssignStatsController(new PlayerStats(this, _playerStatsUI, _armorDatabase.GetArmorScriptableObject(PlayerDataController.Instance.RuntimeData.CurrentArmor).DefenseMultiplier));
    }
    public override void Start()
    {
        _name = SaveManager.Instance.Data.PlayerData.PlayerName;
        base.Start();
        _attackController.AttackMultiplier = _weaponDatabase.GetWeaponScriptableObject(PlayerDataController.Instance.RuntimeData.CurrentWeapon).DamageMultiplier;
        _runManager = new RunManager(_stats,_runChance.RunChancePercentage, _sceneChangeChannel, _combatChannel, _name);
        _skillManager = new SkillManager(this);
        _skillManager.Initialize();
        _stats.SubscribeEvents();
    }
    public override void ExecuteTurnStart()
    {
       _turnChannel.RaiseOnPlayerTurnStarted();
       _selectionChannel.RaiseSelectionEnd();
       _skillManager.PrepareToListenEvents();
    }
    public override void NeutralTurnEnd()
    {
        _skillManager.OnDisable();
        base.NeutralTurnEnd();
    }
    public override void OnAnimationEvent(string eventName)
    {
        if (eventName == "StartDamage")
        {
            _attackController.LaunchAttack();
            _SFXChannel.RaiseEvent(_knifeSFX);
        }
        else if (eventName == "AttackEnd")
        {
            NeutralTurnEnd();
        }
        else if (eventName == "RunStart")
        {
            _runManager.RunStarted();
        }
        else if (eventName == "RunResult")
        {
            _runManager.RollRunChance();
        }
        else if (eventName == "RunEnd")
        {
            if (_runManager.WasRunSuccesfull)
            {
                _turnChannel.RaisePlayerRan();
                _runManager.ExecuteRun();
            }
            else
            {
                StartCoroutine(WaitForRunDelay());
            }
        }
        else if (eventName == "SkillEnd")
        {
            _skillManager.SkillEnd();
            _skillManager.OnDisable();
            _combatChannel.RaiseSkillEnd();
            NeutralTurnEnd();
        }
        else if (eventName == "SpellDamage")
        {
            _SFXChannel.RaiseEvent(_spellSFX);
        }
        else if (eventName == "DamageTaken")
        {
            _SFXChannel.RaiseEvent(_damageSFX);
        }
    }
    private IEnumerator WaitForRunDelay()
    {
        yield return new WaitForSeconds(0.5f);
        NeutralTurnEnd();
    }
}
