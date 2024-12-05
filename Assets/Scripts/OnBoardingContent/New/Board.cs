using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class Board : MonoBehaviour
{
    [SerializeField] private Transform _exitTransform;
    [SerializeField] private Transform _startTransform;
    [SerializeField] private Transform _centerTransform;

    [SerializeField] private List<GameObject> _screens;

    [SerializeField] private GameObject _pointsObjectContent;
    [SerializeField] private List<GameObject> _descriptions;
    [SerializeField] private GameObject[] _buttons;
    [SerializeField] private int _maxPage;
    [SerializeField] private float _tweenTime;
    [SerializeField] private PointChanger[] _points;

    private int _currentPage;
    public int _page = 0;

    private void Start()
    {
        foreach (var SCREEEE in _screens)
        {
            Debug.Log("POS " + SCREEEE.transform.localPosition);
        }

        _page = 0;
        _currentPage = 1;
        ChangePoints(_page);

        Checkpage();


        /*if (_currentPage == _maxPage)
        {
            _buttons[1].SetActive(true);
            _buttons[0].SetActive(false);
        }
        else
        {
            _buttons[1].SetActive(false);
            _buttons[0].SetActive(true);
        }*/
    }

    public void Next()
    {
        Debug.Log("Prev " + _page);

        if (_page < 5)
        {
            Vector3 targetPos = _screens[_page].transform.localPosition;
            targetPos.x += -5000;
            _screens[_page].GetComponent<RectTransform>().DOLocalMove(targetPos, _tweenTime).SetEase(Ease.OutCubic);

            _page++;

            _currentPage++;
            ChangePoints(_page);

            _screens[_page].SetActive(true);
            _screens[_page].transform.localPosition = new Vector3(5000, 0, 0);
            _screens[_page].GetComponent<RectTransform>().DOLocalMove(new Vector3(0, 0, 0), _tweenTime)
                .SetEase(Ease.OutCubic);

            Checkpage();
        }

        /*if (_currentPage < _maxPage)
        {
            Vector3 targetPos = _screens[_page].transform.localPosition;
            targetPos.x += -5000;
            _screens[_page].GetComponent<RectTransform>().DOLocalMove(targetPos, _tweenTime).SetEase(Ease.OutCubic);


            _page++;
            _currentPage++;
            ChangePoints(_page);

            _screens[_page].SetActive(true);
            _screens[_page].transform.localPosition = new Vector3(5000, 0, 0);
            _screens[_page].GetComponent<RectTransform>().DOLocalMove(new Vector3(0, 0, 0), _tweenTime)
                .SetEase(Ease.OutCubic);

            Checkpage();


            /*Debug.Log( "Local " + _screens[0].transform.localPosition);
            Debug.Log(  "Target " + _exitTransform.position);


            Debug.Log("ЭКРАН " + _screens[0].GetComponent<RectTransform>().transform.localPosition);#1#

            /*Vector3 targetPos = _screens[0].transform.localPosition;
            targetPos.x += -5000;
            _screens[0].GetComponent<RectTransform>().DOLocalMove(targetPos, _tweenTime).SetEase(Ease.OutCubic);#1#

            /*_screens[1].SetActive(true);
            _screens[1].transform.localPosition = new Vector3(5000, 0,0);
            _screens[1].GetComponent<RectTransform>().DOLocalMove(new Vector3(0,0,0), _tweenTime).SetEase(Ease.OutCubic);#1#

            /*
           _targetPos += _pageStep;
           MovePages();
           Show(_currentPage);
           _viewSwipeHandler.SetInitialPosition(_targetPos);#1#
        }
        else
        {
            MovePagesPrev();
        }*/
    }

    public void Prev()
    {
        Debug.Log("Prev " + _page);
        if (_page > 0)
        {
            Vector3 targetPos = _screens[_page].transform.localPosition;
            targetPos.x += 5000;
            _screens[_page].GetComponent<RectTransform>().DOLocalMove(targetPos, _tweenTime).SetEase(Ease.OutCubic);

            _page--;
            _currentPage--;
            ChangePoints(_page);

            _screens[_page].SetActive(true);
            _screens[_page].transform.localPosition = new Vector3(-5000, 0, 0);
            _screens[_page].GetComponent<RectTransform>().DOLocalMove(new Vector3(0, 0, 0), _tweenTime)
                .SetEase(Ease.OutCubic);

            Checkpage();
        }
        
        
        
        
        
        
        /*Debug.Log("Prev " + _currentPage);
        if (_currentPage > 1)
        {
            Vector3 targetPos = _screens[_page].transform.localPosition;
            targetPos.x += 5000;
            _screens[_page].GetComponent<RectTransform>().DOLocalMove(targetPos, _tweenTime).SetEase(Ease.OutCubic);

            _page--;
            _currentPage--;
            ChangePoints(_page);

            _screens[_page].SetActive(true);
            _screens[_page].transform.localPosition = new Vector3(-5000, 0, 0);
            _screens[_page].GetComponent<RectTransform>().DOLocalMove(new Vector3(0, 0, 0), _tweenTime)
                .SetEase(Ease.OutCubic);

            Checkpage();
            
            
            
            
            
            
            /*_startPage--;
            _currentPage--;
            Show(_currentPage);
            ChangePoints(_startPage);
            _targetPos -= _pageStep;
            MovePages();
            _viewSwipeHandler.SetInitialPosition(_targetPos);#1#
        }
        else
        {
            MovePagesPrev();
        }*/
    }

    public void Move()
    {
    }

    public void MovePagesPrev()
    {
    }

    private void Checkpage()
    {
        Debug.Log("Page " + _page);
        if (_page == 5)
        {
            _buttons[1].SetActive(true);
            _buttons[0].SetActive(false);
        }
        else
        {
            _buttons[1].SetActive(false);
            _buttons[0].SetActive(true);
        }


        /*if (_currentPage == _maxPage)
        {
            _buttons[1].SetActive(true);
            _buttons[0].SetActive(false);
        }
        else
        {
            _buttons[1].SetActive(false);
            _buttons[0].SetActive(true);
        } */
    }

    private void ChangePoints(int index)
    {
        Debug.Log("Индекс  " + index);
        if (index == 0)
        {
            _pointsObjectContent.SetActive(false);
        }

        if (index > 0)
        {
            _pointsObjectContent.SetActive(true);
            
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
}