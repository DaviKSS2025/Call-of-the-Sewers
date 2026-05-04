using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputName : MonoBehaviour
{
    private TMP_InputField _input;
    [SerializeField] private ChangeNameInterfaceManager _interfaceManager;

    void Awake()
    {
        _input = GetComponent<TMP_InputField>();
    }
    public void OnNameChanged()
    {

    }
    public void OnNameConfirmed()
    {
        _interfaceManager.OpenConfirmationPannel(_input.text);
    }
}
