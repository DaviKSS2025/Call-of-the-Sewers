using UnityEngine;
using System.Collections;

public class PlayerController : BaseEntityController
{
    [SerializeField] private RunChance _runChance;
    [SerializeField] private SceneChangeChannel _sceneChangeChannel;
    [SerializeField] private CharacterStatsUI _playerStatsUI;
    private RunManager _runManager;
    
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
        base.Start();
        _runManager = new RunManager(_stats,_runChance.RunChancePercentage, _sceneChangeChannel, _combatChannel, _name.Name);
    }
    public override void ExecuteTurnStart()
    {
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
    }
    private void NeutralTurnEnd()
    {
        _animatorStateController.PlayIdle();
        OnTurnEnd();
    }

    /*private IEnumerator WaitForAnimation(string stateName)
    {
        // 1. Aguarda o Animator transicionar para o estado (evita pegar o estado anterior)
        yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).IsName(stateName));

        // 2. Aguarda a animação terminar (NormalizedTime >= 1 significa 100% concluída)
        while (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }

        // Lógica de desfecho
        //HandleRunResult();
    }*/
}
