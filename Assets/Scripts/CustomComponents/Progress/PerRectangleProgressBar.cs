using System.Collections;
using System.Collections.Generic;
using ThisOtherThing.UI.Shapes;
using UnityEngine;
using UnityEngine.UI;

public class PerRectangleProgressBar : MonoBehaviour
{
    private List<Rectangle> items = new List<Rectangle>();

    [SerializeField]
    private int itemsCount; // Максимальное количество объектов

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
        // Создаем начальное количество объектов на основе prefab и добавляем их в список
        InitializeItems();
        UpdateProgressBar();
    }

    // Метод для изменения itemsCount и обновления прогресс-бара
    public void SetMaxItemsCount(int newItemsCount)
    {
        // Обновляем количество элементов
        itemsCount = newItemsCount;

        // Пересоздаем объекты в соответствии с новым количеством
        InitializeItems();

        // Обновляем прогресс с новым количеством элементов
        UpdateProgressBar();
    }

    public void SetValue(float value)
    {
        this.value = value;
        UpdateProgressBar();
    }

    // Метод для инициализации объектов в прогресс-баре
    private void InitializeItems()
    {
        // Очищаем старые объекты
        foreach (var item in items)
        {
            Destroy(item.gameObject);
        }
        items.Clear();

        // Создаем нужное количество объектов на основе prefab и добавляем их в список
        for (int i = 0; i < itemsCount; i++)
        {
            Rectangle item = Instantiate(prefab.gameObject, transform).GetComponent<Rectangle>(); // создаем новый объект и добавляем в качестве дочернего
            items.Add(item);
        }
    }

    private void UpdateProgressBar()
    {
        // Вычисляем процент прогресса от 0 до itemsCount
        float percentage = Mathf.Clamp01(value / itemsCount);

        // Вычисляем количество активных объектов в зависимости от процента
        int activeItemsCount = Mathf.FloorToInt(percentage * items.Count);

        // Обновляем цвет каждого объекта
        for (int i = 0; i < items.Count; i++)
        {
            if (i < activeItemsCount)
            {
                // Активный объект
                items[i].ShapeProperties.FillColor = activeColor;
            }
            else
            {
                // Неактивный объект
                items[i].ShapeProperties.FillColor = inactiveColor;
            }
        }
    }
}
