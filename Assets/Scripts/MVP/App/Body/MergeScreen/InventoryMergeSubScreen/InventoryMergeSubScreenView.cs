using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryMergeSubScreenView : MonoBehaviour
{
    [SerializeField]
    private RectTransform inventoryParent;

    [SerializeField]
    private GameObject cardItemPrefab;

    [SerializeField]
    private TextMeshProUGUI headerText;

    [SerializeField]
    private InventoryMergeSubScreenPresenter presenter;

    public void ShowCards(List<CardState> items)
    {
        headerText.text = $"Select 1 item of {items.Count} Items";

    }

    private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(ShowCardsCoroutine(presenter.GetCards()));
    }

    private IEnumerator ShowCardsCoroutine(List<CardState> items)
    {
        for (int i = inventoryParent.childCount - 1; i >= 0; i--)
        {
            if (i % 30 == 0)
                yield return new WaitForEndOfFrame();
            ObjectPool.Instance.ReturnObject(inventoryParent.GetChild(i).gameObject);
        }

        for (int i = 0; i < items.Count; i++)
        {
            if (i % 5 == 0)
                yield return new WaitForEndOfFrame();
            SpawnCard(items[i]);
        }
    }

    private void SpawnCard(CardState state)
    {
        GameObject go = ObjectPool.Instance.GetObject(cardItemPrefab);
        go.transform.SetParent(inventoryParent, false);
        go.transform.localScale = Vector3.one;
        var itemPresenter = go.GetComponent<CardInventoryPresenter>();
        itemPresenter.SetCard(state);
    }
}
