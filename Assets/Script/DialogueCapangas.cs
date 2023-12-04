using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCapangas : MonoBehaviour
{
    public Sprite profile;
    private List<string> speechTxt = new List<string>(); // Alterado para List<string>
    public string actorName;

    public LayerMask playerLayer;
    public float radious;

    private DialogueControlCapangas dc;
    private PlayerController playerController;
    private NPCMovement npcMovement;
    private string[] frases = new string[]
    {
    "Voc�, Numeralis, o estudante, � um zero incapaz de decifrar a complexidade do nosso plano.",
    "Voc�, o 'her�i' Numeralis, � uma inc�gnita que acha poder resolver nossas equa��es.",
    "Voc�, Numeralis, o tolo que acredita que a Matem�tica pode salv�-lo do inevit�vel.",
    "Sua lealdade, Numeralis, � uma constante, mas nossa trai��o ser� a vari�vel que o destruir�.",
    "Na equa��o da vida, voc�, Numeralis, � apenas um termo que podemos eliminar.",
    "Voc�, Numeralis, o n�mero zero em nosso jogo, est� prestes a ser subtra�do da exist�ncia.",
    "Sua fidelidade, Numeralis, s� adiciona � nossa certeza de sua derrota iminente.",
    "Voc�, Numeralis, o ing�nuo, n�o percebe que a resposta final sempre ser� a nossa vit�ria.",
    "Cada passo que voc�, Numeralis, d� � uma multiplica��o de seus pr�prios erros.",
    "Em um mundo de inc�gnitas, voc�, Numeralis, � a vari�vel que est� destinada a falhar.",
    "N�s somos a solu��o para a equa��o da sua destrui��o, e voc�, Numeralis, � o problema.",
    "Voc�, Numeralis, como um n�mero negativo, s� subtrai da sua pr�pria exist�ncia.",
    "A diferen�a entre voc�, Numeralis, e n�s? Voc� � uma fra��o, enquanto somos uma equa��o completa.",
    "Voc�, Numeralis, t�o previs�vel quanto uma adi��o simples em nossos planos.",
    "Aqueles que confiam em voc�, Numeralis, s�o como n�meros primos, isolados e indefesos.",
    "Voc�, Numeralis, uma raiz quadrada que n�o tem a capacidade de compreender o verdadeiro significado do caos.",
    "A equa��o da sua derrota tem voc�, Numeralis, como um fator constante e inevit�vel.",
    "Como uma fra��o, voc�, Numeralis, � apenas uma parte insignificante de nossa grande conquista.",
    "Na soma de nossos esfor�os, a submiss�o de voc�, Numeralis, � o resultado final.",
    "Cada a��o sua, Numeralis, � um incremento em nossa marcha triunfante em dire��o � sua aniquila��o.",
    "Minha m�o vai dar umas beijocas na sua cara tampinha!"
    };
    bool onRadious;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        // Cria uma inst�ncia da classe Random fora do m�todo Start
        System.Random random = new System.Random();

        // Gera um n�mero aleat�rio entre 0 e o comprimento do array frases
        int numeroSorteado = random.Next(0, frases.Length);

        // Adiciona a frase aleat�ria ao array speechTxt
        speechTxt.Add(frases[numeroSorteado]);

        dc = FindObjectOfType<DialogueControlCapangas>();
        npcMovement = GetComponent<NPCMovement>(); // Agora pegamos o componente no mesmo objeto do script
    }

    private void FixedUpdate()
    {
        Interact();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && onRadious)
        {

            

            if (!dc.IsDialogueActiveCapangas())
            {
                dc.SpeechCapangas(profile, speechTxt.ToArray(), actorName, npcMovement);

                if (npcMovement != null)
                {
                    npcMovement.PararNPC();
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

    public void Interact()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radious, playerLayer);

        if (hit != null)
        {
            onRadious = true;
        }
        else
        {
            onRadious = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radious);
    }
}
