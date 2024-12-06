using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private List<GameObject> _screens;
    [SerializeField] private GameObject _pointsObjectContent;
    [SerializeField] private List<GameObject> _descriptions;
    [SerializeField] private GameObject[] _buttons;
    [SerializeField] private int _maxPage;
    [SerializeField] private float _tweenTime;
    [SerializeField] private PointChanger[] _points;

    private int _step = 5000;
    private Vector3 _defaultPosition = new Vector3(0, 0, 0);

    public int _page = 0;

    private void Start()
    {
        _page = 0;
        ChangePoints(_page);
        CheckPage();
    }

    public GameObject GetScreen()
    {
        return _screens[_page];
    }
    
    private void MoveScreen(int direction)
    {
        int newPage = _page + direction;
        if (newPage >= 0 && newPage <= 5)
        {
            Vector3 targetPos = _screens[_page].transform.localPosition;
            targetPos.x += -direction * _step;
            _screens[_page].GetComponent<RectTransform>().DOLocalMove(targetPos, _tweenTime).SetEase(Ease.OutCubic);

            _page = newPage;
            ChangePoints(_page);

            _screens[_page].SetActive(true);
            _screens[_page].transform.localPosition = new Vector3(direction * _step, 0, 0);
            _screens[_page].GetComponent<RectTransform>().DOLocalMove(_defaultPosition, _tweenTime).SetEase(Ease.OutCubic);

            CheckPage();
        }
        else
        {
            MovePagesDefaultPos();
        }
    }
    
    public void Next()
    {
        MoveScreen(1);
    }

    public void Prev()
    {
        MoveScreen(-1);
    }

    public void MovePagesDefaultPos()
    {
        _screens[_page].GetComponent<RectTransform>().DOLocalMove(_defaultPosition, _tweenTime);
    }

    private void CheckPage()
    {
        _buttons[1].SetActive(_page == 5);
        _buttons[0].SetActive(_page != 5);
    }

    private void ChangePoints(int index)
    {
        _pointsObjectContent.SetActive(index > 0);

        if (index > 0)
        {
            foreach (var point in _points)
                point.OffPoint();

            _points[index - 1].OnPoint();
        }
    }
}