using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private AudioClip _onClickSound;

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.Instance.PlayButtonPressed(_onClickSound);
    }
}
