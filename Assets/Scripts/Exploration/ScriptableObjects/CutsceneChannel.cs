using UnityEngine;
using System;
[CreateAssetMenu(fileName = "CutsceneChannel", menuName = "Channels/CutsceneChannel")]
public class CutsceneChannel : ScriptableObject
{
    public Action OnBlackoutRequested;
    public Action OnBlackoutMiddle;
    public Action OnCutsceneEnd;
    public Action OnCombatTransitionCutscene;
    public Action OnHalfBlackoutRequested;
    public void RaiseBlackoutRequested()
    {
        OnBlackoutRequested?.Invoke();
    }
    public void RaiseHalfBlackoutRequested()
    {
        OnHalfBlackoutRequested?.Invoke();
        OnCombatTransitionCutscene?.Invoke();
    }
    public void RaiseCutsceneEnd()
    {
        OnCutsceneEnd?.Invoke();
    }
    public void RaiseBlackoutMiddle()
    {
        OnBlackoutMiddle?.Invoke();
    }
}
