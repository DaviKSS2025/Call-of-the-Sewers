using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    [SerializeField] private CutsceneChannel _cutsceneChannel;
    [SerializeField] private SceneChangeChannel _sceneChangeChannel;
    [SerializeField] private GameObject _blackoutObject;
    [SerializeField] private BlackoutController _blackoutController;

    private void OnEnable()
    {
        _cutsceneChannel.OnBlackoutRequested += OnBlackoutRequested;
        _cutsceneChannel.OnCombatTransitionCutscene += ChangeSceneAfterCutscene;
        _cutsceneChannel.OnHalfBlackoutRequested += OnHalfBlackoutRequested;
    }
    private void OnDisable()
    {
        _cutsceneChannel.OnBlackoutRequested -= OnBlackoutRequested;
        _cutsceneChannel.OnCombatTransitionCutscene -= ChangeSceneAfterCutscene;
        _cutsceneChannel.OnBlackoutMiddle -= CallCombatScene;
        _cutsceneChannel.OnHalfBlackoutRequested -= OnHalfBlackoutRequested;
    }
    private void OnBlackoutRequested()
    {
        _blackoutObject.SetActive(true);
    }
    private void ChangeSceneAfterCutscene()
    {
        _cutsceneChannel.OnBlackoutMiddle += CallCombatScene;
    }
    private void CallCombatScene()
    {
        _sceneChangeChannel.GoToTargetScene(SceneNames.Combat);
        _cutsceneChannel.OnBlackoutMiddle -= CallCombatScene;
    }
    private void OnHalfBlackoutRequested()
    {
        _blackoutObject.SetActive(true);
        _blackoutController.PlayHalfBlackout();
    }
}
