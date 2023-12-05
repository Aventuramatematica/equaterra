using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerNPC : MonoBehaviour
{
    public Text textoPergunta;
    public Text textoTimer;
    public Text textoVidaJogador;
    public Text textoVidaVilao;

    public SimpleObjectPool answerButtonObjectPool;
    public Transform answerButtonParent;
    public GameObject painelBatalhaNPC;

    private DataControllerNPC dataController;
    private RoundDataNPC rodadaAtual;
    private QuestionDataNPC[] questionPool;
    public PlayerHealthController playerHealthController;
    public DialogueCapangas dialogueCapangas;

    private bool rodadaAtiva;
    private float tempoRestante;
    private int questionIndex;

    public int vidaDoJogadorMaxima = 0;
    public int vidaDoVilaoMaxima = 0;

    private int vidaDoJogador;
    private int vidaDoVilao;

    public Slider sliderVidaJogador;
    public Slider sliderVidaVilao;
    public Image profileNPC;
    public Text nameNPC;

    List<int> usedValues = new List<int>();

    private List<GameObject> answerButtonGameObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        dataController = FindObjectOfType<DataControllerNPC>();

        if (dataController == null)
        {
            Debug.LogError("DataController não encontrado.");
            return;
        }

        rodadaAtual = dataController.GetCurrentRoundData();

        if (rodadaAtual == null)
        {
            Debug.LogError("GetCurrentRoundData retornou nulo. Verifique a implementação em DataController.");
            return;
        }

        playerHealthController = FindObjectOfType<PlayerHealthController>();

        if (playerHealthController == null)
        {
            Debug.LogError("PlayerHealthController não encontrado.");
            return;
        }

        questionPool = rodadaAtual.perguntas;
        tempoRestante = rodadaAtual.limiteDeTempo;

        UpdateTimer();

        questionIndex = 0;
        rodadaAtiva = true;

        playerHealthController = FindObjectOfType<PlayerHealthController>();
        int vidaMaximaAtual = playerHealthController.vidaMaximaAtual;

        vidaDoJogadorMaxima = vidaMaximaAtual;

        vidaDoJogador = vidaDoJogadorMaxima;

        int vidaMaximaAtualNPC = 3;

        vidaDoVilaoMaxima = vidaMaximaAtualNPC;

        vidaDoVilao = vidaDoVilaoMaxima;

        AtualizarUIVida();
    }

    //bla bla

    // Update is called once per frame
    void Update()
    {
        if (rodadaAtiva)
        {
            tempoRestante -= Time.deltaTime;
            UpdateTimer();
            if (tempoRestante <= 0)
            {
                Debug.Log("Tempo esgotado! Chamar EndRound()");
                EndRound();
            }
        }
    }

    private void UpdateTimer()
    {
        textoTimer.text = "Timer: " + Mathf.Round(tempoRestante).ToString();

    }

    public void IniciarBatalhaNPC(Sprite profileSprite, string nomeNPC)
    {
        // Atualizar o perfil e nome do NPC
        profileNPC.sprite = profileSprite;
        nameNPC.text = nomeNPC;

        // Verificar se textoPergunta está atribuído antes de chamar ShowQuestion
        if (textoPergunta != null)
        {
            // Iniciar a lógica da batalha NPC aqui, se necessário
            // ...

            // Ativar o painel de vitória se a vida do vilão acabou
            vidaDoVilao = 10;

            // Adicionar 1 à vida máxima do jogador
            vidaDoJogador = playerHealthController.AumentarVidaMaxima(0);


            // Mostrar a primeira pergunta
            ShowQuestion();
        }
    }

    private void ShowQuestion()
    {
        RemoveAnswerButtons();

        int random = Random.Range(0, questionPool.Length);
        while (usedValues.Contains(random))
        {
            random = Random.Range(0, questionPool.Length);
        }

        int random1 = random;
        QuestionDataNPC questionData = questionPool[random1];
        usedValues.Add(random);
        Debug.Log("Mostrando pergunta: " + questionData.textoDaPergunta);
        textoPergunta.text = questionData.textoDaPergunta;

        for (int i = 0; i < questionData.respostas.Length; i++)
        {
            GameObject answerButtonGameObject = answerButtonObjectPool.GetObject();

            answerButtonGameObject.transform.SetParent(answerButtonParent);

            answerButtonGameObjects.Add(answerButtonGameObject);

            AnswerButtonNPC answerButton = answerButtonGameObject.GetComponent<AnswerButtonNPC>();
            answerButton.Setup(questionData.respostas[i]);
        }
    }
    private void RemoveAnswerButtons()
    {
        while (answerButtonGameObjects.Count > 0)
        {
            answerButtonObjectPool.ReturnObject(answerButtonGameObjects[0]);
            answerButtonGameObjects.RemoveAt(0);
        }
    }

    public void AnswerButtonClicked(bool estaCorreto)
    {
        // Verificar se o jogo já terminou
        if (!rodadaAtiva)
        {
            return;
        }

        if (!estaCorreto)
        {
            // Reduzir a vida do jogador
            vidaDoJogador--;
            Debug.Log($"Vida Jogador: {vidaDoJogador}/{vidaDoJogadorMaxima}");
            AtualizarUIVida();

            // Se a vida do jogador atingir zero, chame EndRound
            if (vidaDoJogador <= 0)
            {
                EndRound();
                return;
            }
        }

        // Reduzir a vida do vilão se a resposta estiver correta
        if (estaCorreto)
        {
            vidaDoVilao--;
            Debug.Log($"Vida Vilão: {vidaDoVilao}/{vidaDoVilaoMaxima}");
            AtualizarUIVida();

            // Se a vida do vilão atingir zero, chame EndRound
            if (vidaDoVilao <= 0)
            {
                EndRound();
                return;
            }
        }

        // Se não houver mais perguntas, chame EndRound
        if (questionPool.Length > questionIndex + 1)
        {
            questionIndex++;
            ShowQuestion();
        }
        else
        {
            EndRound();
        }
    }

    private void AtualizarUIVida()
    {
        Debug.Log($"Vida Jogador na UI: {vidaDoJogador}/{vidaDoJogadorMaxima}");
        Debug.Log($"Vida Vilão na UI: {vidaDoVilao}/{vidaDoVilaoMaxima}");
        // Atualize a barra de vida do jogador com base na porcentagem de vida atual.
        float porcentagemVidaJogador = (float)vidaDoJogador / vidaDoJogadorMaxima;
        sliderVidaJogador.value = porcentagemVidaJogador;

        // Atualize a barra de vida do vilão com base na porcentagem de vida atual.
        float porcentagemVidaVilao = (float)vidaDoVilao / vidaDoVilaoMaxima;
        sliderVidaVilao.value = porcentagemVidaVilao;

        textoVidaJogador.text = $"{vidaDoJogador}";
        textoVidaVilao.text = $"{vidaDoVilao}";
    }

    public void EndRound()
    {
        rodadaAtiva = false;

        if (vidaDoVilao <= 0)
        {
            // Ativar o painel de vitória se a vida do vilão acabou
            vidaDoVilao = 10;

            // Adicionar 1 à vida máxima do jogador
            vidaDoJogador = playerHealthController.AumentarVidaMaxima(1);

            // Desativar o objeto do NPC se a vida do NPC atingir zero
            painelBatalhaNPC.SetActive(false);
        }
        else if (vidaDoJogador <= 0)
        {
            // Subtrair 1 da vida máxima do jogador
            vidaDoJogador = playerHealthController.AumentarVidaMaxima(-1);

            // Resetar a vida do vilão para 3
            vidaDoVilao = 10;

            // Atualizar a barra de vida do vilão
            AtualizarUIVida();

            // Desativar o objeto do NPC se a vida do jogador atingir zero
            painelBatalhaNPC.SetActive(false);
        }

        // Resetar variáveis para reiniciar a rodada
        questionIndex = 0;
        usedValues.Clear();
        rodadaAtiva = true;
        tempoRestante = rodadaAtual.limiteDeTempo;

        // Reiniciar a vida do jogador e do vilão
        vidaDoJogador = vidaDoJogadorMaxima;
        vidaDoVilao = vidaDoVilaoMaxima;

        // Atualizar a barra de vida do jogador e do vilão
        AtualizarUIVida();

        // Reiniciar a rodada mostrando a primeira pergunta
        ShowQuestion();
    }


}
