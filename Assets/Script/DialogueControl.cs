using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueControl : MonoBehaviour
{
    [Header("Components")]
    public GameObject dialogueObj;
    public Image profile;
    public Text speechText;
    public Text actorNameText;

    [Header("Settings")] 
    public float typingSpeed;
    private string[] sentences;
    private int index;
    private bool dialogueActive = false;  // variável para rastrear o estado do diálogo

    private PlayerController playerController;

    private NPCMovement npcMovement;

    public bool IsDialogueActive()
    {
        return dialogueActive;
    }

    

    public void Speech(Sprite p, string[] txt, string actorName, NPCMovement npc)
    {
        dialogueActive = true;
        index = 0;  // Inicializa o índice para começar do início
        dialogueObj.SetActive(true);
        profile.sprite = p;
        sentences = txt;
        actorNameText.text = actorName;
        StartCoroutine(TypeSentence());
        npcMovement = npc;
    }

    IEnumerator TypeSentence()
    {
        speechText.text = "";  // Limpa o texto antes de começar a digitar
        foreach (char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    public void NextSentence()
    {
        Debug.Log($"{speechText.text}");
        if (speechText.text == sentences[index])
         {
             // Ainda tem texto dentro do rray
             if (index < sentences.Length - 1)
             {
                 Debug.Log("Ainda tem texto dentro do array");
                 index++;
                 speechText.text = "";
                 StartCoroutine(TypeSentence());
             }
             else // Quando acabar os textos
             {
                 Debug.Log("N tem texto dentro do array");
                 speechText.text = "";
                 index = 0;
                 dialogueObj.SetActive(false);
                 dialogueActive = false;  // Define o diálogo como inativo
                 Debug.Log("npcMovement: " + npcMovement);
                 if (npcMovement != null)
                 {
                     npcMovement.RetomarNPC();
                 }

                // Certifique-se de que playerController n�o seja nulo
                if (playerController != null)
                {
                    Debug.Log("Entriy");
                    // Chame a fun��o BlockNum no PlayerController
                    playerController.BlockNum();
                }
               
            }

        }
    }

 
}
