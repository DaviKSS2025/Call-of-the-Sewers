using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ChoiceController : MonoBehaviour
{
    private List<ChoiceOption> _currentOptions = new List<ChoiceOption>();
    [SerializeField] private GameObject[] _optionButtons;
    [SerializeField] private TextMeshProUGUI[] _optionTexts;
    [SerializeField] private GameObject _optionPannel;
    [SerializeField] private ChoiceChannel _choiceChannel;
    private bool _canSelectButton;
    private void OnEnable()
    {
        _choiceChannel.OnChoiceRequested += ShowChoices;
    }
    private void OnDisable()
    {
        _choiceChannel.OnChoiceRequested -= ShowChoices;
    }

    private void ShowChoices(List<ChoiceOption> options)
    {
        _currentOptions = options;

        _optionPannel.SetActive(true);
        for (int i = 0; i < _currentOptions.Count; i++)
        {
            _optionButtons[i].SetActive(true);
            _optionTexts[i].text = _currentOptions[i].Text;
        }
        StartCoroutine(EnableSelectionNextFrame());
    }
    private IEnumerator EnableSelectionNextFrame()
    {
        _canSelectButton = false;
        yield return null; // espera 1 frame

        EventSystem.current.SetSelectedGameObject(_optionButtons[0]);
        _canSelectButton = true;
    }
    public void SelectOption(int index)
    {
        if (_canSelectButton)
        {
            _currentOptions[index].Effect.Execute();
            ClearButtons();
            _choiceChannel.RaiseChoiceEnd();
        }
    }
    private void ClearButtons()
    {
        foreach (TextMeshProUGUI text in _optionTexts)
        {
            text.text = null;
        }
        foreach(GameObject go in _optionButtons)
        {
            go.SetActive(false);
        }
        _optionPannel.SetActive(false);
    }
}
public interface IChoiceEffect
{
    void Execute();
}
public class ChoiceOption
{
    private string _text;
    private IChoiceEffect _effect;

    public ChoiceOption(string text, IChoiceEffect effect)
    {
        _text = text;
        _effect = effect;
    }

    public IChoiceEffect Effect => _effect;
    public string Text => _text;
}