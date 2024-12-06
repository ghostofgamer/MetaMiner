using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeBoard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Board _board;
    [SerializeField] private float _dragThreshold = 50f;
    [SerializeField] private RectTransform _boardObject;
    [SerializeField] private List<GameObject> _screens;
    
    private Vector2 _startDragPosition;
    private bool _isDragging;
    private Vector3 _initialPosition;
    private Vector3 _targetPosition;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        _startDragPosition = eventData.position;
        _isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_isDragging)
        {
            Vector2 currentDragPosition = eventData.position;
            float deltaX = currentDragPosition.x - _startDragPosition.x;
            GameObject page = _board.GetScreen();
            Vector3 newPosition = page.transform.localPosition + new Vector3(deltaX, 0, 0);
            page.transform.localPosition = newPosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _isDragging = false;
        Vector2 endDragPosition = eventData.position;
        float swipeDistance = endDragPosition.x - _startDragPosition.x;
        
        if (Mathf.Abs(swipeDistance) > _dragThreshold)
        {
            if (swipeDistance > 0)
                _board.Prev();
            else
                _board.Next();
        }
        else
        {
            _board.MovePagesDefaultPos();
        }
    }
}
