using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _statusMenu;
    [SerializeField] private GameObject _inventoryMenu;
    [SerializeField] private GameObject _quitGameMenu;
    [SerializeField] private GameObject _verticalToolbar;
    [SerializeField] private InventoryChannel _inventoryChannel;
    [SerializeField] private GameObject _charPannel;
    [SerializeField] private GameObject _NPCPannel;
    [SerializeField] private Selectable _charPannelSelectable;
    [SerializeField] private Selectable _NPCPannelSelectable;
    [SerializeField] private GameStateChannel _gameStateChannel;
    [SerializeField] private InputChannel _inputChannel;
    [SerializeField] private SFXEventChannel _sfxChannel;
    [SerializeField] private SimpleSFXEvent _selectSFX;
    [SerializeField] private SimpleSFXEvent _useSFX;
    public Action OpenedMenu;
    public Action ClosedMenu;
    private IConsumableEffectOnTarget _consumableUsed;
    private void Start()
    {
        _gameStateChannel.OnGameStateChange += OnToggleMenuPerformed;
        _inventoryChannel.OpenSelectTargetOnStatusPannel += OpenStatusSelectionTarget;
        _inventoryChannel.InstantItemUsed += CloseAllMenus;
        _inventoryChannel.MatchesUsed += CloseAllMenus;
    }
    private void OnDisable()
    {
        _gameStateChannel.OnGameStateChange -= OnToggleMenuPerformed;
        _inventoryChannel.OpenSelectTargetOnStatusPannel -= OpenStatusSelectionTarget;
        _inventoryChannel.InstantItemUsed -= CloseAllMenus;
        _inventoryChannel.MatchesUsed -= CloseAllMenus;
    }

    private void OnToggleMenuPerformed(CurrentGameState gameState)
    {
        if (gameState == CurrentGameState.StatusPannel)
        {
            OpenStatusMenu();
        }
        else if (gameState == CurrentGameState.Gameplay)
        {
            CloseAllMenus();
        }
    }
    private void CancelSelectTarget()
    {
        _charPannelSelectable.interactable = false;
        _NPCPannelSelectable.interactable = false;
    }
    private void OpenStatusSelectionTarget(IConsumableEffectOnTarget consumableEffect)
    {
        _verticalToolbar.SetActive(false);
        _inventoryMenu.SetActive(false);
        _statusMenu.SetActive(true);
        _charPannelSelectable.interactable = true;
        _NPCPannelSelectable.interactable = NPCDataController.Instance.RuntimeData.Count > 0;
        _consumableUsed = consumableEffect;
        EventSystem.current.SetSelectedGameObject(_charPannel);
        _inputChannel.OnSubmit += ManageTargetEffectSelection;
    }
    public void OpenStatusMenu()
    {
        _mainMenu.SetActive(true);
        _verticalToolbar.SetActive(true);
        _statusMenu.SetActive(true);
        OpenedMenu?.Invoke();
        _inventoryMenu.SetActive(false);
        _quitGameMenu.SetActive(false);
    }
    public void OpenInventoryMenu()
    {
        _inventoryMenu.SetActive(true);
        _statusMenu.SetActive(false);
        _quitGameMenu.SetActive(false);
    }
    public void OpenQuitGameMenu()
    {
        _quitGameMenu.SetActive(true);
        _verticalToolbar.SetActive(false);
        _inventoryMenu.SetActive(false);
        _statusMenu.SetActive(false);
    }
    public void CloseAllMenus()
    {
        _statusMenu.SetActive(false);
        _inventoryMenu.SetActive(false);
        _quitGameMenu.SetActive(false);
        CancelSelectTarget();
        ClosedMenu?.Invoke();
        _mainMenu.SetActive(false);
        _inputChannel.OnSubmit -= ManageTargetEffectSelection;
    }
    public void PlaySelectedSFX()
    {
        _sfxChannel.RaiseEvent(_selectSFX);
    }
    public void PlayUseSFX()
    {
        _sfxChannel.RaiseEvent(_useSFX);
    }
    private void ManageTargetEffectSelection()
    {
        if (_charPannel == EventSystem.current.currentSelectedGameObject)
        {
            _consumableUsed.Execute(TargetType.Player);
        }
        else
        {
            _consumableUsed.Execute(TargetType.NPC);
        }
        _inventoryChannel.RaiseInstantItemUsed();
        CloseAllMenus();
    }
}
