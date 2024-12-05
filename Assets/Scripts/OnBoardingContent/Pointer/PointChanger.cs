using UnityEngine;

public class PointChanger : MonoBehaviour
{
    [SerializeField] private GameObject _fullPoint;
    [SerializeField] private GameObject _notFullPoint;

    public void OffPoint()
    {
        _fullPoint.SetActive(false);
        // _notFullPoint.SetActive(true);
    }

    public void OnPoint()
    {
        _fullPoint.SetActive(true);
        // _notFullPoint.SetActive(false);
    }
}
