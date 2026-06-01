using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BlackoutController : MonoBehaviour
{
    [SerializeField] private CutsceneChannel _cutsceneChannel;
    [SerializeField] protected SFXEventChannel _sfxChannel;
    [SerializeField] protected SimpleSFXEvent _doomSFX;
    private Animator _animator;

    private static int HalfBlackout = Animator.StringToHash("HalfBlackout");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnBlackoutMiddle()
    {
        _cutsceneChannel.RaiseBlackoutMiddle();
    }
    public void OnBlackoutEnd()
    {
        _cutsceneChannel.RaiseCutsceneEnd();
        gameObject.SetActive(false);
    }
    public void PlayHalfBlackout()
    {
        _animator.SetTrigger(HalfBlackout);
        _sfxChannel.RaiseEvent(_doomSFX);
    }
}
