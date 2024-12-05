using System;
using System.Collections.Generic;
using DG.Tweening;
using ThisOtherThing.UI.Shapes;
using UnityEngine;

public class SwipeOnBoarding : MonoBehaviour
{
    [SerializeField] private GameObject _pointsObJectContent;
    [SerializeField] private List<GameObject> _descriptions;
    [SerializeField] private GameObject[] _buttons;

    [SerializeField] private ViewSwipeHandler _viewSwipeHandler;

    [SerializeField] private int _maxPage;
    [SerializeField] private Vector3 _pageStep;
    [SerializeField] private RectTransform _levelPagesRect;

    [SerializeField] private float _tweenTime;

    // [SerializeField] private Rectangle[] _points;
    [SerializeField] private PointChanger[] _points;

    private int _currentPage;
    private Vector3 _targetPos;

    private int _startPage = 0;

    private void Awake()
    {
        _startPage = 0;
        ChangePoints(_startPage);
        _currentPage = 1;
        Show(_currentPage);
        _targetPos = _levelPagesRect.localPosition;

        if (_currentPage == _maxPage)
        {
            _buttons[1].SetActive(true);
            _buttons[0].SetActive(false);
        }
        else
        {
            _buttons[1].SetActive(false);
            _buttons[0].SetActive(true);
        }
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
            Show(_currentPage);
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
            Show(_currentPage);
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
        if (_currentPage == _maxPage)
        {
            _buttons[1].SetActive(true);
            _buttons[0].SetActive(false);
        }
        else
        {
            _buttons[1].SetActive(false);
            _buttons[0].SetActive(true);
        }


        _levelPagesRect.DOLocalMove(_targetPos, _tweenTime).SetEase(Ease.OutCubic);
    }

    private void ChangePoints(int index)
    {
        Debug.Log("Индекс  " + index);
        if (index == 0)
        {
            _pointsObJectContent.SetActive(false);
        }

        if (index > 0)
        {
            _pointsObJectContent.SetActive(true);
            for (int i = 0; i < _points.Length; i++)
            {
                _points[i].OffPoint();
                // _points[i].ShapeProperties.DrawFill = false;
            }

            _points[index - 1].OnPoint();
        }


        /*for (int i = 0; i < _points.Length; i++)
        {
            _points[i].OffPoint();
            // _points[i].ShapeProperties.DrawFill = false;
        }
        _points[index].OnPoint();*/
    }

    public void Show(int indexDescription)
    {
        /*foreach (var description in _descriptions)
            description.SetActive(false);

        _descriptions[indexDescription-1].SetActive(true);*/
    }
}