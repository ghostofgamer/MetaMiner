using MetaMiners.Network;
using System.Collections.Generic;
using UnityEngine;

public class MergeScreenPresenter : MonoBehaviour
{
    [SerializeField]
    private MergeScreenModel model;

    [SerializeField]
    private MergeScreenView view;

    [SerializeField]
    private InventoryMergeSubScreenPresenter inventoryPresenter;

    [field: SerializeField]
    public List<MergeItemPresenter> Items { get; private set; }

    private void OnEnable()
    {
        ClearAll();
    }

    public void SelectItem(CardState cardState)
    {
        Items[model.SelectedItem].SetCard(cardState);
        OpenMerge();
    }

    public void SetSelectedItem(int index)
    {
        model.SelectedItem.Value = index;
    }

    public void OpenInventory()
    {
        List<CardState> cards = new List<CardState>();

        string type = string.Empty;

        foreach (var item in Items)
        {
            var card = item.GetCard();

            if (card != null)
            {
                type = card.Type;
                cards.Add(card);
            }
        }

        inventoryPresenter.ShowCards(cards, type);
        view.OpenInventory();
    }

    public void OpenMerge()
    {
        view.OpenMerge();
    }

    public void ClearAll()
    {
        foreach (var item in Items)
        {
            item.SetCard(null);
        }
    }

    public void CheckMerge()
    {
        bool isEmpty = false;
        MergeItemPresenter lastEmpty = null;
        List<MergeItemPresenter> emptyItems = new List<MergeItemPresenter>();
        List<int> ids = new List<int>();

        foreach (var item in Items)
        {
            var card = item.GetCard();
            if (card == null)
            {
                isEmpty = true;
                lastEmpty = item;
                emptyItems.Add(item);
                Debug.Log($"{item.name}");
            }
            else
            {
                ids.Add(int.Parse(card.Id));
            }
        }

        if (isEmpty)
        {
            foreach (var item in emptyItems)
            {
                item.ShowError(false, "");
            }

            lastEmpty.ShowError(true, $"You need to add {emptyItems.Count} more Items for merge");
        }
        else
        {
            NetworkManager.Instance.PostCheckMerge(ids);
        }
    }

    public void Merge()
    {
        bool isEmpty = false;
        MergeItemPresenter lastEmpty = null;
        List<MergeItemPresenter> emptyItems = new List<MergeItemPresenter>();
        List<int> ids = new List<int>();

        foreach (var item in Items)
        {
            var card = item.GetCard();
            if (card == null)
            {
                isEmpty = true;
                lastEmpty = item;
                emptyItems.Add(item);
                Debug.Log($"{item.name}");
            }
            else
            {
                ids.Add(int.Parse(card.Id));
            }
        }

        if (isEmpty)
        {
            foreach (var item in emptyItems)
            {
                item.ShowError(false, "");
            }

            lastEmpty.ShowError(true, $"You need to add {emptyItems.Count} more Items for merge");
        }
        else
        {
            NetworkManager.Instance.PostMergeCards(ids);
        }
    }
}
