using UnityEngine;
using UnityEngine.UI;

public class RunOptionCombatButton : OptionCombatBaseButton
{
    public override void OnUsed()
    {
        base.OnUsed();
        if (_playerController.RunManager.HasManaToRun())
        {
            _button.interactable = false;
            _playerController.AnimatorStateController.PlayRun();
            ChangeVerticalButtonNavigation(Navigation.Mode.None);
            _turnChangeChannel.RaiseHideUIOnEndActions();
        }
        else
        {
            PlayErrorSFX();
        }
    }

    private void PlayErrorSFX()
    {

    }
}
