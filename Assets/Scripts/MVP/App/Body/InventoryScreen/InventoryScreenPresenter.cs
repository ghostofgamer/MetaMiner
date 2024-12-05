using MetaMiners.Network;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryScreenPresenter : MonoBehaviour
{
    [SerializeField]
    public InventoryScreenModel model;

    [SerializeField]
    private InventoryScreenView view;

    private void Awake()
    {
        model.Cards.Subscribe(ShowSortedItems);
        model.Boxes.Subscribe(ShowSortedItems);
        model.RareSort.Subscribe(ShowSortedItems);
        model.TypeSort.Subscribe(ShowSortedItems);

        SetTypeSort("all");
        SetRareSort("all");
    }

    private void OnEnable()
    {
        NetworkManager.Instance.PostGetProfile();
    }

    public void SetBoxes(List<Responses.Box> boxes)
    {
        var boxList = boxes.Select(box => new BoxState(
            box.Id,
            box.Owner,
            box.Type,
            box.MintedIn,
            box.CommonChance,
            box.RareChance,
            box.EpicChance,
            box.LegendaryChance,
            box.PremiumPassChance,
            box.UpgradeCharacterChance,
            box.UsdtChance,
            box.CoinsMin,
            box.CoinsMax,
            box.UsdtMin,
            box.UsdtMax,
            box.UpgradePerkMin,
            box.UpgradePerkMax,
            box.NothingChance
        )).ToList();

        model.Boxes.Value = boxList;
    }

    public void SetCards(List<Responses.Card> cards)
    {
        var cardList = cards.Select(card => new CardState(card.Id, card.Type, card.Level, card.PowerLevel, card.ElectricityLevel, card.BatteryLevel, card.EnergyAvailable, card.HasAutoMining)).ToList();

        model.Cards.Value = cardList;
    }

    public void SetTypeSort(string type)
    {
        model.TypeSort.Value = type;
    }

    public void SetRareSort(string rare)
    {
        model.RareSort.Value = rare;
    }

    public void ShowSortedItems()
    {
        if (!enabled) return;

        List<IInventoryItem> items = new List<IInventoryItem>();
        items.AddRange(model.Boxes.Value);
        items.AddRange(model.Cards.Value);

        // Фильтрация и сортировка
        var filteredAndSortedItems = items
            .Where(item => model.TypeSort == null || model.TypeSort == "all" || item.ItemType == model.TypeSort) // Фильтр по TypeSort
            .Where(item => string.IsNullOrEmpty(model.RareSort) || model.RareSort == "all" || item.Type == model.RareSort) // Фильтр по RareSort
            .OrderBy(item => item.ItemType) // Сортировка по ItemType
            .ThenBy(item => item.Type) // Дополнительная сортировка по Type
            .ToList();

        view.ShowItemsInInventory(filteredAndSortedItems);
    }
}
