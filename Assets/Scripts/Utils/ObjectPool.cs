using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : Singleton<ObjectPool>
{
    [System.Serializable]
    public class PoolItem
    {
        public GameObject prefab;
        public int initialSize;
    }

    [SerializeField]
    private List<PoolItem> poolItems = new List<PoolItem>();

    private Dictionary<GameObject, Queue<GameObject>> poolDictionary = new Dictionary<GameObject, Queue<GameObject>>();

    private void Start()
    {
        // Инициализация пула с заданными префабами
        foreach (var item in poolItems)
        {
            Queue<GameObject> objectQueue = new Queue<GameObject>();

            for (int i = 0; i < item.initialSize; i++)
            {
                GameObject obj = Instantiate(item.prefab, transform);
                obj.SetActive(false);
                objectQueue.Enqueue(obj);
            }

            poolDictionary.Add(item.prefab, objectQueue);
        }
    }

    // Получение объекта из пула
    public GameObject GetObject(GameObject prefab)
    {
        if (poolDictionary.ContainsKey(prefab) && poolDictionary[prefab].Count > 0)
        {
            GameObject obj = poolDictionary[prefab].Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            // Если объектов недостаточно, создаём новые
            GameObject newObj = Instantiate(prefab);
            newObj.SetActive(true);
            return newObj;
        }
    }

    // Возврат объекта в пул
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(transform);
        // Поиск префаба, который использовался для этого объекта
        foreach (var poolItem in poolItems)
        {
            if (obj.name.StartsWith(poolItem.prefab.name))
            {
                if (poolDictionary.ContainsKey(poolItem.prefab))
                {
                    poolDictionary[poolItem.prefab].Enqueue(obj);
                    return;
                }
            }
        }

        // Если объект не был найден в пуле, уничтожаем его
        Destroy(obj);
    }
}
