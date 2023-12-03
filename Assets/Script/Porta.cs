using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Porta : MonoBehaviour
{
    [SerializeField]
    private string nomeProximaFase;

    // Certifique-se de que o collider seja um "Is Trigger"
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificar se a colis�o foi com o jogador (com base na camada)
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            IrProxFase();
            Debug.Log("Chamando pr�xima fase");
        }
    }

    private void IrProxFase()
    {
        // Verificar se a cena que est� sendo carregada � diferente da cena atual
        if (SceneManager.GetActiveScene().name != this.nomeProximaFase)
        {
            SceneManager.LoadScene(this.nomeProximaFase);
        }
    }
}