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
    public Action OpenedMenu;
    public Action ClosedMenu;
    private void Start()
    {
        _gameStateChannel.OnGameStateChange += OnToggleMenuPerformed;
        _inventoryChannel.OpenSelectTargetOnStatusPannel += OpenStatusSelectionTarget;
    }
    private void OnDisable()
    {
        _gameStateChannel.OnGameStateChange -= OnToggleMenuPerformed;
        _inventoryChannel.OpenSelectTargetOnStatusPannel -= OpenStatusSelectionTarget;
    }

    private void OnToggleMenuPerformed(CurrentGameState gameState)
    {
        if (gameState == CurrentGameState.StatusPannel)
        {
            OpenStatusMenu();
        }
        else if (gameState == CurrentGameState.Gameplay)
        {
            _statusMenu.SetActive(false);
            _inventoryMenu.SetActive(false);
            _quitGameMenu.SetActive(false);
            CancelSelectTarget();
            ClosedMenu?.Invoke();
            _mainMenu.SetActive(false);
        }
    }
    private void CancelSelectTarget()
    {
        _charPannelSelectable.enabled = false;
        _NPCPannelSelectable.enabled = false;
    }
    private void OpenStatusSelectionTarget()
    {
        _verticalToolbar.SetActive(false);
        _inventoryMenu.SetActive(false);
        _statusMenu.SetActive(true);
        _charPannelSelectable.enabled = true;
        _NPCPannelSelectable.enabled = true;
        EventSystem.current.SetSelectedGameObject(_charPannel);
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
}
