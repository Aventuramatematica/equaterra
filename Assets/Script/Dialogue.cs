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

    private DialogueControl dc;
    private NPCMovement npcMovement;
    bool onRadious;

    private void Start()
    {
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
