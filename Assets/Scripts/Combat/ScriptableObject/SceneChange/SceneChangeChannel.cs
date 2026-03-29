using UnityEngine;
using System;

[CreateAssetMenu(fileName = "SceneChangeChannel", menuName = "Channels/SceneChangeChannel")]
public class SceneChangeChannel : ScriptableObject
{
    public Action SceneChanged;
    public Action<SceneNames> SceneStartFromMenu;
    public Action NewGameStarted;
    public void RaiseSceneChanged()
    {
        SceneChanged?.Invoke();
    }
    public void RaiseSceneStartFromMenu(SceneNames targetMap)
    {
        SceneStartFromMenu?.Invoke(targetMap);
    }
    public void RaiseNewGameStarted()
    {
        NewGameStarted?.Invoke();
    }
}
