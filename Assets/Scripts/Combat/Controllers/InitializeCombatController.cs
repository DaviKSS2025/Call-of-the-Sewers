using System.Collections.Generic;
using UnityEngine;

public class InitializeCombatController : MonoBehaviour
{
    [SerializeField] private EnemyGenerator _enemyGenerator;
    [SerializeField] private NPCGenerator _NPCGenerator;
    [SerializeField] private SelectionChannel _selectionChannel;
    [SerializeField] private TurnChangeChannel _turnChangeChannel;
    [SerializeField] private CombatChannel _combatChannel;
    [SerializeField] private PlayerController _playerController;
    private SelectTargetController _selectTargetController;
    private CombatController _combatController;
    private TurnBasedController _turnBasedController;
    [SerializeField] private List<BaseEntityController> _turnOrder = new();

    private void Start()
    {
        SpawnEntities();
        RaiseMonsterAppear();
        InitializeSelectionSystem();
        InitializeCombatSystem();
        ExecuteFirstTurn();
    }

    private void SpawnEntities()
    {
        _turnOrder.Clear();

        _turnOrder.Add(_playerController);

        _turnOrder.AddRange(_NPCGenerator.Initialize());
        _turnOrder.AddRange(_enemyGenerator.Initialize());
    }
    private void RaiseMonsterAppear()
    {
        foreach (BaseEntityController firstEnemy in _turnOrder)
        {
            if (firstEnemy.EntityType == TargetType.Enemy)
            {
                _combatChannel.RaiseMonsterAppearing(firstEnemy.EntityNameString);
                break;
            }
        }
    }
    private void InitializeSelectionSystem()
    {
        _selectTargetController = new SelectTargetController(_selectionChannel,_turnOrder);
        _selectTargetController.Initialize();
    }
    private void InitializeCombatSystem()
    {
        _combatController = new CombatController(_combatChannel, _selectionChannel, _turnChangeChannel, _turnOrder);
        _combatController.Initialize();
    }
    private void ExecuteFirstTurn()
    {
        _turnBasedController = new TurnBasedController(_turnChangeChannel, _turnOrder);
        _turnBasedController.StartTurns();
    }
    private void OnDisable()
    {
        _selectTargetController.OnDisable();
        _combatController.OnDisable();
        _turnBasedController.OnDisable();
    }
}
