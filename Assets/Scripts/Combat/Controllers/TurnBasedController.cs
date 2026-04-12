using System.Collections.Generic;

public class TurnBasedController
{
    private TurnChangeChannel _turnChangeChannel;
    private List<BaseEntityController> _entityList;

    public TurnBasedController(TurnChangeChannel turnChangeChannel, List<BaseEntityController> entityList)
    {
        _turnChangeChannel = turnChangeChannel;
        _entityList = entityList;
    }
    public void StartTurns()
    {
        _turnChangeChannel.RaiseUpdateCurrentTurnUser(_entityList[0]);
        _turnChangeChannel.EndCurrentTurn += StartNextTurn;
        _turnChangeChannel.OnTurnOrderChanged += UpdateTurnOrder;
    }
    public void OnDisable()
    {
        _turnChangeChannel.EndCurrentTurn -= StartNextTurn;
        _turnChangeChannel.OnTurnOrderChanged -= UpdateTurnOrder;
    }

    private void StartNextTurn(BaseEntityController entity)
    {
        int currentIndex = _entityList.IndexOf(entity);
        int indexToGo = currentIndex < _entityList.Count - 1 ? currentIndex + 1 : 0;
        _turnChangeChannel.RaiseUpdateCurrentTurnUser(_entityList[indexToGo]);
    }
    private void UpdateTurnOrder(List<BaseEntityController> turnOrder)
    {
        _entityList = turnOrder;
    }
}
