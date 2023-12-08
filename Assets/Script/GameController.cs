using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public Text textoPergunta;
    public Text textoPontos;
    public Text textoTimer;
    public Text textoHighScoreDerrota;
    public Text textoHighScoreVitoria;
    public Text textoVidaJogador;
    public Text textoVidaVilao;

    public SimpleObjectPool answerButtonObjectPool;
    public Transform answerButtonParent;
    public GameObject painelBatalha;
    public GameObject painelDerrota;
    public GameObject painelVitoria;

    private DataController dataController;
    private RoundData rodadaAtual;
    private QuestionData[] questionPool;
    public PlayerHealthController playerHealthController;
    public NPCHealthController npcHealthController;

    private bool rodadaAtiva;
    private float tempoRestante;
    private int questionIndex;
    private int playerScore;

    public int vidaDoJogadorMaxima = 0;
    public int vidaDoVilaoMaxima = 0;

    private int vidaDoJogador;
    private int vidaDoVilao;

    public Slider sliderVidaJogador;
    public Slider sliderVidaVilao;

    List<int> usedValues = new List<int>();

    private List<GameObject> answerButtonGameObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        dataController = FindObjectOfType<DataController>();

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

        questionPool = rodadaAtual.perguntas;
        tempoRestante = rodadaAtual.limiteDeTempo;

        UpdateTimer();

        playerScore = 0;
        questionIndex = 0;
        ShowQuestion();
        rodadaAtiva = true;

        playerHealthController = FindObjectOfType<PlayerHealthController>();
        int vidaMaximaAtual = playerHealthController.vidaMaximaAtual;

        vidaDoJogadorMaxima = vidaMaximaAtual;

        vidaDoJogador = vidaDoJogadorMaxima;

        npcHealthController = FindObjectOfType<NPCHealthController>();
        int vidaMaximaAtualNPC = npcHealthController.vidaMaximaAtualNPC;

        vidaDoVilaoMaxima = vidaMaximaAtualNPC;

        vidaDoVilao = vidaDoVilaoMaxima;

        AtualizarUIVida();

        // Configura vidaDoVilaoMaxima com base no rodadaIndex
        ConfigurarVidaDoVilaoMaxima();
    }


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

    private void ShowQuestion()
    {
        RemoveAnswerButtons();

        int random = Random.Range(0, questionPool.Length);
        while (usedValues.Contains(random))
        {
            random = Random.Range(0, questionPool.Length);
        }

        int random1 = random;
        QuestionData questionData = questionPool[random1];
        usedValues.Add(random);
        Debug.Log("Mostrando pergunta: " + questionData.textoDaPergunta);
        textoPergunta.text = questionData.textoDaPergunta;

        for ( int i = 0; i < questionData.respostas.Length; i++)
        {
            GameObject answerButtonGameObject = answerButtonObjectPool.GetObject();

            answerButtonGameObject.transform.SetParent(answerButtonParent);

            answerButtonGameObjects.Add(answerButtonGameObject);

            AnswerButton answerButton = answerButtonGameObject.GetComponent<AnswerButton>();
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

        if (estaCorreto)
        {
            // Aumentar o score
            playerScore += rodadaAtual.pontosPorAcerto;
        }
        else
        {
            // Reduzir o score por erro
            playerScore -= rodadaAtual.pontosPorErro;

            // Certifique-se de que o score não seja negativo
            playerScore = Mathf.Max(playerScore, 0);
        }

        textoPontos.text = "Score: " + playerScore.ToString();

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
            vidaDoVilao --;
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

    private void ConfigurarVidaDoVilaoMaxima()
    {
        switch (dataController.rodadaIndex)
        {
            case 0:
                vidaDoVilaoMaxima = 5;
                break;
            case 1:
                vidaDoVilaoMaxima = 10;
                break;
            case 2:
                vidaDoVilaoMaxima = 15;
                break;
            case 3:
                vidaDoVilaoMaxima = 20;
                break;
                // Adicione mais casos conforme necessário
        }
    }


    public void EndRound()
    {
        rodadaAtiva = false;

        dataController.EnviarNovoHighScore(playerScore);
        textoHighScoreDerrota.text = "High Score: " + dataController.GetHighScore().ToString();
        textoHighScoreVitoria.text = "High Score: " + dataController.GetHighScore().ToString();

        if (vidaDoVilao <= 0)
        {
            // Ativar o painel de vitória se a vida do vilão acabou
            painelVitoria.SetActive(true);
        }
        else
        {
            // Ativar o painel de derrota se a vida do jogador acabou
            painelDerrota.SetActive(true);
        }

        painelBatalha.SetActive(false);
    }


    public void ReturnToMenu()
    {
        PlayerPrefs.SetInt("vidaPlayer", 0);
        SceneManager.LoadScene("Menu");

    }

    public void fimJogo()
    {
        SceneManager.LoadScene("CutsceneFinal");
    }
}
