using UnityEngine;
public class VictoryCanvas : MonoBehaviour
{
    [SerializeField] private SceneChangeChannel _sceneChangeChannel;
    private SceneNames _targetScene = SceneNames.Sewers;

    public SceneNames TargetScene
    {
        set => _targetScene = value;
    }
    public void ReturnToLastRoom()
    {
        _sceneChangeChannel.RaiseGoToTargetScene(_targetScene);
    }
}
