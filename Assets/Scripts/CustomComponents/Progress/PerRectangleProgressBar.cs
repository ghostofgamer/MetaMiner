using System.Collections;
using System.Collections.Generic;
using ThisOtherThing.UI.Shapes;
using UnityEngine;
using UnityEngine.UI;

public class PerRectangleProgressBar : MonoBehaviour
{
    private List<Rectangle> items = new List<Rectangle>();

    [SerializeField]
    private int itemsCount; // ������������ ���������� ��������

    [SerializeField]
    private float value;

    [SerializeField]
    private Graphic prefab;

    [SerializeField]
    private Color activeColor = Color.white;

    [SerializeField]
    private Color inactiveColor = Color.gray;

    private void Awake()
    {
        // ������� ��������� ���������� �������� �� ������ prefab � ��������� �� � ������
        InitializeItems();
        UpdateProgressBar();
    }

    // ����� ��� ��������� itemsCount � ���������� ��������-����
    public void SetMaxItemsCount(int newItemsCount)
    {
        // ��������� ���������� ���������
        itemsCount = newItemsCount;

        // ����������� ������� � ������������ � ����� �����������
        InitializeItems();

        // ��������� �������� � ����� ����������� ���������
        UpdateProgressBar();
    }

    public void SetValue(float value)
    {
        this.value = value;
        UpdateProgressBar();
    }

    // ����� ��� ������������� �������� � ��������-����
    private void InitializeItems()
    {
        // ������� ������ �������
        foreach (var item in items)
        {
            Destroy(item.gameObject);
        }
        items.Clear();

        // ������� ������ ���������� �������� �� ������ prefab � ��������� �� � ������
        for (int i = 0; i < itemsCount; i++)
        {
            Rectangle item = Instantiate(prefab.gameObject, transform).GetComponent<Rectangle>(); // ������� ����� ������ � ��������� � �������� ���������
            items.Add(item);
        }
    }

    private void UpdateProgressBar()
    {
        // ��������� ������� ��������� �� 0 �� itemsCount
        float percentage = Mathf.Clamp01(value / itemsCount);

        // ��������� ���������� �������� �������� � ����������� �� ��������
        int activeItemsCount = Mathf.FloorToInt(percentage * items.Count);

        // ��������� ���� ������� �������
        for (int i = 0; i < items.Count; i++)
        {
            if (i < activeItemsCount)
            {
                // �������� ������
                items[i].ShapeProperties.FillColor = activeColor;
            }
            else
            {
                // ���������� ������
                items[i].ShapeProperties.FillColor = inactiveColor;
            }
        }
    }
}
