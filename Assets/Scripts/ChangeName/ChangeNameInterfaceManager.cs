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

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(_inputPannel.gameObject);
    }
    public void OpenConfirmationPannel(string playerName)
    {
        _inputPannel.SetActive(false);
        _confirmationPannel.SetActive(true);
        _playerNamePreview.text = playerName;
        EventSystem.current.SetSelectedGameObject(_confirmationButton.gameObject);
    }
    public void ConfirmNickName()
    {
        SaveController.Instance.SetPlayerName(_playerNamePreview.text);
        _sceneChangeChannel.SceneStartFromMenu(SceneNames.Sewers);
    }
    public void CancelNickName()
    {
        _inputPannel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_inputPannel.gameObject);
        _confirmationPannel.SetActive(false);
    }
}
