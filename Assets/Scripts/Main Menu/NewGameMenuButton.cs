using UnityEngine;

public class NewGameMenuButton : MainMenuBaseButton
{
    [SerializeField] private SceneChangeChannel _sceneChangeChannel;
    public override void OnUsed()
    {
        base.OnUsed();
        NewGame();
    }
    private void NewGame()
    {
        SaveManager.Instance.NewGame();
        _sceneChangeChannel.RaiseNewGameStarted();
    }
}
