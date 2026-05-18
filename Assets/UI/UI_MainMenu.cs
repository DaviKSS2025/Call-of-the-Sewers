using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UI_MainMenu : MonoBehaviour
{
    // UIDocument que renderiza o UXML do menu.
    [SerializeField]
    private UIDocument uiDocument;

    // Imagem usada como fundo do menu.
    [SerializeField]
    private Texture2D backgroundImage;

    // Imagem usada como titulo/logo do jogo.
    [SerializeField]
    private Texture2D titleImage;

    // Canal usado pelo sistema existente para trocar cenas.
    [SerializeField]
    private SceneChangeChannel sceneChangeChannel;

    [Header("Direct WAV SFX")]
    // AudioSource usado para tocar arquivos .wav/.mp3 arrastados no Inspector.
    [SerializeField]
    private AudioSource audioSource;

    // Som tocado quando o mouse passa por cima de um botao.
    [SerializeField]
    private AudioClip hoverSound;

    // Som tocado quando um botao e clicado.
    [SerializeField]
    private AudioClip clickSound;

    [Header("Event Channel SFX")]
    // Fallback para o sistema antigo de audio por evento.
    [SerializeField]
    private SFXEventChannel audioChannel;

    // Som de selecao usado pelo sistema antigo.
    [SerializeField]
    private SimpleSFXEvent selectSound;

    // Som de confirmacao usado pelo sistema antigo.
    [SerializeField]
    private SimpleSFXEvent useSound;

    // Referencias capturadas do UXML em runtime.
    private VisualElement background;
    private VisualElement titleImageElement;
    private Button continueButton;
    private Button newGameButton;
    private Button quitButton;

    private void Awake()
    {
        // Garante as referencias basicas antes da UI ser registrada.
        uiDocument ??= GetComponent<UIDocument>();
        audioSource ??= GetComponent<AudioSource>();

        // Cria um AudioSource local caso nenhum tenha sido configurado.
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
    }

    private void OnEnable()
    {
        // Sem UIDocument nao existe arvore visual para buscar os botoes.
        if (uiDocument == null)
        {
            Debug.LogWarning("UI_MainMenu precisa de um UIDocument no mesmo GameObject ou no campo Ui Document.", this);
            return;
        }

        VisualElement root = uiDocument.rootVisualElement;

        // Busca os elementos definidos no UI_MainMenu.uxml.
        background = root.Q<VisualElement>("background");
        titleImageElement = root.Q<VisualElement>("titleImage");
        continueButton = root.Q<Button>("continue-button");
        newGameButton = root.Q<Button>("new-game-button");
        quitButton = root.Q<Button>("quit-button");

        // Aplica assets visuais e liga os botoes as acoes do menu.
        ApplyBackgroundImage();
        ApplyTitleImage();
        RegisterButton(continueButton, "continue-button", ContinueMenu);
        RegisterButton(newGameButton, "new-game-button", NewGameButton);
        RegisterButton(quitButton, "quit-button", QuitMenuButton);
    }

    public void SetBackgroundImage(Texture2D texture)
    {
        // Permite trocar a imagem de fundo por codigo.
        backgroundImage = texture;
        ApplyBackgroundImage();
    }

    public void SetTitleImage(Texture2D texture)
    {
        // Permite trocar a imagem do titulo por codigo.
        titleImage = texture;
        ApplyTitleImage();
    }

    private void ApplyBackgroundImage()
    {
        // Aplica o fundo apenas quando o elemento e a textura existem.
        if (background == null || backgroundImage == null)
        {
            return;
        }

        background.style.backgroundImage = new StyleBackground(backgroundImage);
    }

    private void ApplyTitleImage()
    {
        // Aplica a imagem do titulo apenas quando o elemento e a textura existem.
        if (titleImageElement == null || titleImage == null)
        {
            return;
        }

        titleImageElement.style.backgroundImage = new StyleBackground(titleImage);
    }

    private void RegisterButton(Button button, string buttonName, System.Action action)
    {
        // Conecta hover, clique e acao principal de cada botao.
        if (button == null)
        {
            Debug.LogWarning($"UI_MainMenu nao encontrou o botao '{buttonName}' no UXML.", this);
            return;
        }

        button.RegisterCallback<PointerEnterEvent>(_ => PlaySelectSound());
        button.clicked += () =>
        {
            PlayUseSound();
            action?.Invoke();
        };
    }

    // UI Toolkit version of Scripts/Main Menu/ContinueMenuButton.cs.
    // Continue:
    // - se ja existe nome escolhido no save, carrega o mapa atual;
    // - caso contrario, cria um novo jogo e vai para o fluxo de novo jogo.
    private void ContinueMenu()
    {
        // Se nao houver SaveManager na cena, cai direto no fluxo de novo jogo.
        if (SaveManager.Instance == null)
        {
            Debug.LogWarning("UI_MainMenu nao encontrou SaveManager. Iniciando fluxo de novo jogo diretamente.", this);
            LoadSceneDirect(SceneNames.ChangeName);
            return;
        }

        if (SaveManager.Instance.Data.ChoosedNickName)
        {
            // Save valido com nome escolhido: continua do mapa salvo.
            LoadGame();
            return;
        }

        // Save sem nome escolhido: trata como novo jogo.
        NewGame();
    }

    // UI Toolkit version of Scripts/Main Menu/NewGameMenuButton.cs.
    private void NewGameButton()
    {
        NewGame();
    }

    // UI Toolkit version of Scripts/Main Menu/QuitMenuButton.cs.
    private void QuitMenuButton()
    {
        // No Editor, Application.Quit nao para o Play Mode.
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void LoadGame()
    {
        // Carrega a cena salva no SaveManager.
        SceneNames targetScene = SaveManager.Instance.Data.ExplorationData.CurrentMapName;

        // Mantem compatibilidade com listeners que usam o canal de cena.
        if (sceneChangeChannel != null)
        {
            sceneChangeChannel.RaiseGoToTargetScene(targetScene);
        }

        LoadSceneDirect(targetScene);
    }

    private void NewGame()
    {
        // Recria o save quando o SaveManager esta disponivel.
        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.NewGame();
        }
        else
        {
            Debug.LogWarning("UI_MainMenu nao encontrou SaveManager. Carregando ChangeName sem recriar save nesta cena.", this);
        }

        // Mantem compatibilidade com listeners que usam o canal de cena.
        if (sceneChangeChannel != null)
        {
            sceneChangeChannel.RaiseNewGameStarted();
        }

        LoadSceneDirect(SceneNames.ChangeName);
    }

    private void PlaySelectSound()
    {
        // Prioriza AudioClip direto arrastado no Inspector.
        if (hoverSound != null)
        {
            audioSource.PlayOneShot(hoverSound);
            return;
        }

        // Fallback para o sistema antigo de SFX por evento.
        if (audioChannel != null && selectSound != null)
        {
            audioChannel.RaiseEvent(selectSound);
        }
    }

    private void PlayUseSound()
    {
        // Prioriza AudioClip direto arrastado no Inspector.
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
            return;
        }

        // Fallback para o sistema antigo de SFX por evento.
        if (audioChannel != null && useSound != null)
        {
            audioChannel.RaiseEvent(useSound);
        }
    }

    private static void LoadSceneDirect(SceneNames sceneName)
    {
        // Fallback direto para carregar cena mesmo sem SceneChangeController.
        SceneManager.LoadScene(sceneName.ToString());
    }
}
