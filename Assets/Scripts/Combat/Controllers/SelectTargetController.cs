using UnityEngine.InputSystem;
using System.Collections.Generic;
public class SelectTargetController
{
    private SelectionChannel _selectionChannel;
    private InputSystem_Actions _inputActions;
    private List<BaseEntityController> _turnOrder = new();
    private List<BaseEntityController> _currentTargetList = new();
    private bool isPlayerSelectingTargets;
    private int _selectionIndex;
    private TargetType _entityType = TargetType.Enemy;
    public SelectTargetController(SelectionChannel selectionChannel, List<BaseEntityController> turnOrder)
    {
        _selectionChannel = selectionChannel;
        _turnOrder = turnOrder;
    }
    public void Initialize()
    {
        _inputActions = new InputSystem_Actions();
        _inputActions.UI.Enable();

        _inputActions.UI.RightPressed.performed += OnRightPerformed;
        _inputActions.UI.LeftPressed.performed += OnLeftPerformed;
        _inputActions.UI.Cancel.performed += OnCancelPerformed;

        _selectionChannel.SelectionStarted += StartSelection;
        _selectionChannel.SelectionConfirmed += StopSelection;
    }
    public void OnDisable()
    {
        _inputActions.Disable();

        _inputActions.UI.RightPressed.performed -= OnRightPerformed;
        _inputActions.UI.LeftPressed.performed -= OnLeftPerformed;
        _inputActions.UI.Cancel.performed -= OnCancelPerformed;

        _selectionChannel.SelectionStarted -= StartSelection;
        _selectionChannel.SelectionConfirmed -= StopSelection;
    }

    private void StartSelection(TargetType entityType)
    {
        _currentTargetList.Clear();
        foreach (BaseEntityController entity in _turnOrder)
        {
            if (entity.EntityType == entityType)
            {
                _currentTargetList.Add(entity);
            }
        }
        if (_currentTargetList.Count == 0)
        {
            return;
        }

        _selectionIndex = 0;
        _entityType = entityType;
        isPlayerSelectingTargets = true;
        UpdateTargetName();
    }
    private void StopSelection()
    {
        isPlayerSelectingTargets = false;
    }
    private void OnCancelPerformed(InputAction.CallbackContext context)
    {
        if (isPlayerSelectingTargets)
        {
            StopSelection();
            _selectionChannel.RaiseSelectionEnd();
        }
    }
    private void OnRightPerformed(InputAction.CallbackContext context)
    {
        NavigateOnTargetSelectionArray(1);
    }
    private void OnLeftPerformed(InputAction.CallbackContext context)
    {
        NavigateOnTargetSelectionArray(-1);
    }
    private void NavigateOnTargetSelectionArray(int input)
    {
        if (!isPlayerSelectingTargets)
            return;



        int delta = _entityType == TargetType.Enemy ? -input : input;
        int newIndex = _selectionIndex + delta;

        if (newIndex < 0 || newIndex >= _currentTargetList.Count)
            return;

        _selectionIndex = newIndex;
        UpdateTargetName();
    }
    private void UpdateTargetName()
    {
        _selectionChannel.RaiseNewTargetSelected(_currentTargetList[_selectionIndex]);
    }
}