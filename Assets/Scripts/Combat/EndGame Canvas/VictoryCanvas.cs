using UnityEngine;
public class VictoryCanvas : MonoBehaviour
{
    [SerializeField] private SceneChangeChannel _sceneChangeChannel;
    public void ReturnToLastRoom()
    {
        _sceneChangeChannel.RaiseGoToTargetScene(MapDataController.Instance.RuntimeData.CurrentSceneName);
    }
}
