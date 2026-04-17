using UnityEngine;

public class BlackoutController : MonoBehaviour
{
    [SerializeField] private CutsceneChannel _cutsceneChannel;

    public void OnBlackoutMiddle()
    {
        _cutsceneChannel.RaiseBlackoutMiddle();
    }
    public void OnBlackoutEnd()
    {
        _cutsceneChannel.RaiseCutsceneEnd();
        gameObject.SetActive(false);
    }
}
