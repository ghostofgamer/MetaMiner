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
        Debug.Log("OnBeginDrag");
        _startDragPosition = eventData.position;
        _isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag");
        
        if (_isDragging)
        {
            Debug.Log("_isDragging");
            Vector2 currentDragPosition = eventData.position;
            float deltaX = currentDragPosition.x - _startDragPosition.x;
            
            /*Vector3 newPosition = _initialPosition + new Vector3(deltaX, 0, 0);
            _boardObject.localPosition = newPosition;*/
            
            
            
            
            
            // swipeHandler.OnDrag(eventData);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Debug.Log("OnEndDrag");
        
        _isDragging = false;
        Vector2 endDragPosition = eventData.position;
        float swipeDistance = endDragPosition.x - _startDragPosition.x;

        // Debug.Log(swipeDistance);
        
        if (Mathf.Abs(swipeDistance) > _dragThreshold)
        {
            if (swipeDistance > 0)
            {
                _board.Prev();
                Debug.Log("PreviousPage");
                // _swipeOnBoarding.Prev();
            }
            else
            {
                _board.Next();
                // _swipeOnBoarding.Next();
                Debug.Log("NextPage");
            }
        }
        else
        {
            // _swipeOnBoarding.MovePages();
        }
    }
}
