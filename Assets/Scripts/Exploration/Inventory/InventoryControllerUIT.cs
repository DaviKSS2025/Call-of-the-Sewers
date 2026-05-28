using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class InventoryControllerUIT : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    [SerializeField] private InputChannel inputChannel;
    [SerializeField] private GameStateChannel gameStateChannel;
    [SerializeField] private bool logToggleDebug;

    private VisualElement root;
    private VisualElement itemsGrid;
    private VisualElement selectedItemIcon;
    private Label emptyStateLabel;
    private Label selectedItemName;
    private Label selectedItemEffect;
    private Label selectedItemLore;

    private readonly List<VisualElement> tabElements = new();
    private VisualElement selectedItemRow;
    private bool isOpen;

    private void Awake()
    {
        uiDocument ??= GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        if (uiDocument == null)
        {
            Debug.LogWarning("InventoryControllerUIT precisa de um UIDocument no mesmo GameObject ou no campo Ui Document.", this);
            return;
        }

        BindUxml();
        RegisterTabCallbacks();
        RegisterVisibilityCallbacks();
        SetInventoryVisible(false);
        ShowEmptyState();
    }

    private void OnDisable()
    {
        UnregisterTabCallbacks();
        UnregisterVisibilityCallbacks();
    }

    private void Update()
    {
        if (gameStateChannel != null || inputChannel != null)
        {
            return;
        }

        if (WasEscapePressedThisFrame())
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        SetInventoryVisible(!isOpen);
    }

    public void OpenInventory()
    {
        SetInventoryVisible(true);
    }

    public void CloseInventory()
    {
        SetInventoryVisible(false);
    }

    private void BindUxml()
    {
        root = uiDocument.rootVisualElement.Q<VisualElement>("inventory-root");
        itemsGrid = uiDocument.rootVisualElement.Q<VisualElement>("inventory-items-grid");
        selectedItemIcon = uiDocument.rootVisualElement.Q<VisualElement>("selected-item-icon");
        emptyStateLabel = uiDocument.rootVisualElement.Q<Label>("empty-state-label");
        selectedItemName = uiDocument.rootVisualElement.Q<Label>("selected-item-name");
        selectedItemEffect = uiDocument.rootVisualElement.Q<Label>("selected-item-effect");
        selectedItemLore = uiDocument.rootVisualElement.Q<Label>("selected-item-lore");

        tabElements.Clear();
        tabElements.Add(uiDocument.rootVisualElement.Q<VisualElement>("tab-items"));
        tabElements.Add(uiDocument.rootVisualElement.Q<VisualElement>("tab-equipment"));
        tabElements.Add(uiDocument.rootVisualElement.Q<VisualElement>("tab-documents"));
    }

    private void SetInventoryVisible(bool visible)
    {
        isOpen = visible;

        if (root == null)
        {
            Debug.LogWarning("InventoryControllerUIT nao encontrou o VisualElement 'inventory-root'.", this);
            return;
        }

        root.style.display = visible ? DisplayStyle.Flex : DisplayStyle.None;

        if (logToggleDebug)
        {
            Debug.Log($"InventoryControllerUIT visible: {visible}", this);
        }

        if (visible)
        {
            PopulateItemsOnOpen();
        }
    }

    private void RegisterVisibilityCallbacks()
    {
        if (gameStateChannel != null)
        {
            gameStateChannel.OnGameStateChange += OnGameStateChanged;
            return;
        }

        if (inputChannel != null)
        {
            inputChannel.OnMenuToggle += ToggleInventory;
        }
    }

    private void UnregisterVisibilityCallbacks()
    {
        if (gameStateChannel != null)
        {
            gameStateChannel.OnGameStateChange -= OnGameStateChanged;
        }

        if (inputChannel != null)
        {
            inputChannel.OnMenuToggle -= ToggleInventory;
        }
    }

    private void OnGameStateChanged(CurrentGameState gameState)
    {
        SetInventoryVisible(gameState == CurrentGameState.StatusPannel);
    }

    private bool WasEscapePressedThisFrame()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            return true;
        }

#if ENABLE_LEGACY_INPUT_MANAGER
        return Input.GetKeyDown(KeyCode.Escape);
