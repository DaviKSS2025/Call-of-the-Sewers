using System.Collections.Generic;
public class SelectTargetController
{
    private SelectionChannel _selectionChannel;
    private TurnChangeChannel _turnChangeChannel;
    private InputChannel _inputChannel;
    private List<BaseEntityController> _turnOrder = new();
    private List<BaseEntityController> _currentTargetList = new();
    private bool isPlayerSelectingTargets;
    private int _selectionIndex;
    private TargetType _entityType = TargetType.Enemy;
    public SelectTargetController(SelectionChannel selectionChannel, List<BaseEntityController> turnOrder, TurnChangeChannel turnChangeChannel, InputChannel inputChannel)
    {
        _selectionChannel = selectionChannel;
        _turnOrder = turnOrder;
        _turnChangeChannel = turnChangeChannel;
        _inputChannel = inputChannel;
    }
    public void Initialize()
    {
        _selectionChannel.SelectionStarted += StartSelection;

        _turnChangeChannel.OnTurnOrderChanged += UpdateTurnOrder;
    }
    public void OnDisable()
    {
        UnsubscribeSelectionEvents();

        _selectionChannel.SelectionStarted -= StartSelection;

        _turnChangeChannel.OnTurnOrderChanged -= UpdateTurnOrder;
    }
    private void SubscribeSelectionEvents()
    {
        _inputChannel.OnUIRight += OnRightPerformed;
        _inputChannel.OnUILeft += OnLeftPerformed;
        _inputChannel.OnUICancel += OnCancelPerformed;

        _selectionChannel.SelectionConfirmed += StopSelection;
    }
    private void UnsubscribeSelectionEvents()
    {
        _inputChannel.OnUIRight -= OnRightPerformed;
        _inputChannel.OnUILeft -= OnLeftPerformed;
        _inputChannel.OnUICancel -= OnCancelPerformed;

        _selectionChannel.SelectionConfirmed -= StopSelection;
    }
    private void StartSelection(TargetType entityType)
    {
        SubscribeSelectionEvents();
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
        UnsubscribeSelectionEvents();
    }
    private void OnCancelPerformed()
    {
        if (isPlayerSelectingTargets)
        {
            StopSelection();
            _selectionChannel.RaiseSelectionEnd();
        }
    }
    private void OnRightPerformed()
    {
        NavigateOnTargetSelectionArray(1);
    }
    private void OnLeftPerformed()
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
    private void UpdateTurnOrder(List<BaseEntityController> turnOrder)
    {
        _turnOrder = turnOrder;
    }
}