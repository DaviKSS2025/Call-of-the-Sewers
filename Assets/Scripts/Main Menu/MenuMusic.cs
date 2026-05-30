using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    [SerializeField] private SimpleMusicEvent _menuStartMusic; // Music played on menu startup
    [SerializeField] private MusicEventChannel _musicChannel;

    private void Start()
    {
        _musicChannel.RaiseEvent(_menuStartMusic);
    }
}
