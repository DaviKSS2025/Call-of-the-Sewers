using UnityEngine;
using System;

[CreateAssetMenu(fileName = "SceneChangeChannel", menuName = "Channels/SceneChangeChannel")]
public class SceneChangeChannel : ScriptableObject
{
    public Action SceneChanged;
    public Action<SceneNames> GoToTargetScene;
    public Action NewGameStarted;
    public void RaiseSceneChanged()
    {
        SceneChanged?.Invoke();
    }
    public void RaiseGoToTargetScene(SceneNames targetMap)
    {
        GoToTargetScene?.Invoke(targetMap);
    }
    public void RaiseNewGameStarted()
    {
        NewGameStarted?.Invoke();
    }
}
