using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueControlCapangas: MonoBehaviour
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


    private NPCMovement npcMovement;

    public bool IsDialogueActiveCapangas()
    {
        return dialogueActive;
    }



    public void SpeechCapangas(Sprite p, string[] txt, string actorName, NPCMovement npc)
    {
        dialogueActive = true;
        index = 0;  // Inicializa o índice para começar do início
        dialogueObj.SetActive(true);
        profile.sprite = p;
        sentences = txt;
        actorNameText.text = actorName;
        StartCoroutine(TypeSentenceCampangas());
        npcMovement = npc;
    }

    IEnumerator TypeSentenceCampangas()
    {
        speechText.text = "";  // Limpa o texto antes de começar a digitar
        foreach (char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }


    public void NextSentenceCapangas()
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
                StartCoroutine(TypeSentenceCampangas());
            }
            else // Quando acabar os textos
            {
                Debug.Log("N tem texto dentro do array");
                speechText.text = "";
                index = 0;
                dialogueObj.SetActive(false);
                dialogueActive = false;  // Define o diálogo como inativ
                Debug.Log("npcMovement: " + npcMovement);
                if (npcMovement != null)
                {
                    npcMovement.RetomarNPC();
                }
            }
        }
    }

    public void BattleNpc()
    {

        SceneManager.LoadScene("QuizGame");

        if (npcMovement != null)
        {
            npcMovement.RetomarNPC();
        }


    }
}
