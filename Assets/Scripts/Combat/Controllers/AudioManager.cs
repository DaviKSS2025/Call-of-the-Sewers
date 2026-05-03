using UnityEngine;
using UnityEngine.Audio;

/// <summary>
///Central audio controller.
///Listens to audio event channels and routes SFX and Music playback
///through dedicated AudioSources and an AudioMixer for global volume control.
///AudioManager does not know who requests sounds.
///It only reacts to raised audio events, keeping systems decoupled.
/// </summary>
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _SFXSource;
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private SFXEventChannel _SFXChannel;
    [SerializeField] private MusicEventChannel _musicChannel;
    [SerializeField] private AudioMixer _globalVolumeGroup; // Controls global Music/SFX volumes
    [SerializeField] private SimpleMusicEvent _menuStartMusic; // Music played on menu startup

    public static AudioManager Instance { get; private set; }
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        _SFXChannel.OnSFXRequested += PlaySFX;
        _musicChannel.OnMusicRequested += PlayMusic;
    }
    private void OnDisable() 
    {
        _SFXChannel.OnSFXRequested -= PlaySFX;
        _musicChannel.OnMusicRequested -= PlayMusic;
    }
    private void Start()
    {
        // Start menu music and apply saved volume preferences
        _musicChannel.RaiseEvent(_menuStartMusic);
    }

    private void PlaySFX(SimpleSFXEvent sfx)
    {
        sfx.Play(_SFXSource);
    }
    private void PlayMusic(SimpleMusicEvent sfx)
    {
        sfx.Play(_musicSource);
    }
}
