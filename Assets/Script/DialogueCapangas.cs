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
    "Você, Numeralis, o estudante, é um zero incapaz de decifrar a complexidade do nosso plano.",
    "Você, o 'herói' Numeralis, é uma incógnita que acha poder resolver nossas equações.",
    "Você, Numeralis, o tolo que acredita que a Matemática pode salvá-lo do inevitável.",
    "Sua lealdade, Numeralis, é uma constante, mas nossa traição será a variável que o destruirá.",
    "Na equação da vida, você, Numeralis, é apenas um termo que podemos eliminar.",
    "Você, Numeralis, o número zero em nosso jogo, está prestes a ser subtraído da existência.",
    "Sua fidelidade, Numeralis, só adiciona à nossa certeza de sua derrota iminente.",
    "Você, Numeralis, o ingênuo, não percebe que a resposta final sempre será a nossa vitória.",
    "Cada passo que você, Numeralis, dá é uma multiplicação de seus próprios erros.",
    "Em um mundo de incógnitas, você, Numeralis, é a variável que está destinada a falhar.",
    "Nós somos a solução para a equação da sua destruição, e você, Numeralis, é o problema.",
    "Você, Numeralis, como um número negativo, só subtrai da sua própria existência.",
    "A diferença entre você, Numeralis, e nós? Você é uma fração, enquanto somos uma equação completa.",
    "Você, Numeralis, tão previsível quanto uma adição simples em nossos planos.",
    "Aqueles que confiam em você, Numeralis, são como números primos, isolados e indefesos.",
    "Você, Numeralis, uma raiz quadrada que não tem a capacidade de compreender o verdadeiro significado do caos.",
    "A equação da sua derrota tem você, Numeralis, como um fator constante e inevitável.",
    "Como uma fração, você, Numeralis, é apenas uma parte insignificante de nossa grande conquista.",
    "Na soma de nossos esforços, a submissão de você, Numeralis, é o resultado final.",
    "Cada ação sua, Numeralis, é um incremento em nossa marcha triunfante em direção à sua aniquilação.",
    "Minha mão vai dar umas beijocas na sua cara tampinha!"
    };
    bool onRadious;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        // Cria uma instância da classe Random fora do método Start
        System.Random random = new System.Random();

        // Gera um número aleatório entre 0 e o comprimento do array frases
        int numeroSorteado = random.Next(0, frases.Length);

        // Adiciona a frase aleatória ao array speechTxt
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
                
                // Certifique-se de que playerController não seja nulo
                if (playerController != null)
                {
                    Debug.Log("Entriy");
                    // Chame a função BlockNum no PlayerController
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
