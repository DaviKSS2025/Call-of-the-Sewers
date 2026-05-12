using UnityEngine;
using UnityEngine.EventSystems;

public class StatusVerticalButton : BaseVerticalToolbarButton
{
    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
    }
    public override void OnUsed()
    {
        base.OnUsed();
        _menuController.OpenStatusMenu();
    }
}
