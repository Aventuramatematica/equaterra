// PlayerHealthController.cs
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    // Tornando vidaMaximaAtual p�blica para acess�-la de fora
    public int vidaMaximaAtual { get; private set; }

    public Text textoVidaJogador;

    void Start()
    {
        // Inicializa a vida m�xima do jogador
        vidaMaximaAtual = 3; // Valor padr�o

        // Chama o m�todo para configurar a vidaMaximaAtual com base no rodadaIndex
        ConfigurarVidaMaximaAtual();

        // Atualiza o texto da vida do jogador
        AtualizarUITextoVida();
    }

    // M�todo para atualizar o texto da vida do jogador
    private void AtualizarUITextoVida()
    {
        textoVidaJogador.text = $"{vidaMaximaAtual}";
    }

    public void ConfigurarVidaMaximaAtual()
    {
        DataController dataController = FindObjectOfType<DataController>();

        if (dataController != null)
        {
            // Atualiza vidaMaximaAtual com base no valor de rodadaIndex em DataController
            vidaMaximaAtual = dataController.rodadaIndex switch
            {
                0 => 3,
                1 => 2,
                2 => 1,
                3 => 1,
                // Adicione mais casos conforme necess�rio
                _ => vidaMaximaAtual // Valor padr�o se rodadaIndex n�o corresponder a nenhum caso
            };

            // Atualiza o texto da vida do jogador
            AtualizarUITextoVida();
        }
    }

    public void DefinirVidaMaximaAtual(int novaVidaMaxima)
    {
        vidaMaximaAtual = novaVidaMaxima;
        AtualizarUITextoVida();
    }

    // M�todo para receber dano e atualizar a vida
    public void ReceberDano(int quantidadeDano)
    {
        vidaMaximaAtual -= quantidadeDano;

        // Garante que a vida n�o seja negativa
        vidaMaximaAtual = Mathf.Max(vidaMaximaAtual, 0);

        // Atualiza o texto da vida do jogador
        AtualizarUITextoVida();

        // Verifica se a vida do jogador atingiu zero
        if (vidaMaximaAtual <= 0)
        {
            // Adicione aqui qualquer l�gica adicional quando a vida do jogador atingir zero
            Debug.Log("Vida do jogador atingiu zero!");
        }
    }

    // M�todo para aumentar a vida m�xima do jogador
    public int AumentarVidaMaxima(int quantidadeAumento)
    {
        vidaMaximaAtual += quantidadeAumento;

        // Garante que a vida m�xima n�o seja negativa
        vidaMaximaAtual = Mathf.Max(vidaMaximaAtual, 0);

        // Atualiza o texto da vida do jogador
        AtualizarUITextoVida();

        return vidaMaximaAtual;
    }

}
