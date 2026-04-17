using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    [SerializeField] private CutsceneChannel _cutsceneChannel;
    [SerializeField] private GameObject _blackoutObject;
    private void OnEnable()
    {
        _cutsceneChannel.OnBlackoutRequested += OnBlackoutRequested;
    }
    private void OnDisable()
    {
        _cutsceneChannel.OnBlackoutRequested -= OnBlackoutRequested;
    }
    private void OnBlackoutRequested()
    {
        _blackoutObject.SetActive(true);
    }
}
