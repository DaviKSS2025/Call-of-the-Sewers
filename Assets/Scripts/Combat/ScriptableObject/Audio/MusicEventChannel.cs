using System;
using UnityEngine;

[CreateAssetMenu(fileName = "MusicEventChannel", menuName = "Scriptable Objects/MusicEventChannel")]
public class MusicEventChannel : ScriptableObject
{
    // Agora o evento avisa: "Alguém quer tocar ESSE som aqui"
    public event Action<SimpleMusicEvent> OnMusicRequested;

    public void RaiseEvent(SimpleMusicEvent musicEvent)
    {
        OnMusicRequested?.Invoke(musicEvent);
    }
}
