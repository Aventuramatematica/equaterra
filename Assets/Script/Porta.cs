using UnityEngine;
using UnityEngine.SceneManagement;

public class Porta : MonoBehaviour
{
    // Nome da cena para a qual você deseja transicionar
    public string nomeDaCena;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto que colidiu tem a tag "Player"
        if (other.CompareTag("Player"))
        {
            // Transiciona para a cena desejada
            SceneManager.LoadScene(nomeDaCena);
        }
    }
}

