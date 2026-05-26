using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource[] _SFXSources;
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private SFXEventChannel _SFXChannel;
    [SerializeField] private MusicEventChannel _musicChannel;
    [SerializeField] private SimpleMusicEvent _menuStartMusic; // Music played on menu startup

    private int _currentSource;

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
        _musicChannel.RaiseEvent(_menuStartMusic);
    }

    private void PlaySFX(SimpleSFXEvent sfx)
    {
        sfx.Play(_SFXSources[_currentSource]);

        _currentSource++;

        if (_currentSource >= _SFXSources.Length)
        {
            _currentSource = 0;
        }
    }
    private void PlayMusic(SimpleMusicEvent sfx)
    {
        sfx.Play(_musicSource);
    }
}
