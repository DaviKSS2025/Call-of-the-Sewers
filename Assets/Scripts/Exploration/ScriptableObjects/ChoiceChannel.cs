using UnityEngine;
using System;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "ChoiceChannel", menuName = "Channels/ChoiceChannel")]
public class ChoiceChannel : ScriptableObject
{
    public Action<List<ChoiceOption>> OnChoiceRequested;
    public Action ChoiceRequested;
    public Action OnChoiceEnd;

    public void RaiseChoiceRequested(List<ChoiceOption> choices)
    {
        OnChoiceRequested?.Invoke(choices);
        ChoiceRequested?.Invoke();
    }
    public void RaiseChoiceEnd()
    {
        OnChoiceEnd?.Invoke();
    }
}
