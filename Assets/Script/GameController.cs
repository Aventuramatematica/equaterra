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
    public Text textoHighScore;

    public SimpleObjectPool answerButtonObjectPool;
    public Transform answerButtonParent;
    public GameObject painelBatalha;
    public GameObject painelFimRodada;

    private DataController dataController;
    private RoundData rodadaAtual;
    private QuestionData[] questionPool;

    private bool rodadaAtiva;
    private float tempoRestante;
    private int questionIndex;
    private int playerScore;

    public int vidaDoJogadorMaxima = 3;
    public int vidaDoVilaoMaxima = 3;

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

        vidaDoJogador = vidaDoJogadorMaxima;
        vidaDoVilao = vidaDoVilaoMaxima;

        AtualizarUIVida();
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
            textoPontos.text = "Score: " + playerScore.ToString();

            // Reduzir a vida do vilão
            vidaDoVilao--;
            Debug.Log($"Vida Vilão: {vidaDoVilao}/{vidaDoVilaoMaxima}");

            // Limitar a vida do vilão para não ficar negativa
            vidaDoVilao = Mathf.Max(vidaDoVilao, 0);

            // Atualizar a interface gráfica de vida
            AtualizarUIVida();

            // Verificar se a vida do vilão atingiu zero
            if (vidaDoVilao <= 0)
            {
                EndRound();
                return;
            }
        }
        else
        {
            // Reduzir a vida do jogador
            vidaDoJogador--;
            Debug.Log($"Vida Jogador: {vidaDoJogador}/{vidaDoJogadorMaxima}");

            // Se a vida do jogador atingir zero, chame EndRound
            if (vidaDoJogador <= 0)
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
    }


    public void EndRound()
    {
        rodadaAtiva = false;

        dataController.EnviarNovoHighScore(playerScore);
        textoHighScore.text = "High Score: " + dataController.GetHighScore().ToString();

        painelBatalha.SetActive(false);
        painelFimRodada.SetActive(true);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("QuizMenu");
    }

    public void fimJogo()
    {
        SceneManager.LoadScene("CutsceneFinal");
    }
}
