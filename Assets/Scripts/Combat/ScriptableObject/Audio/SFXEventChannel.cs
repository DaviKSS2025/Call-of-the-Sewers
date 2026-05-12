using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Channels/SFXChannel")]
public class SFXEventChannel : ScriptableObject
{
    // Agora o evento avisa: "Alguém quer tocar ESSE som aqui"
    public event Action<SimpleSFXEvent> OnSFXRequested;

    public void RaiseEvent(SimpleSFXEvent sfxEvent)
    {
        OnSFXRequested?.Invoke(sfxEvent);
    }
}
