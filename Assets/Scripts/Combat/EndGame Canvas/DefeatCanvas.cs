using UnityEngine;

public class DefeatCanvas : MonoBehaviour
{
    [SerializeField] private SceneChangeChannel _sceneChangeChannel;
    public void ReturnToMainMenu()
    {
        _sceneChangeChannel.RaiseGoToTargetScene(SceneNames.MainMenu);
    }
}
