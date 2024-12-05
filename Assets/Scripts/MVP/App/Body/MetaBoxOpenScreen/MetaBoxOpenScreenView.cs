using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MetaBoxOpenScreenView : MonoBehaviour
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

    [SerializeField]
    private GameObject commonCardObject, farmCardObject, mmcCardObject, usdtCardObject;

    [SerializeField]
    private TextMeshProUGUI mmcRewardText, usdtRewardText;

    [SerializeField]
    private CanvasGroup claimButton;

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

    private void OnEnable()
    {
        claimButton.alpha = 0f;
        claimButton.interactable = false;
        commonCardObject.SetActive(false);
    }

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

        farmCardObject.SetActive(true);
        mmcCardObject.SetActive(false);
        usdtCardObject.SetActive(false);

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

    public void ShowMMC(float mmc)
    {
        farmCardObject.SetActive(false);
        mmcCardObject.SetActive(true);
        usdtCardObject.SetActive(false);

        mmcRewardText.text = $"{mmc}";
    }

    public void ShowUSDT(float usdt)
    {
        farmCardObject.SetActive(false);
        mmcCardObject.SetActive(false);
        usdtCardObject.SetActive(true);

        usdtRewardText.text = $"{usdt}";
    }

    public void InvokeCardShowAnimation()
    {
        PrepareCardToAnimation();

        commonCardObject.SetActive(true);
        commonCardObject.transform.DORotate(Vector3.zero, 0.5f).SetEase(Ease.OutBounce);

        claimButton.interactable = true;
        claimButton.DOFade(1f, 0.5f);
    }

    public void InvokeCardHideAnimation(int currentCard, Action callback)
    {
        if (currentCard >= 0)
        {
            Canvas canvas = GetComponentInParent<Canvas>();
            RectTransform rectTransform = canvas.GetComponent<RectTransform>();
            commonCardObject.GetComponent<RectTransform>().DOAnchorPosY(-rectTransform.sizeDelta.y, 0.3f).SetEase(Ease.InCubic).OnComplete(() => callback?.Invoke());
        }
        else
            callback?.Invoke();

        claimButton.interactable = false;
        claimButton.DOFade(0f, 0.3f);
    }

    public void PrepareCardToAnimation()
    {
        commonCardObject.transform.rotation = Quaternion.Euler(-10f, 90f, 0f);
        commonCardObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        commonCardObject.SetActive(false);
    }
}
