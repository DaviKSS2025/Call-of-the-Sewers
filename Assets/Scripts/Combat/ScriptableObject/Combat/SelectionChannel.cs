using System;
using UnityEngine;
[CreateAssetMenu(fileName = "SelectionChannel", menuName = "Channels/SelectionChannel")]
public class SelectionChannel : ScriptableObject
{
    public Action<BaseEntityController> NewTargetSelected;
    public Action<TargetType> SelectionStarted;
    public Action StartSelectionUI;
    public Action SelectionEnd;
    public Action SelectionConfirmed;
    public void RaiseNewTargetSelected(BaseEntityController entity)
    {
        NewTargetSelected?.Invoke(entity);
    }
    public void RaiseSelectionStarted(TargetType targetsToSelect)
    {
        SelectionStarted?.Invoke(targetsToSelect);
        StartSelectionUI?.Invoke();
    }
    public void RaiseSelectionEnd()
    {
        SelectionEnd?.Invoke();
    }
    public void RaiseSelectionConfirmed()
    {
        SelectionConfirmed?.Invoke();
    }
}
