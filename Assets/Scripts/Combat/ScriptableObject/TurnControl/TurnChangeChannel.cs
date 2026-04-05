using UnityEngine;
using System;

[CreateAssetMenu(fileName = "TurnChangeChannel", menuName = "Channels/TurnChangeChannel")]
public class TurnChangeChannel : ScriptableObject
{
    public Action<BaseEntityController> UpdateCurrentTurnUser;
    public Action<BaseEntityController> EndCurrentTurn;
    public Action HideUIOnEndActions;
    public Action<BaseEntityController> OnEntityDeath;
    public Action OnPlayerTurnStarted;
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
    public void RaiseEntityDeath(BaseEntityController entity)
    {
        OnEntityDeath?.Invoke(entity);
    }
    public void RaiseOnPlayerTurnStarted()
    {
        OnPlayerTurnStarted?.Invoke();
    }
}
