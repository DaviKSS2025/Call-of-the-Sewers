using UnityEngine;
using System;

[CreateAssetMenu(fileName = "SceneChangeChannel", menuName = "Channels/SceneChangeChannel")]
public class SceneChangeChannel : ScriptableObject
{
    public Action SceneChanged;

    public void RaiseSceneChanged()
    {
        SceneChanged?.Invoke();
    }
}
