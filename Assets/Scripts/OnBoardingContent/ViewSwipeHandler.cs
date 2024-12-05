using UnityEngine;
using UnityEngine.EventSystems;

public class ViewSwipeHandler : MonoBehaviour , IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private SwipeOnBoarding _swipeOnBoarding;
    
    
    // public SwipeHandler swipeHandler;
    [SerializeField] private RectTransform _content;
    private Vector2 _startDragPosition;
    private bool _isDragging;
    private Vector3 _initialPosition;
    [SerializeField] private float _dragThreshold = 50f;
    private Vector3 _targetPosition;
    
    private void Awake()
    {
        _initialPosition = _content.localPosition;
        _targetPosition = _initialPosition;
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        _startDragPosition = eventData.position;
        _isDragging = true;
        Debug.Log("OnBeginDrag");
        // swipeHandler.OnBeginDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_isDragging)
        {
            Debug.Log("_isDragging");
            Vector2 currentDragPosition = eventData.position;
            float deltaX = currentDragPosition.x - _startDragPosition.x;
            Vector3 newPosition = _initialPosition + new Vector3(deltaX, 0, 0);
            _content.localPosition = newPosition;
            // swipeHandler.OnDrag(eventData);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        
        _isDragging = false;
        Vector2 endDragPosition = eventData.position;
        float swipeDistance = endDragPosition.x - _startDragPosition.x;

        Debug.Log(swipeDistance);
        
        if (Mathf.Abs(swipeDistance) > _dragThreshold)
        {
            if (swipeDistance > 0)
            {
                // PreviousPage();
                Debug.Log("PreviousPage");
                
                _swipeOnBoarding.Prev();
                // _initialPosition = _swipeOnBoarding.GetPosition();
                
            }
            else
            {
                _swipeOnBoarding.Next();
                // NextPage();
                // _initialPosition = _swipeOnBoarding.GetPosition();
                Debug.Log("NextPage");
            }
        }
        else
        {
            _swipeOnBoarding.MovePages();
            // Snap back to the current page if the swipe distance is not enough
            // MoveToTargetPosition();
        }
        // swipeHandler.OnEndDrag(eventData);
    }

    public void SetInitialPosition(Vector3 position)
    {
        _initialPosition = position;
    }
}
