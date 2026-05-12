using UnityEngine;
using UnityEngine.UIElements;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField]
    private UIDocument uiDocument;

    [SerializeField]
    private Texture2D backgroundImage;

    [SerializeField]
    private Texture2D titleImage;

    [SerializeField]
    private SceneChangeChannel sceneChangeChannel;

    [SerializeField]
    private SFXEventChannel audioChannel;

    [SerializeField]
    private SimpleSFXEvent selectSound;

    [SerializeField]
    private SimpleSFXEvent useSound;

    private VisualElement background;
    private VisualElement titleImageElement;
    private Button continueButton;
    private Button newGameButton;
    private Button quitButton;

    private void Awake()
    {
        uiDocument ??= GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        if (uiDocument == null)
        {
            Debug.LogWarning("UI_MainMenu precisa de um UIDocument no mesmo GameObject ou no campo Ui Document.", this);
            return;
        }

        VisualElement root = uiDocument.rootVisualElement;

        background = root.Q<VisualElement>("background");
        titleImageElement = root.Q<VisualElement>("titleImage");
        continueButton = root.Q<Button>("continue-button");
        newGameButton = root.Q<Button>("new-game-button");
        quitButton = root.Q<Button>("quit-button");

        ApplyBackgroundImage();
        ApplyTitleImage();
        RegisterButton(continueButton, ContinueGame);
        RegisterButton(newGameButton, NewGame);
        RegisterButton(quitButton, QuitGame);
    }

    // Arraste a imagem de fundo no campo Background Image do Inspector.
    // Se preferir trocar em codigo, chame SetBackgroundImage(texture).
    public void SetBackgroundImage(Texture2D texture)
    {
        backgroundImage = texture;
        ApplyBackgroundImage();
    }

    // Arraste a imagem do titulo do jogo no campo Title Image do Inspector.
    // Este item fica separado do container dos botoes para voce poder posicionar
    // e trocar o titulo sem afetar a navegacao do menu.
    public void SetTitleImage(Texture2D texture)
    {
        titleImage = texture;
        ApplyTitleImage();
    }

    private void ApplyBackgroundImage()
    {
        if (background == null || backgroundImage == null)
        {
            return;
        }

        background.style.backgroundImage = new StyleBackground(backgroundImage);
    }

    private void ApplyTitleImage()
    {
        if (titleImageElement == null || titleImage == null)
        {
            return;
        }

        titleImageElement.style.backgroundImage = new StyleBackground(titleImage);
    }

    private void RegisterButton(Button button, System.Action action)
    {
        if (button == null)
        {
            return;
        }

        button.RegisterCallback<PointerEnterEvent>(_ => PlaySelectSound());
        button.clicked += () =>
        {
            PlayUseSound();
            action?.Invoke();
        };
    }

    private void ContinueGame()
    {
        if (sceneChangeChannel == null)
        {
            Debug.LogWarning("UI_MainMenu precisa de um SceneChangeChannel para continuar o jogo.", this);
            return;
        }

        if (SaveManager.Instance.Data.ChoosedNickName)
        {
            sceneChangeChannel.RaiseGoToTargetScene(SaveManager.Instance.Data.ExplorationData.CurrentMapName);
            return;
        }

        NewGame();
    }

    private void NewGame()
    {
        if (sceneChangeChannel == null)
        {
            Debug.LogWarning("UI_MainMenu precisa de um SceneChangeChannel para iniciar um novo jogo.", this);
            return;
        }

        SaveManager.Instance.NewGame();
        sceneChangeChannel.RaiseNewGameStarted();
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private void PlaySelectSound()
    {
        if (audioChannel != null && selectSound != null)
        {
            audioChannel.RaiseEvent(selectSound);
        }
    }

    private void PlayUseSound()
    {
        if (audioChannel != null && useSound != null)
        {
            audioChannel.RaiseEvent(useSound);
        }
    }
}
