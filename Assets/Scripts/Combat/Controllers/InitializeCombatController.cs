using System.Collections.Generic;
using UnityEngine;

public class InitializeCombatController : MonoBehaviour
{
    [SerializeField] private EnemyGenerator _enemyGenerator;
    [SerializeField] private NPCGenerator _NPCGenerator;
    [SerializeField] private SelectionChannel _selectionChannel;
    [SerializeField] private TurnChangeChannel _turnChangeChannel;
    [SerializeField] private InputChannel _inputChannel;
    [SerializeField] private CombatChannel _combatChannel;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private GameObject _victoryCanvas;
    [SerializeField] private GameObject _defeatCanvas;
    private SelectTargetController _selectTargetController;
    private CombatController _combatController;
    private TurnBasedController _turnBasedController;
    private TurnOrderManager _turnOrderManager;
    private List<BaseEntityController> _turnOrder = new();

    private void Start()
    {
        SpawnEntities();
        RaiseMonsterAppear();
        InitializeTurnOrderManager();
        InitializeSelectionSystem();
        InitializeCombatSystem();
        ExecuteFirstTurn();
        SubscribeEndGameEvents();
    }
    private void SubscribeEndGameEvents()
    {
        _turnOrderManager.OnEndGame += DisableDependencies;
        _turnOrderManager.OnPlayerVictory += OnPlayerVictory;
        _turnOrderManager.OnPlayerDefeated += OnPlayerDefeated;
    }
    #region Initialize Combat Logic
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
    private void InitializeTurnOrderManager()
    {
        _turnOrderManager = new TurnOrderManager(_turnChangeChannel, _turnOrder);
        _turnOrderManager.Initialize();
    }
    private void InitializeSelectionSystem()
    {
        _selectTargetController = new SelectTargetController(_selectionChannel,_turnOrder, _turnChangeChannel, _inputChannel);
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
#endregion
    private void OnDisable()
    {
        DisableDependencies();
    }
    private void DisableDependencies()
    {
        _selectTargetController.OnDisable();
        _combatController.OnDisable();
        _turnBasedController.OnDisable();
        _turnOrderManager.OnDisable();
        _turnOrderManager.OnEndGame -= DisableDependencies;
    }
    private void OnPlayerVictory()
    {
        _victoryCanvas.SetActive(true);
    }
    private void OnPlayerDefeated()
    {
        _defeatCanvas.SetActive(true);
    }
}
