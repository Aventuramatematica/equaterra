using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public GameObject dialogueBox; // Arraste o objeto do diálogo para este campo no Inspector.
    public KeyCode interactKey = KeyCode.E; // Escolha a tecla desejada.

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(interactKey))
            {
                // Mostrar o diálogo quando o jogador pressionar a tecla de interação.
                dialogueBox.SetActive(true);
            }
        }
    }
}