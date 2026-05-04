using UnityEngine;

public class CancelReturnButton : BaseVerticalToolbarButton
{
    public override void OnUsed()
    {
        base.OnUsed();
        _menuController.OpenStatusMenu();
    }
}
