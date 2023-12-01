using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText;

    public void StartDialogue(string dialogue)
    {
        dialogueText.text = dialogue;
    }

    public void EndDialogue()
    {
        // Esconder o diálogo quando o jogador terminar de ler.
        gameObject.SetActive(false);
    }
}
