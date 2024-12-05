using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MergeItemView : MonoBehaviour
{
    [SerializeField]
    private MergeItemPresenter presenter;

    [SerializeField]
    private GameObject normalState;

    [SerializeField]
    private GameObject objectState;

    [SerializeField]
    private GameObject warningState;

    [SerializeField]
    private TextMeshProUGUI rareText;

    [SerializeField]
    private Image rareIconImage;

    [SerializeField]
    private TextMeshProUGUI farmLevelText;

    [SerializeField]
    private TextMeshProUGUI powerClickText;

    [SerializeField]
    private TextMeshProUGUI energyText;

    [SerializeField]
    private TextMeshProUGUI restoreText;

    [SerializeField]
    private Image itemPictureImage;

    [SerializeField]
    private Color commonColor;

    [SerializeField]
    private Color rareColor;

    [SerializeField]
    private Color epicColor;

    [SerializeField]
    private Color legendaryColor;

    [SerializeField]
    private GameObject errorTextObject;

    [SerializeField]
    private TextMeshProUGUI errorText;

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
        HideError();

        if (state == null)
        {
            normalState.SetActive(true);
            objectState.SetActive(false);

            return;
        }
        else
        {
            normalState.SetActive(false);
            objectState.SetActive(true);
        }

        rareText.text = state.Type.ToUpper();

        switch (state.Type)
        {
            case "common":
                rareText.color = commonColor;
                rareIconImage.color = commonColor;
                break;
            case "rare":
                rareText.color = rareColor;
                rareIconImage.color = rareColor;
                break;
            case "epic":
                rareText.color = epicColor;
                rareIconImage.color = epicColor;
                break;
            case "legendary":
                rareText.color = legendaryColor;
                rareIconImage.color = legendaryColor;
                break;
        }

        farmLevelText.text = $"Farm Level {state.Level}";
        itemPictureImage.sprite = GetSprite(state.Type, state.Level);

        var powerConfig = AppPresenter.Instance.GetConfig(state.PowerLevel, state.Type);
        var batteryConfig = AppPresenter.Instance.GetConfig(state.BatteryLevel, state.Type);
        var restoreEnergyConfig = AppPresenter.Instance.GetConfig(state.ElectricityLevel, state.Type);

        if (powerConfig == null)
        {
            Debug.LogWarning($"NULL in Power Config for Power Level = {state.PowerLevel} and Type = {state.Type}");
        }

        if (batteryConfig == null)
        {
            Debug.LogWarning($"NULL in Battery Config for Battery Level = {state.BatteryLevel} and Type = {state.Type}");
        }

        if (restoreEnergyConfig == null)
        {
            Debug.LogWarning($"NULL in Restore Energy Config for Electricity Level = {state.ElectricityLevel} and Type = {state.Type}");
        }

        if (powerConfig == null | batteryConfig == null | restoreEnergyConfig == null)
        {
            return;
        }

        powerClickText.text = powerClickText.text = $"{powerConfig.Power}/click";
        energyText.text = energyText.text = $"{batteryConfig.BatteryLimit}";
        restoreText.text = restoreText.text = $"{restoreEnergyConfig.Electricity}/s";
    }

    public void ShowError(bool showText, string text)
    {
        normalState.SetActive(false);
        objectState.SetActive(false);

        warningState.SetActive(true);
        errorTextObject.SetActive(showText);
        errorText.text = text;

        StopAllCoroutines();
        StartCoroutine(DelayedHideError());
    }

    public void HideError()
    {
        if (presenter.GetCard() == null)
        {
            normalState.SetActive(true);
        }
        else
        {
            objectState.SetActive(true);
        }

        warningState.SetActive(false);
        errorTextObject.SetActive(false);
    }

    private IEnumerator DelayedHideError()
    {
        yield return new WaitForSeconds(3f);
        HideError();
    }
}
