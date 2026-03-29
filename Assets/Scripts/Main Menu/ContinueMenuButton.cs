using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ContinueMenuButton : MainMenuBaseButton
{
    [SerializeField] private SceneChangeChannel _sceneChangeChannel;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(gameObject);

        ChangeVerticalButtonNavigation(Navigation.Mode.Automatic);
    }
    public override void OnUsed()
    {
        base.OnUsed();
        if (SaveManager.Instance.Data.ChoosedNickName)
        {
            LoadGame();
        }
        else
        {
            NewGame();
        }
    }
    private void LoadGame()
    {
        _sceneChangeChannel.RaiseSceneStartFromMenu(SaveManager.Instance.Data.CurrentMapName);
    }
    private void NewGame()
    {
        SaveManager.Instance.NewGame();
        _sceneChangeChannel.RaiseNewGameStarted();
    }
    private void ChangeVerticalButtonNavigation(Navigation.Mode mode)
    {
        var nav = _button.navigation;
        nav.mode = mode;
        _button.navigation = nav;
    }
}
