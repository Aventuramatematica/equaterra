using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerNPC : MonoBehaviour
{
    public Text textoPergunta;
    public Text textoTimer;
    public Text textoVidaJogador;

    public SimpleObjectPool answerButtonObjectPool;
    public Transform answerButtonParent;
    public GameObject painelBatalhaNPC;

    public int vidaDoJogadorMaxima = 0;

    private int vidaDoJogador;
    private int questionIndex;

    public Slider sliderVidaJogador;
    public Image profileNPC;
    public Text nameNPC;

    private List<GameObject> answerButtonGameObjects = new List<GameObject>();
    private RoundData rodadaAtual;
    private QuestionData[] questionPool;

    // Start is called before the first frame update
    void Start()
    {
        // Configura vidaDoJogadorMaxima com base no rodadaIndex (como no script original)
        ConfigurarVidaDoJogadorMaxima();

        // Adquira a rodada atual e as perguntas
        rodadaAtual = FindObjectOfType<DataController>().GetCurrentRoundData();
        questionPool = rodadaAtual.perguntas;

        // Inicializa��es adicionais...

        // Atualizar a interface gr�fica de vida
        AtualizarUIVida();

        // Mostrar a primeira pergunta
        ShowQuestion();
    }

    // Update is called once per frame
    void Update()
    {
        // Adicione l�gica de temporizador ou outros eventos conforme necess�rio...
    }

    public void IniciarBatalhaNPC(Sprite profileSprite, string nomeNPC)
    {
        // Atualizar o perfil e nome do NPC
        profileNPC.sprite = profileSprite;
        nameNPC.text = nomeNPC;

        // Verificar se textoPergunta est� atribu�do antes de chamar ShowQuestion
        if (textoPergunta != null)
        {
            // Iniciar a l�gica da batalha NPC aqui, se necess�rio
            // ...

            // Mostrar a primeira pergunta
            ShowQuestion();
        }
    }

    private void ShowQuestion()
    {
        RemoveAnswerButtons();

        // Verifica se h� perguntas dispon�veis
        if (questionPool.Length > 0)
        {
            // Seleciona aleatoriamente uma pergunta n�o usada ainda
            int randomIndex = Random.Range(0, questionPool.Length);

            // Mostra a pergunta selecionada
            QuestionData questionData = questionPool[randomIndex];
            textoPergunta.text = questionData.textoDaPergunta;

            // Adiciona os bot�es de resposta
            for (int i = 0; i < questionData.respostas.Length; i++)
            {
                GameObject answerButtonGameObject = answerButtonObjectPool.GetObject();
                answerButtonGameObject.transform.SetParent(answerButtonParent);
                answerButtonGameObjects.Add(answerButtonGameObject);

                AnswerButton answerButton = answerButtonGameObject.GetComponent<AnswerButton>();
                answerButton.Setup(questionData.respostas[i]);
            }

            // Remove a pergunta usada do pool
            List<QuestionData> remainingQuestions = new List<QuestionData>(questionPool);
            remainingQuestions.RemoveAt(randomIndex);
            questionPool = remainingQuestions.ToArray();
        }
        else
        {
            Debug.LogWarning("Nenhuma pergunta dispon�vel.");
        }
    }


    public void AnswerButtonClicked(bool estaCorreto)
    {
        // Adicione l�gica de resposta do NPC conforme necess�rio...
        // ...

        // Mostrar a pr�xima pergunta
        if (questionIndex < questionPool.Length)
        {
            questionIndex++;
            ShowQuestion();
        }
        else
        {
            // Desativar o canvas de batalha NPC
            painelBatalhaNPC.SetActive(false);
        }
    }

    private void AtualizarUIVida()
    {
        // Atualize a barra de vida do jogador com base na porcentagem de vida atual.
        float porcentagemVidaJogador = (float)vidaDoJogador / vidaDoJogadorMaxima;
        sliderVidaJogador.value = porcentagemVidaJogador;

        textoVidaJogador.text = $"{vidaDoJogador}";
    }

    private void ConfigurarVidaDoJogadorMaxima()
    {
        // Configura vidaDoJogadorMaxima com base no rodadaIndex (como no script original)
        // ...
    }

    private void RemoveAnswerButtons()
    {
        while (answerButtonGameObjects.Count > 0)
        {
            answerButtonObjectPool.ReturnObject(answerButtonGameObjects[0]);
            answerButtonGameObjects.RemoveAt(0);
        }
    }
}
