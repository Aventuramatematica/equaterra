using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.E;
    public float interactRadius = 2f;
    public GameObject panel;

    private bool isPlayerInRange = false;

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(interactKey))
        {
            TogglePanel();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && panel != null && panel.activeSelf)
        {
            ClosePanel();
        }

        isPlayerInRange = false;
    }

    private void TogglePanel()
    {
        if (panel != null)
        {
            panel.SetActive(!panel.activeSelf);
        }
    }

    private void ClosePanel()
    {
        if (panel != null)
        {
            panel.SetActive(false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}
