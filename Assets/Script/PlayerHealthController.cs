// PlayerHealthController.cs
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    // Tornando vidaMaximaAtual p�blica para acess�-la de fora
    public int vidaMaximaAtual { get; private set; }

    public Text textoVidaJogador;

    private bool novaVida = false;

    void Start()
    {


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
            // Obt�m o valor salvo de "vidaPlayer" usando PlayerPrefs.GetInt
            int vd = PlayerPrefs.GetInt("vidaPlayer");

            if (vd == 0)
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

                // Salva a vidaMaximaAtual usando PlayerPrefs.SetInt
                PlayerPrefs.SetInt("vidaPlayer", vidaMaximaAtual);
            }
            else
            {
                // Se o valor j� estiver definido, utiliza esse valor
                vidaMaximaAtual = vd;
            }
        }
    }

    public void DefinirVidaMaximaAtual(int novaVidaMaxima)
    {
        vidaMaximaAtual = novaVidaMaxima;
        novaVida = true;
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
