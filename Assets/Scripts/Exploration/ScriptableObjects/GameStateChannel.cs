using UnityEngine;
using System;
[CreateAssetMenu(fileName = "GameStateChannel", menuName = "Channels/GameStateChannel")]
public class GameStateChannel : ScriptableObject
{
    public Action<CurrentGameState> OnGameStateChange;

    public void RaiseGameStateChange(CurrentGameState state)
    {
        OnGameStateChange?.Invoke(state);
    }
}
