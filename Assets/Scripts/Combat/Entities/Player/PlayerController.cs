using UnityEngine;

public class PlayerController : BaseEntityController
{
    [SerializeField] private RunChance _runChance;
    [SerializeField] private SceneChangeChannel _sceneChangeChannel;
    [SerializeField] private CharacterStatsUI _playerStatsUI;
    private RunManager _runManager;
    private SkillManager _skillManager;
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
        AssignStatsController(new PlayerStats(this, _playerStatsUI));
    }
    public override void Start()
    {
        _name = SaveManager.Instance.Data.PlayerData.PlayerName;
        base.Start();
        _runManager = new RunManager(_stats,_runChance.RunChancePercentage, _sceneChangeChannel, _combatChannel, _name);
        _skillManager = new SkillManager(this);
        _skillManager.Initialize();
    }
    public override void ExecuteTurnStart()
    {
       _turnChannel.RaiseOnPlayerTurnStarted();
       _selectionChannel.RaiseSelectionEnd();
    }
    public override void OnAnimationEvent(string eventName)
    {
        if (eventName == "StartDamage")
        {
            _attackController.LaunchAttack();
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
                _runManager.ExecuteRun();
            }
            else
            {
                NeutralTurnEnd();
            }
        }
        else if (eventName == "SkillEnd")
        {
            _skillManager.SkillEnd();
            _combatChannel.RaiseSkillEnd();
        }
    }
}
