using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public GameObject dialogueBox; // Arraste o objeto do di�logo para este campo no Inspector.
    public KeyCode interactKey = KeyCode.E; // Escolha a tecla desejada.

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(interactKey))
            {
                // Mostrar o di�logo quando o jogador pressionar a tecla de intera��o.
                dialogueBox.SetActive(true);
            }
        }
    }
}