#else
        return false;
#endif
    }

    private void PopulateItemsOnOpen()
    {
        // TODO: CONEXÃO COM BACKEND - Popular a lista de itens ao abrir o inventario lendo os dados do backend.
        itemsGrid?.Clear();
        selectedItemRow = null;
        ShowEmptyState();

        List<ConsumableItemData> items = InventoryDataController.Instance != null
            ? InventoryDataController.Instance.GetItemList()
            : null;

        if (items == null)
        {
            return;
        }

        foreach (ConsumableItemData item in items)
        {
            CreateItemRow(item);
        }
    }

    private void RegisterTabCallbacks()
    {
        // TODO: CONEXÃO COM BACKEND - Registrar evento de clique na aba esquerda e mudar categoria.
        foreach (VisualElement tab in tabElements)
        {
            tab?.RegisterCallback<ClickEvent>(OnTabClicked);
        }
    }

    private void UnregisterTabCallbacks()
    {
        foreach (VisualElement tab in tabElements)
        {
            tab?.UnregisterCallback<ClickEvent>(OnTabClicked);
        }
    }

    private void OnTabClicked(ClickEvent evt)
    {
        if (evt.currentTarget is not VisualElement clickedTab)
        {
            return;
        }

        foreach (VisualElement tab in tabElements)
        {
            tab?.RemoveFromClassList("inventory-tab--active");
        }

        clickedTab.AddToClassList("inventory-tab--active");

        // TODO: CONEXÃO COM BACKEND - Filtrar itens pela categoria selecionada.
    }

    private void CreateItemRow(ConsumableItemData item)
    {
        if (itemsGrid == null || item == null)
        {
            return;
        }

        VisualElement row = new();
        row.AddToClassList("inventory-item-row");
        row.userData = item;

        VisualElement icon = new();
        icon.AddToClassList("inventory-item-icon");

        if (item.SpriteImage != null)
        {
            icon.style.backgroundImage = new StyleBackground(item.SpriteImage);
        }

        Label nameLabel = new(item.Name);
        nameLabel.AddToClassList("inventory-item-name");

        row.Add(icon);
        row.Add(nameLabel);
        row.RegisterCallback<ClickEvent>(OnItemClicked);
        itemsGrid.Add(row);
    }

    private void OnItemClicked(ClickEvent evt)
    {
        // TODO: CONEXÃO COM BACKEND - Registrar clique em item da lista inferior e atualizar nome, icone, lore e efeito no painel superior.
        if (evt.currentTarget is not VisualElement row || row.userData is not ConsumableItemData item)
        {
            return;
        }

        selectedItemRow?.RemoveFromClassList("inventory-item-row--selected");
        selectedItemRow = row;
        selectedItemRow.AddToClassList("inventory-item-row--selected");

        ShowItemDetails(item);
    }

    private void ShowItemDetails(ConsumableItemData item)
    {
        if (emptyStateLabel == null || selectedItemName == null || selectedItemEffect == null || selectedItemLore == null || selectedItemIcon == null)
        {
            return;
        }

        emptyStateLabel.style.display = DisplayStyle.None;
        selectedItemName.style.display = DisplayStyle.Flex;
        selectedItemEffect.style.display = DisplayStyle.Flex;
        selectedItemLore.style.display = DisplayStyle.Flex;

        selectedItemName.text = item.Name;
        selectedItemEffect.text = item.Description;
        selectedItemLore.text = "Lore indisponivel neste prototipo.";
        selectedItemIcon.style.backgroundImage = item.SpriteImage != null ? new StyleBackground(item.SpriteImage) : default;
    }

    private void ShowEmptyState()
    {
        if (emptyStateLabel == null || selectedItemName == null || selectedItemEffect == null || selectedItemLore == null || selectedItemIcon == null)
        {
            return;
        }

        emptyStateLabel.style.display = DisplayStyle.Flex;
        selectedItemName.style.display = DisplayStyle.None;
        selectedItemEffect.style.display = DisplayStyle.None;
        selectedItemLore.style.display = DisplayStyle.None;
        selectedItemIcon.style.backgroundImage = default;
    }
}
