using System;
using DG.Tweening;
using ThisOtherThing.UI.Shapes;
using UnityEngine;

public class SwipeOnBoarding : MonoBehaviour
{
    [SerializeField] private ViewSwipeHandler _viewSwipeHandler;
    
    [SerializeField] private int _maxPage;
    [SerializeField] private Vector3 _pageStep;
    [SerializeField] private RectTransform _levelPagesRect;
    [SerializeField] private float _tweenTime;
    // [SerializeField] private Rectangle[] _points;
    [SerializeField] private PointChanger[] _points;

    private int _currentPage;
    private Vector3 _targetPos;
    
    private int  _startPage=0;

    private void Awake()
    {
        _startPage = 0;
        ChangePoints(_startPage);
        _currentPage = 1;
        _targetPos = _levelPagesRect.localPosition;
    }

    public Vector3 GetPosition()
    {
        return _targetPos;
    }
    
    public void Next()
    {
        if (_currentPage < _maxPage)
        {
            _startPage++;
            _currentPage++;
            ChangePoints(_startPage);
            _targetPos += _pageStep;
            MovePages();
            _viewSwipeHandler.SetInitialPosition(_targetPos);
        }
        else
        {
            MovePages();
        }
    }

    public void Prev()
    {
        if (_currentPage > 1)
        {
            _startPage--;
            _currentPage--;
            ChangePoints(_startPage);
            _targetPos -= _pageStep;
            MovePages();
            _viewSwipeHandler.SetInitialPosition(_targetPos);
        }
        else
        {
            MovePages();
        }
    }

    public void MovePages()
    {
        _levelPagesRect.DOLocalMove(_targetPos, _tweenTime).SetEase(Ease.OutCubic);
    }

    private void ChangePoints(int index)
    {
        Debug.Log("Индекс  " + index);

        for (int i = 0; i < _points.Length; i++)
        {
            _points[i].OffPoint();
            // _points[i].ShapeProperties.DrawFill = false;
        }
        _points[index].OnPoint();
    }
}