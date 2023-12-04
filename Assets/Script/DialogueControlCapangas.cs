using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControlCapangas : MonoBehaviour
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
    private bool dialogueActive = false;  // vari�vel para rastrear o estado do di�logo

    public Canvas batalhaCanvas;  // Adicione este campo para a refer�ncia do Canvas

    private NPCMovement npcMovement;

    public bool IsDialogueActiveCapangas()
    {
        return dialogueActive;
    }

    public void SpeechCapangas(Sprite p, string[] txt, string actorName, NPCMovement npc)
    {
        dialogueActive = true;
        index = 0;  // Inicializa o �ndice para come�ar do in�cio
        dialogueObj.SetActive(true);
        profile.sprite = p;
        sentences = txt;
        actorNameText.text = actorName;
        StartCoroutine(TypeSentenceCampangas());
        npcMovement = npc;
    }

    IEnumerator TypeSentenceCampangas()
    {
        speechText.text = "";  // Limpa o texto antes de come�ar a digitar
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
            // Ainda tem texto dentro do array
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
                dialogueActive = false;  // Define o di�logo como inativo
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
        // Ativar o canvas "BatalhaNPC" em vez de carregar uma cena
        if (batalhaCanvas != null)
        {
            batalhaCanvas.gameObject.SetActive(true);
        }

        if (npcMovement != null)
        {
            npcMovement.RetomarNPC();
        }

        // Obter refer�ncia para GameControllerNPC
        GameControllerNPC gameControllerNPC = FindObjectOfType<GameControllerNPC>();

        // Verificar se GameControllerNPC foi encontrado
        if (gameControllerNPC != null)
        {
            // Chamar o m�todo IniciarBatalhaNPC e passar os par�metros necess�rios
            gameControllerNPC.IniciarBatalhaNPC(profile.sprite, actorNameText.text);
        }
    }
}
