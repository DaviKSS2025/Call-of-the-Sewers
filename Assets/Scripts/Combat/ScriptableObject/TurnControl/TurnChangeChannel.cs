using UnityEngine;
using System;

[CreateAssetMenu(fileName = "TurnChangeChannel", menuName = "Channels/TurnChangeChannel")]
public class TurnChangeChannel : ScriptableObject
{
    public Action<BaseEntityController> UpdateCurrentTurnUser;
    public Action<BaseEntityController> EndCurrentTurn;
    public Action HideUIOnEndActions;
    public void RaiseUpdateCurrentTurnUser(BaseEntityController entity)
    {
        UpdateCurrentTurnUser?.Invoke(entity);
    }
    public void RaiseEndCurrentTurn(BaseEntityController entity)
    {
        EndCurrentTurn?.Invoke(entity);
    }
    public void RaiseHideUIOnEndActions()
    {
        HideUIOnEndActions?.Invoke();
    }
}
