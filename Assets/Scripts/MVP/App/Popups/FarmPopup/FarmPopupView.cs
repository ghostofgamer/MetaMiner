using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FarmPopupView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI rarityOnCardText;

    [SerializeField]
    private TextMeshProUGUI rarityText;

    [SerializeField]
    private TextMeshProUGUI farmLevelText;

    [SerializeField]
    private Image rareIconImage;

    [SerializeField]
    private TextMeshProUGUI powerClickOnCardText;

    [SerializeField]
    private TextMeshProUGUI energyOnCardText;

    [SerializeField]
    private TextMeshProUGUI restoreOnCardText;

    [SerializeField]
    private TextMeshProUGUI powerClickText;

    [SerializeField]
    private TextMeshProUGUI energyText;

    [SerializeField]
    private TextMeshProUGUI restoreText;

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

    [SerializeField]
    private Color commonColor;

    [SerializeField]
    private Color rareColor;

    [SerializeField]
    private Color epicColor;

    [SerializeField]
    private Color legendaryColor;

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
        rarityText.text = state.Type.ToUpper();

        backgroundCommonCardImage.gameObject.SetActive(false);
        backgroundRareCardImage.gameObject.SetActive(false);
        backgroundEpicCardImage.gameObject.SetActive(false);
        backgroundLegendaryCardImage.gameObject.SetActive(false);

        switch (state.Type)
        {
            case "common":
                rarityText.color = commonColor;
                rareIconImage.color = commonColor;
                rarityOnCardText.text = "Common";

                backgroundCommonCardImage.gameObject.SetActive(true);
                backgroundCommonCardImage.sprite = GetSprite(state.Type, state.Level);
                break;
            case "rare":
                rarityText.color = rareColor;
                rareIconImage.color = rareColor;
                rarityOnCardText.text = "Rare";

                backgroundRareCardImage.gameObject.SetActive(true);
                backgroundRareCardImage.sprite = GetSprite(state.Type, state.Level);
                break;
            case "epic":
                rarityText.color = epicColor;
                rareIconImage.color = epicColor;
                rarityOnCardText.text = "Epic";

                backgroundEpicCardImage.gameObject.SetActive(true);
                backgroundEpicCardImage.sprite = GetSprite(state.Type, state.Level);
                break;
            case "legendary":
                rarityText.color = legendaryColor;
                rareIconImage.color = legendaryColor;
                rarityOnCardText.text = "Legendary";

                backgroundLegendaryCardImage.gameObject.SetActive(true);
                backgroundLegendaryCardImage.sprite = GetSprite(state.Type, state.Level);
                break;
        }

        var powerConfig = AppPresenter.Instance.GetConfig(state.PowerLevel, state.Type);
        var batteryConfig = AppPresenter.Instance.GetConfig(state.BatteryLevel, state.Type);
        var restoreEnergyConfig = AppPresenter.Instance.GetConfig(state.ElectricityLevel, state.Type);

        powerClickOnCardText.text = powerClickText.text = $"{powerConfig.Power}/click";
        energyOnCardText.text = energyText.text = $"{batteryConfig.BatteryLimit}";
        restoreOnCardText.text = restoreText.text = $"{restoreEnergyConfig.Electricity}/s";

        powerClickCostText.text = $"{powerConfig.UpgradeCost:n0}";
        energyCostText.text = $"{batteryConfig.UpgradeCost:n0}";
        restoreCostText.text = $"{restoreEnergyConfig.UpgradeCost:n0}";

        farmLevelText.text = $"Farm level {state.Level}";
    }
}
