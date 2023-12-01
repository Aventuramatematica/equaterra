using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private bool dialogueActive = false;  // vari�vel para rastrear o estado do di�logo

    public bool IsDialogueActive()
    {
        return dialogueActive;
    }


    public void Speech(Sprite p, string[] txt, string actorName)
    {
        dialogueActive = true;  
        dialogueObj.SetActive(true);
        profile.sprite = p;
        sentences = txt;
        actorNameText.text = actorName;
        StartCoroutine(TypeSentence());
    }

    IEnumerator TypeSentence()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        if (speechText.text == sentences[index])
        {
            // Ainda tem texto dentro do array
            if (index < sentences.Length - 1)
            {
                index++;
                speechText.text = "";
                StartCoroutine(TypeSentence());
            }
            else // Quando acabar os textos
            {
                speechText.text = "";
                index = 0;
                dialogueObj.SetActive(false);
                dialogueActive = false;  // Define o di�logo como inativo
            }
        }
    }
}
