using UnityEngine;

public class QuitMenuButton : MainMenuBaseButton
{
    public override void OnUsed()
    {
        base.OnUsed();
        Application.Quit();
    }
}
