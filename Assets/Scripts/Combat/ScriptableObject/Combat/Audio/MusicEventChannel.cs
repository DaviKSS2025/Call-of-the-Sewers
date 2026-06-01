using System;
using UnityEngine;

[CreateAssetMenu(fileName = "MusicEventChannel", menuName = "Scriptable Objects/MusicEventChannel")]
public class MusicEventChannel : ScriptableObject
{
    public event Action<SimpleMusicEvent> OnMusicRequested;

    public void RaiseEvent(SimpleMusicEvent musicEvent)
    {
        OnMusicRequested?.Invoke(musicEvent);
    }
}
