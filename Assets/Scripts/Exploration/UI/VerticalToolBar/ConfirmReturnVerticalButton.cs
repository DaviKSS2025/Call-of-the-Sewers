using UnityEngine;
using UnityEngine.EventSystems;

public class ConfirmReturnVerticalButton : BaseVerticalToolbarButton
{
    [SerializeField] private SceneChangeChannel _sceneChangeChannel;

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
    }
    public override void OnUsed()
    {
        base.OnUsed();
        _sceneChangeChannel.RaiseGoToTargetScene(SceneNames.MainMenu);
    }
}
