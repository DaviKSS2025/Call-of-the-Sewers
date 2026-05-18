using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UI_ChangeName : MonoBehaviour
{
    // UIDocument que renderiza o UXML desta tela.
    [SerializeField]
    private UIDocument uiDocument;

    // Imagem do rosto do personagem exibida no painel esquerdo.
    [SerializeField]
    private Texture2D portraitImage;

    // Canal usado pelo sistema existente para trocar cenas.
    [SerializeField]
    private SceneChangeChannel sceneChangeChannel;

    // Limite simples para impedir nomes muito longos na UI/save.
    [SerializeField]
    private int maxNameLength = 16;

    [Header("SFX")]
    // AudioSource usado para tocar os .wav desta tela.
    [SerializeField]
    private AudioSource audioSource;

    // Som tocado quando o texto do nome muda.
    [SerializeField]
    private AudioClip typingSound;

    // Som tocado quando o nome e aceito.
    [SerializeField]
    private AudioClip acceptNameSound;

    private VisualElement portrait;
    private TextField nameInput;
    private VisualElement root;
    private bool isConfirmingName;

    private void Awake()
    {
        uiDocument ??= GetComponent<UIDocument>();
        audioSource ??= GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
    }

    private void OnEnable()
    {
        if (uiDocument == null)
        {
            Debug.LogWarning("UI_ChangeName precisa de um UIDocument no mesmo GameObject ou no campo Ui Document.", this);
            return;
        }

        root = uiDocument.rootVisualElement;

        // Busca os elementos definidos no UI_ChangeName.uxml.
        portrait = root.Q<VisualElement>("portrait");
        nameInput = root.Q<TextField>("name-input");

        ApplyPortraitImage();
        ConfigureInput();

        if (Keyboard.current != null)
        {
            Keyboard.current.onTextInput += OnTextInput;
        }
    }

    private void OnDisable()
    {
        if (Keyboard.current != null)
        {
            Keyboard.current.onTextInput -= OnTextInput;
        }

        if (nameInput == null)
        {
            return;
        }

        nameInput.UnregisterCallback<PointerDownEvent>(OnInputPointerDown);
        nameInput.UnregisterValueChangedCallback(OnNameChanged);
        root?.UnregisterCallback<PointerDownEvent>(OnRootPointerDown, TrickleDown.TrickleDown);
        root?.UnregisterCallback<KeyDownEvent>(OnRootKeyDown, TrickleDown.TrickleDown);
    }

    private void Update()
    {
        if (Keyboard.current == null || nameInput == null || isConfirmingName)
        {
            return;
        }

        if (Keyboard.current.enterKey.wasPressedThisFrame || Keyboard.current.numpadEnterKey.wasPressedThisFrame)
        {
            ConfirmName();
            return;
        }

        if (Keyboard.current.backspaceKey.wasPressedThisFrame)
        {
            RemoveLastCharacter();
            return;
        }

        if (Keyboard.current.deleteKey.wasPressedThisFrame)
        {
            nameInput.value = string.Empty;
        }
    }

    // Permite trocar a imagem do rosto por codigo.
    public void SetPortraitImage(Texture2D texture)
    {
        portraitImage = texture;
        ApplyPortraitImage();
    }

    private void ApplyPortraitImage()
    {
        if (portrait == null || portraitImage == null)
        {
            return;
        }

        portrait.style.backgroundImage = new StyleBackground(portraitImage);
        portrait.style.unityBackgroundScaleMode = ScaleMode.ScaleToFit;
        portrait.style.backgroundPositionX = new BackgroundPosition(BackgroundPositionKeyword.Center);
        portrait.style.backgroundPositionY = new BackgroundPosition(BackgroundPositionKeyword.Center);
    }

    private void ConfigureInput()
    {
        if (nameInput == null)
        {
            Debug.LogWarning("UI_ChangeName nao encontrou o TextField 'name-input' no UXML.", this);
            return;
        }

        nameInput.value = string.Empty;
        nameInput.maxLength = maxNameLength;
        nameInput.isDelayed = false;
        nameInput.label = string.Empty;
        nameInput.RegisterCallback<PointerDownEvent>(OnInputPointerDown);
        nameInput.RegisterValueChangedCallback(OnNameChanged);
        root.focusable = true;
        root.tabIndex = 0;
        root.RegisterCallback<PointerDownEvent>(OnRootPointerDown, TrickleDown.TrickleDown);
        root.RegisterCallback<KeyDownEvent>(OnRootKeyDown, TrickleDown.TrickleDown);
        ApplyInputTextStyle();
        FocusInput();
    }

    private void OnRootPointerDown(PointerDownEvent evt)
    {
        FocusInput();
    }

    private void OnInputPointerDown(PointerDownEvent evt)
    {
        FocusInput();
        evt.StopPropagation();
    }

    private void FocusInput()
    {
        root.Focus();
        nameInput.schedule.Execute(() =>
        {
            root.Focus();
        }).StartingIn(0);
    }

    private void ApplyInputTextStyle()
    {
        nameInput.style.color = Color.black;
        nameInput.style.backgroundColor = Color.white;

        TextElement textElement = nameInput.Q<TextElement>();

        if (textElement == null)
        {
            return;
        }

        textElement.style.color = Color.black;
        textElement.style.backgroundColor = Color.white;
        textElement.style.unityTextAlign = TextAnchor.MiddleCenter;
    }

    private void OnNameChanged(ChangeEvent<string> evt)
    {
        if (typingSound != null)
        {
            audioSource.PlayOneShot(typingSound);
        }
    }

    private void OnRootKeyDown(KeyDownEvent evt)
    {
        if (Keyboard.current != null)
        {
            return;
        }

        if (evt.keyCode == KeyCode.Return || evt.keyCode == KeyCode.KeypadEnter)
        {
            ConfirmName();
            evt.StopPropagation();
            return;
        }

        if (evt.keyCode == KeyCode.Backspace)
        {
            RemoveLastCharacter();
            evt.StopPropagation();
            return;
        }

        if (evt.keyCode == KeyCode.Delete)
        {
            nameInput.value = string.Empty;
            evt.StopPropagation();
            return;
        }

        if (evt.character == '\0' || char.IsControl(evt.character))
        {
            return;
        }

        AddCharacter(evt.character);
        evt.StopPropagation();
    }

    private void OnTextInput(char character)
    {
        if (nameInput == null || isConfirmingName || char.IsControl(character))
        {
            return;
        }

        AddCharacter(character);
    }

    private void AddCharacter(char character)
    {
        if (nameInput.value.Length >= maxNameLength)
        {
            return;
        }

        nameInput.value += character;
    }

    private void RemoveLastCharacter()
    {
        if (string.IsNullOrEmpty(nameInput.value))
        {
            return;
        }

        nameInput.value = nameInput.value.Substring(0, nameInput.value.Length - 1);
    }

    // Salva o nome digitado e avanca para Sewers.
    public void ConfirmName()
    {
        if (nameInput == null || isConfirmingName)
        {
            return;
        }

        string playerName = nameInput.value.Trim();

        if (string.IsNullOrEmpty(playerName))
        {
            return;
        }

        if (SaveController.Instance != null)
        {
            SaveController.Instance.SetPlayerName(playerName);
        }
        else if (SaveManager.Instance != null)
        {
            SaveManager.Instance.Data.PlayerData.PlayerName = playerName;
            SaveManager.Instance.Data.ChoosedNickName = true;
            SaveManager.Instance.Save();
        }
        else
        {
            Debug.LogWarning("UI_ChangeName nao encontrou SaveController nem SaveManager. O nome nao foi salvo.", this);
        }

        isConfirmingName = true;
        PlayAcceptNameSound();
        Invoke(nameof(GoToSewers), GetAcceptNameDelay());
    }

    private void PlayAcceptNameSound()
    {
        if (acceptNameSound != null)
        {
            audioSource.PlayOneShot(acceptNameSound);
        }
    }

    private float GetAcceptNameDelay()
    {
        if (acceptNameSound == null)
        {
            return 0f;
        }

        return Mathf.Min(acceptNameSound.length, 0.35f);
    }

    private void GoToSewers()
    {
        if (sceneChangeChannel != null)
        {
            sceneChangeChannel.RaiseGoToTargetScene(SceneNames.Sewers);
        }

        SceneManager.LoadScene(SceneNames.Sewers.ToString());
    }
}
