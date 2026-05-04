using UnityEngine;

public class QuitVerticalButton : BaseVerticalToolbarButton
{
    public override void OnUsed()
    {
        base.OnUsed();
        _menuController.OpenQuitGameMenu();
    }
}
