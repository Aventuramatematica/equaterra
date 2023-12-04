using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonAudioController : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public AudioClip hoverSound;
    public AudioClip clickSound;

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Instance.PlayHoverSound(hoverSound);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.Instance.PlayClickSound(clickSound);
    }
}
