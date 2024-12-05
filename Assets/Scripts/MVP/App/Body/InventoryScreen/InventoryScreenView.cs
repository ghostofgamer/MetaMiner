using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScreenView : MonoBehaviour
{
    [SerializeField]
    private RectTransform inventoryParent;

    [SerializeField]
    private GameObject boxItemPrefab;

    [SerializeField]
    private GameObject cardItemPrefab;

    [SerializeField]
    private TextMeshProUGUI inventoryCountText;

    [SerializeField]
    private TMP_Dropdown typeDropdown;

    [SerializeField]
    private TMP_Dropdown rareDropdown;

    [SerializeField]
    private InventoryScreenPresenter presenter;

    public void ShowItemsInInventory(List<IInventoryItem> items)
    {
        inventoryCountText.text = $"Inventory | {items.Count} Items";

        if (enabled)
        {
            StopAllCoroutines();
            StartCoroutine(ShowItemsInInventoryCoroutine(items));
        }
    }

    private IEnumerator ShowItemsInInventoryCoroutine(List<IInventoryItem> items)
    {
        for (int i = inventoryParent.childCount - 1; i >= 0; i--)
        {
            if (i % 5 == 0)
                yield return new WaitForEndOfFrame();
            ObjectPool.Instance.ReturnObject(inventoryParent.GetChild(i).gameObject);
        }

        for (int i = 0; i < items.Count; i++)
        {
            if (i % 5 == 0)
                yield return new WaitForEndOfFrame();

            switch (items[i].ItemType)
            {
                case "box":
                    SpawnBox(items[i] as BoxState);
                    break;
                case "farm":
                    SpawnCard(items[i] as CardState);
                    break;
            }
        }
    }

    private void SpawnBox(BoxState state)
    {
        var go = ObjectPool.Instance.GetObject(boxItemPrefab);
        go.transform.SetParent(inventoryParent);
        go.transform.localScale = Vector3.one;
        var itemPresenter = go.GetComponent<BoxInventoryPresenter>();
        itemPresenter.SetBox(state);
    }

    private void SpawnCard(CardState state)
    {
        var go = ObjectPool.Instance.GetObject(cardItemPrefab);
        go.transform.SetParent(inventoryParent);
        go.transform.localScale = Vector3.one;
        var itemPresenter = go.GetComponent<CardInventoryPresenter>();
        itemPresenter.SetCard(state);
    }

    public void OnChangeFilters()
    {
        string typeFilter = "all";
        presenter.SetRareSort(rareDropdown.options[rareDropdown.value].text.ToLower());

        switch (typeDropdown.value)
        {
            case 0:
                typeFilter = "all";
                break;
            case 1:
                typeFilter = "farm";
                break;
            case 2:
                typeFilter = "box";
                presenter.SetRareSort("all");
                rareDropdown.value = 0;
                break;
            case 3:
                typeFilter = "pass";
                break;
        }

        presenter.SetTypeSort(typeFilter);
    }
}
