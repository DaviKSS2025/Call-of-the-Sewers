using UnityEngine;

public class InventoryVerticalButton : BaseVerticalToolbarButton
{
    public override void OnUsed()
    {
        base.OnUsed();
        _menuController.OpenInventoryMenu();
    }
}
