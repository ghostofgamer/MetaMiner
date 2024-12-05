using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewFarmPopupView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI rarityOnCardText;

    [SerializeField]
    private TextMeshProUGUI powerClickOnCardText;

    [SerializeField]
    private TextMeshProUGUI energyOnCardText;

    [SerializeField]
    private TextMeshProUGUI restoreOnCardText;

    [SerializeField]
    private TextMeshProUGUI powerClickCostText;

    [SerializeField]
    private TextMeshProUGUI energyCostText;

    [SerializeField]
    private TextMeshProUGUI restoreCostText;

    [SerializeField]
    private Image backgroundCommonCardImage;

    [SerializeField]
    private Image backgroundRareCardImage;

    [SerializeField]
    private Image backgroundEpicCardImage;

    [SerializeField]
    private Image backgroundLegendaryCardImage;

    [System.Serializable]
    public class RaritySprites
    {
        public Sprite[] levels = new Sprite[5];
    }

    [Header("Sprites by Rarity")]
    [SerializeField]
    private RaritySprites common = new RaritySprites();
    [SerializeField]
    private RaritySprites rare = new RaritySprites();
    [SerializeField]
    private RaritySprites epic = new RaritySprites();
    [SerializeField]
    private RaritySprites legendary = new RaritySprites();

    private Sprite GetSprite(string rarity, int level)
    {
        RaritySprites raritySet = rarity switch
        {
            "common" => common,
            "rare" => rare,
            "epic" => epic,
            "legendary" => legendary,
            _ => null
        };

        if (raritySet == null || level < 1 || level > 5) return null;

        Sprite sprite = raritySet.levels[level - 1];
        return sprite;
    }

    public void ShowCard(CardState state)
    {
        if (state == null) return;

        backgroundCommonCardImage.gameObject.SetActive(false);
        backgroundRareCardImage.gameObject.SetActive(false);
        backgroundEpicCardImage.gameObject.SetActive(false);
        backgroundLegendaryCardImage.gameObject.SetActive(false);

        switch (state.Type)
        {
            case "common":
                rarityOnCardText.text = "Common";

                backgroundCommonCardImage.gameObject.SetActive(true);
                backgroundCommonCardImage.sprite = GetSprite(state.Type, state.Level);
                break;
            case "rare":
                rarityOnCardText.text = "Rare";

                backgroundRareCardImage.gameObject.SetActive(true);
                backgroundRareCardImage.sprite = GetSprite(state.Type, state.Level);
                break;
            case "epic":
                rarityOnCardText.text = "Epic";

                backgroundEpicCardImage.gameObject.SetActive(true);
                backgroundEpicCardImage.sprite = GetSprite(state.Type, state.Level);
                break;
            case "legendary":
                rarityOnCardText.text = "Legendary";

                backgroundLegendaryCardImage.gameObject.SetActive(true);
                backgroundLegendaryCardImage.sprite = GetSprite(state.Type, state.Level);
                break;
        }

        var powerConfig = AppPresenter.Instance.GetConfig(state.PowerLevel, state.Type);
        var batteryConfig = AppPresenter.Instance.GetConfig(state.BatteryLevel, state.Type);
        var restoreEnergyConfig = AppPresenter.Instance.GetConfig(state.ElectricityLevel, state.Type);

        if (powerConfig == null | batteryConfig == null | restoreEnergyConfig == null) return;

        powerClickOnCardText.text = $"{powerConfig.Power}/click";
        energyOnCardText.text = $"{batteryConfig.BatteryLimit}";
        restoreOnCardText.text = $"{restoreEnergyConfig.Electricity}/s";

        powerClickCostText.text = $"{powerConfig.UpgradeCost:n0}";
        energyCostText.text = $"{batteryConfig.UpgradeCost:n0}";
        restoreCostText.text = $"{restoreEnergyConfig.UpgradeCost:n0}";
    }
}
