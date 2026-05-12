using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UI_Combat : MonoBehaviour
{
    // Representa um personagem exibido no menuCharacter.
    // Por enquanto a UI trabalha com valores inteiros de 0 a 100.
    // Quando voce conectar com os scripts reais do jogo, preencha estes campos
    // usando os dados do personagem/player/NPC em vez de valores fixos no Inspector.
    [Serializable]
    private class CharacterStatus
    {
        public string Name = "Character Name";

        [Range(0, 100)]
        public int Vitalidade = 100;

        [Range(0, 100)]
        public int Sanidade = 100;
    }

    [SerializeField]
    private UIDocument uiDocument;

    [SerializeField]
    private string targetName = "Target Name";

    [SerializeField]
    private string combatText = "New Text";

    // Lista de personagens do jogador exibida no menuCharacter.
    // No Inspector, aumente o tamanho dessa lista para testar uma party com varios personagens.
    // Em codigo, use SetCharacters(...) para substituir a lista inteira,
    // SetCharacterStats(...) para atualizar Vitalidade/Sanidade de um personagem especifico,
    // e SetCharacterName(...) para trocar o nome exibido.
    [SerializeField]
    private List<CharacterStatus> characters = new List<CharacterStatus>
    {
        new CharacterStatus()
    };

    private Label targetNameLabel;
    private Label combatTextLabel;
    private VisualElement menuCharacter;
    private Button selectedActionButton;

    private void Awake()
    {
        uiDocument ??= GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        if (uiDocument == null)
        {
            Debug.LogWarning("UI_Combat precisa de um UIDocument no mesmo GameObject ou no campo Ui Document.", this);
            return;
        }

        VisualElement root = uiDocument.rootVisualElement;

        targetNameLabel = root.Q<Label>("target-name");
        combatTextLabel = root.Q<Label>("combat-text");
        menuCharacter = root.Q<VisualElement>("menuCharacter");

        RegisterActionButton(root, "attack-button", "You attack.");
        RegisterActionButton(root, "skill-button", "Choose a skill.");
        RegisterActionButton(root, "run-button", "You try to run.");

        Refresh();
    }

    // Atalho para atualizar o primeiro personagem da lista.
    // Use isso se o jogo tiver apenas um personagem jogavel, ou enquanto estiver prototipando.
    public void SetStats(int newVitalidade, int newSanidade)
    {
        SetCharacterStats(0, newVitalidade, newSanidade);
    }

    // Atualiza Vitalidade e Sanidade de um personagem pelo indice na lista.
    // Exemplo de uso a partir de outro script:
    //
    // [SerializeField] private UI_Combat combatUi;
    //
    // private void OnPlayerDamaged(int currentHp, int currentSanity)
    // {
    //     combatUi.SetCharacterStats(0, currentHp, currentSanity);
    // }
    //
    // characterIndex = 0 altera o primeiro personagem, 1 altera o segundo, e assim por diante.
    public void SetCharacterStats(int characterIndex, int vitalidade, int sanidade)
    {
        if (characterIndex < 0 || characterIndex >= characters.Count)
        {
            return;
        }

        characters[characterIndex].Vitalidade = Mathf.Clamp(vitalidade, 0, 100);
        characters[characterIndex].Sanidade = Mathf.Clamp(sanidade, 0, 100);
        Refresh();
    }

    // Atualiza o nome do alvo no topo da tela.
    // Chame quando selecionar/trocar o inimigo atual.
    // Exemplo: combatUi.SetTargetName(enemy.DisplayName);
    public void SetTargetName(string newTargetName)
    {
        targetName = newTargetName;
        Refresh();
    }

    // Atalho para renomear o primeiro personagem da lista.
    public void SetCharacterName(string newCharacterName)
    {
        SetCharacterName(0, newCharacterName);
    }

    // Renomeia um personagem especifico pelo indice da lista.
    // Use quando montar a UI a partir da party real do jogador.
    public void SetCharacterName(int characterIndex, string newCharacterName)
    {
        if (characterIndex < 0 || characterIndex >= characters.Count)
        {
            return;
        }

        characters[characterIndex].Name = newCharacterName;
        Refresh();
    }

    // Atualiza o texto de combate solto na tela.
    // Chame isso para mensagens como dano, erro, defesa, fuga, status etc.
    // Exemplo: combatUi.SetCombatText("O inimigo perdeu 12 de Vitalidade.");
    public void SetCombatText(string newCombatText)
    {
        combatText = newCombatText;
        Refresh();
    }

    // Registra um botao do menuActions.
    // O fundo do botao so aparece quando ele recebe a classe "selected",
    // entao sempre que um botao e clicado removemos a selecao anterior
    // e marcamos apenas o botao atual.
    private void RegisterActionButton(VisualElement root, string buttonName, string message)
    {
        Button button = root.Q<Button>(buttonName);

        if (button == null)
        {
            return;
        }

        button.RegisterCallback<ClickEvent>(_ =>
        {
            SelectActionButton(button);
            SetCombatText(message);
        });
    }

    private void SelectActionButton(Button button)
    {
        selectedActionButton?.RemoveFromClassList("selected");
        selectedActionButton = button;
        selectedActionButton.AddToClassList("selected");
    }

    // Substitui todos os personagens exibidos no menuCharacter.
    // Este metodo e o ponto mais direto para conectar a UI com uma party real.
    // Exemplo:
    //
    // combatUi.SetCharacters(new[]
    // {
    //     ("Arisa", 82, 60),
    //     ("D. Jones", 47, 35),
    // });
    public void SetCharacters(IEnumerable<(string name, int vitalidade, int sanidade)> newCharacters)
    {
        characters.Clear();

        foreach ((string name, int vitalidade, int sanidade) in newCharacters)
        {
            characters.Add(new CharacterStatus
            {
                Name = name,
                Vitalidade = Mathf.Clamp(vitalidade, 0, 100),
                Sanidade = Mathf.Clamp(sanidade, 0, 100)
            });
        }

        Refresh();
    }

    private void Refresh()
    {
        if (targetNameLabel != null)
        {
            targetNameLabel.text = targetName;
        }

        if (combatTextLabel != null)
        {
            combatTextLabel.text = combatText;
        }

        RefreshCharacterList();
    }

    private void RefreshCharacterList()
    {
        if (menuCharacter == null)
        {
            return;
        }

        menuCharacter.Clear();

        foreach (CharacterStatus character in characters)
        {
            menuCharacter.Add(CreateCharacterContainer(character));
        }
    }

    // Cria visualmente um item da lista do menuCharacter.
    // Cada personagem vira:
    // containerCharacter -> character + containerBars
    // containerBars -> vitality + sanity
    // vitality/sanity -> numero + barra preenchida.
    // Se voce mudar os nomes/classes no UXML, mantenha este metodo sincronizado.
    private static VisualElement CreateCharacterContainer(CharacterStatus character)
    {
        VisualElement container = new VisualElement();
        container.name = "containerCharacter";
        container.AddToClassList("character-container");

        Label nameLabel = new Label(character.Name);
        nameLabel.name = "character";
        nameLabel.AddToClassList("character-name");
        container.Add(nameLabel);

        VisualElement barsPanel = new VisualElement();
        barsPanel.name = "containerBars";
        barsPanel.AddToClassList("bars-panel");
        barsPanel.Add(CreateBar("vitality", "barVitality", character.Vitalidade, "vitality-fill"));
        barsPanel.Add(CreateBar("sanity", "barSanity", character.Sanidade, "sanity-fill"));
        container.Add(barsPanel);

        return container;
    }

    // Cria uma barra com numero entre 0 e 100.
    // fillClass decide a cor pelo USS:
    // vitality-fill = vermelho
    // sanity-fill = azul
    private static VisualElement CreateBar(string groupName, string valueName, int value, string fillClass)
    {
        VisualElement group = new VisualElement();
        group.name = groupName;
        group.AddToClassList("bar-group");

        Label valueElement = new Label(value.ToString());
        valueElement.name = valueName;
        valueElement.AddToClassList("bar-value");
        group.Add(valueElement);

        VisualElement frame = new VisualElement();
        frame.AddToClassList("bar-frame");

        VisualElement fill = new VisualElement();
        fill.AddToClassList("bar-fill");
        fill.AddToClassList(fillClass);
        fill.style.width = Length.Percent(value);
        frame.Add(fill);

        group.Add(frame);
        return group;
    }
}
