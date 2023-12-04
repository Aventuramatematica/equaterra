using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public Sprite profile;
    public string[] speechTxt;
    public string actorName;

    public LayerMask playerLayer;
    public float radious;

    private PlayerController playerController;
    private DialogueControl dc;
    private NPCMovement npcMovement;
    bool onRadious;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        dc = FindObjectOfType<DialogueControl>();
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
            if (!dc.IsDialogueActive())
            {
                dc.Speech(profile, speechTxt, actorName, npcMovement);

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
