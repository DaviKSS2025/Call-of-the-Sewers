using UnityEngine;

public interface IPlayerState
{
    // Called once when state changes
    void OnEnter();

    // Called on Update() during the state
    void OnUpdate();

    // Reset flags when state changes
    void OnExit();

    // Animation methods
    void HandleAnimationEvent(string eventName);
}
