// PlayerHealthController.cs
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    // Tornando vidaMaximaAtual pública para acessá-la de fora
    public int vidaMaximaAtual { get; private set; }

    public Text textoVidaJogador;

    void Start()
    {
        // Inicializa a vida máxima do jogador
        vidaMaximaAtual = 3; // Valor padrão

        // Chama o método para configurar a vidaMaximaAtual com base no rodadaIndex
        ConfigurarVidaMaximaAtual();

        // Atualiza o texto da vida do jogador
        AtualizarUITextoVida();
    }

    // Método para atualizar o texto da vida do jogador
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
                // Adicione mais casos conforme necessário
                _ => vidaMaximaAtual // Valor padrão se rodadaIndex não corresponder a nenhum caso
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

    // Método para receber dano e atualizar a vida
    public void ReceberDano(int quantidadeDano)
    {
        vidaMaximaAtual -= quantidadeDano;

        // Garante que a vida não seja negativa
        vidaMaximaAtual = Mathf.Max(vidaMaximaAtual, 0);

        // Atualiza o texto da vida do jogador
        AtualizarUITextoVida();

        // Verifica se a vida do jogador atingiu zero
        if (vidaMaximaAtual <= 0)
        {
            // Adicione aqui qualquer lógica adicional quando a vida do jogador atingir zero
            Debug.Log("Vida do jogador atingiu zero!");
        }
    }

    // Método para aumentar a vida máxima do jogador
    public int AumentarVidaMaxima(int quantidadeAumento)
    {
        vidaMaximaAtual += quantidadeAumento;

        // Garante que a vida máxima não seja negativa
        vidaMaximaAtual = Mathf.Max(vidaMaximaAtual, 0);

        // Atualiza o texto da vida do jogador
        AtualizarUITextoVida();

        return vidaMaximaAtual;
    }

}
