using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryMergeSubScreenPresenter : MonoBehaviour
{
    [SerializeField]
    private InventoryMergeSubScreenModel model;

    [SerializeField]
    private InventoryMergeSubScreenView view;

    private void Awake()
    {
        model.Cards.Subscribe(cards => view.ShowCards(cards));
    }

    public void ShowCards(List<CardState> exclude, string type)
    {
        List<CardState> inventory = AppPresenter.Instance.BodyPresenter.InventoryScreenPresenter.model.Cards;

        foreach (CardState card in exclude)
        {
            if (card != null)
            {
                inventory.Remove(card);
            }
        }

        if (type != string.Empty)
            inventory = inventory.Where(card => card.Type == type).ToList();

        model.Cards.Value = inventory;
    }

    public List<CardState> GetCards()
    {
        return model.Cards.Value;
    }
}
