using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ChangeNameInterfaceManager : MonoBehaviour
{
    [SerializeField] private GameObject _confirmationPannel;
    [SerializeField] private GameObject _inputPannel;
    [SerializeField] private TextMeshProUGUI _playerNamePreview;
    [SerializeField] private Button _confirmationButton;
    [SerializeField] private SceneChangeChannel _sceneChangeChannel;
    [SerializeField] protected SFXEventChannel _audioChannel;
    [SerializeField] protected SimpleSFXEvent _selectSound;
    [SerializeField] protected SimpleSFXEvent _useSound;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(_inputPannel.gameObject);
    }
    public void OpenConfirmationPannel(string playerName)
    {
        _audioChannel.RaiseEvent(_useSound);

        _inputPannel.SetActive(false);
        _confirmationPannel.SetActive(true);
        _playerNamePreview.text = playerName;
        EventSystem.current.SetSelectedGameObject(_confirmationButton.gameObject);
    }
    public void ConfirmNickName()
    {
        _audioChannel.RaiseEvent(_useSound);
        SaveController.Instance.SetPlayerName(_playerNamePreview.text);
        _sceneChangeChannel.RaiseGoToTargetScene(SceneNames.Sewers);
    }
    public void CancelNickName()
    {
        _audioChannel.RaiseEvent(_useSound);

        _inputPannel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_inputPannel.gameObject);
        _confirmationPannel.SetActive(false);
    }
    public virtual void OnSelected()
    {
        _audioChannel.RaiseEvent(_selectSound);
    }
}
