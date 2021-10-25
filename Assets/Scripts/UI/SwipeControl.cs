using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeControl : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField] private PlayerMover _playerMover;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Time.timeScale != 0)
        {
            if (Mathf.Abs(eventData.delta.x) < Mathf.Abs(eventData.delta.y))
            {
                if (eventData.delta.y > 0)
                    _playerMover.Jump();
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Time.timeScale != 0)
        {
            if (Mathf.Abs(eventData.delta.x) >= Mathf.Abs(eventData.delta.y))
            {
                _playerMover.Move(eventData.delta.x);
            }
        }
    }
}
