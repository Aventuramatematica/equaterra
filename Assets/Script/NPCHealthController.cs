// PlayerHealthController.cs
using UnityEngine;
using UnityEngine.UI;

public class NPCHealthController : MonoBehaviour
{
    // Tornando vidaMaximaAtual p�blica para acess�-la de fora
    public int vidaMaximaAtualNPC { get; private set; }

    public Text textoVidaVilao;

    void Start()
    {
        // Inicializa a vida m�xima do jogador
        vidaMaximaAtualNPC = 3; // Valor padr�o

        // Chama o m�todo para configurar a vidaMaximaAtual com base no rodadaIndex
        ConfigurarVidaMaximaAtual();

        // Atualiza o texto da vida do jogador
        AtualizarUITextoVida();
    }

    // M�todo para atualizar o texto da vida do jogador
    private void AtualizarUITextoVida()
    {
        textoVidaVilao.text = $"{vidaMaximaAtualNPC}";
    }

    public void ConfigurarVidaMaximaAtual()
    {
        DataController dataController = FindObjectOfType<DataController>();

        if (dataController != null)
        {
            // Atualiza vidaMaximaAtual com base no valor de rodadaIndex em DataController
            vidaMaximaAtualNPC = dataController.rodadaIndex switch
            {
                0 => 10,
                1 => 15,
                2 => 20,
                3 => 10,
                // Adicione mais casos conforme necess�rio
                _ => vidaMaximaAtualNPC // Valor padr�o se rodadaIndex n�o corresponder a nenhum caso
            };

            // Atualiza o texto da vida do jogador
            AtualizarUITextoVida();
        }
    }

    public void DefinirVidaMaximaAtual(int novaVidaMaxima)
    {
        vidaMaximaAtualNPC = novaVidaMaxima;
        AtualizarUITextoVida();
    }

    // M�todo para receber dano e atualizar a vida
    public void ReceberDano(int quantidadeDano)
    {
        vidaMaximaAtualNPC -= quantidadeDano;

        // Garante que a vida n�o seja negativa
        vidaMaximaAtualNPC = Mathf.Max(vidaMaximaAtualNPC, 0);

        // Atualiza o texto da vida do jogador
        AtualizarUITextoVida();

        // Verifica se a vida do jogador atingiu zero
        if (vidaMaximaAtualNPC <= 0)
        {
            // Adicione aqui qualquer l�gica adicional quando a vida do jogador atingir zero
            Debug.Log("Vida do jogador atingiu zero!");
        }
    }

    // M�todo para aumentar a vida m�xima do jogador
    public void AumentarVidaMaxima(int quantidadeAumento)
    {
        vidaMaximaAtualNPC += quantidadeAumento;

        // Atualiza o texto da vida do jogador
        AtualizarUITextoVida();
    }
}
