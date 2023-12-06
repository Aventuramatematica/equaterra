// PlayerHealthController.cs
using UnityEngine;
using UnityEngine.UI;

public class NPCHealthController : MonoBehaviour
{
    // Tornando vidaMaximaAtual pública para acessá-la de fora
    public int vidaMaximaAtualNPC { get; private set; }
    private int teste;

    public Text textoVidaVilao;

    void Start()
    {
        // Inicializa a vida máxima do jogador
        vidaMaximaAtualNPC = 7; // Valor padrão

        // Chama o método para configurar a vidaMaximaAtual com base no rodadaIndex
        ConfigurarVidaMaximaAtual();

        // Atualiza o texto da vida do jogador
        AtualizarUITextoVida();
    }

    // Método para atualizar o texto da vida do jogador
    private void AtualizarUITextoVida()
    {
        textoVidaVilao.text = $"{vidaMaximaAtualNPC}";
    }

    public int ConfigurarVidaMaximaAtual()
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
                // Adicione mais casos conforme necessário
                _ => vidaMaximaAtualNPC // Valor padrão se rodadaIndex não corresponder a nenhum caso
            };

            // Atualiza o texto da vida do NPC
            AtualizarUITextoVida();

            // Remove a verificação desnecessária
            teste = vidaMaximaAtualNPC;

            return vidaMaximaAtualNPC;
        }
        else
        {
            // Trate o caso em que dataController é null, retorne um valor padrão ou lide com isso de acordo com sua lógica
            Debug.LogError("DataController não encontrado!");
            return 0; // Ou outro valor padrão, dependendo da lógica do seu jogo
        }
    }

    public void DefinirVidaMaximaAtual(int novaVidaMaxima)
    {
        vidaMaximaAtualNPC = novaVidaMaxima;
        AtualizarUITextoVida();
    }

    // Método para receber dano e atualizar a vida
    public void ReceberDano(int quantidadeDano)
    {
        vidaMaximaAtualNPC -= quantidadeDano;

        // Garante que a vida não seja negativa
        vidaMaximaAtualNPC = Mathf.Max(vidaMaximaAtualNPC, 0);

        // Atualiza o texto da vida do jogador
        AtualizarUITextoVida();

        // Verifica se a vida do jogador atingiu zero
        if (vidaMaximaAtualNPC <= 0)
        {
            // Adicione aqui qualquer lógica adicional quando a vida do jogador atingir zero
            Debug.Log("Vida do jogador atingiu zero!");
        }
    }

    // Método para aumentar a vida máxima do jogador
    public void AumentarVidaMaxima(int quantidadeAumento)
    {
        vidaMaximaAtualNPC += quantidadeAumento;

        // Atualiza o texto da vida do jogador
        AtualizarUITextoVida();
    }
}
