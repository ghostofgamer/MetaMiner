using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Globalization;

public class MetaBoxPopupView : MonoBehaviour
{
    [SerializeField]
    private GameObject itemPrefab;

    [SerializeField]
    private RectTransform parent;

    [SerializeField]
    private Sprite mmcSprite, usdtSprite, skillSprite, cardSprite;

    public void ShowBox(BoxState state)
    {
        // Очистка предыдущих элементов
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }

        // 1. MMC Coins
        if (state.CoinsMin > 0 && state.CoinsMax > 0)
        {
            SpawnItem("MMC Coins", mmcSprite);
        }

        // 2. +1 to a random farm skill
        if (state.UpgradePerkMin > 0 && state.UpgradePerkMax > 0)
        {
            SpawnItem($"+{state.UpgradePerkMax} to a random farm skill.", skillSprite);
        }

        // 3. Random farm card (Common, Rare, etc.)
        string cardText = BuildCardText(state);
        if (!string.IsNullOrEmpty(cardText))
        {
            SpawnItem(cardText, cardSprite);
        }

        // 4. USDT Coins
        float usdtMin = TryParseFloat(state.UsdtMin);
        float usdtMax = TryParseFloat(state.UsdtMax);
        if (usdtMin > 0 && usdtMax > 0)
        {
            SpawnItem("USDT Coins", usdtSprite);
        }
    }

    private void SpawnItem(string text, Sprite icon)
    {
        // Создание префаба
        var item = Instantiate(itemPrefab, parent);

        // Установка текста
        var textComponent = item.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        if (textComponent != null)
        {
            textComponent.text = text;
        }

        // Установка иконки
        var imageComponent = item.transform.Find("Icon").GetComponent<Image>();
        if (imageComponent != null)
        {
            imageComponent.sprite = icon;
        }
    }

    private float TryParseFloat(string value)
    {
        if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out float result))
        {
            return result;
        }
        return 0f;
    }

    private string BuildCardText(BoxState state)
    {
        // Формируем список выпадений карточек
        var cardTypes = new System.Collections.Generic.List<string>();

        if (TryParseFloat(state.CommonChance) > 0)
        {
            cardTypes.Add("\"Common\"");
        }

        if (TryParseFloat(state.RareChance) > 0)
        {
            cardTypes.Add("\"Rare\"");
        }

        if (TryParseFloat(state.EpicChance) > 0)
        {
            cardTypes.Add("\"Epic\"");
        }

        if (TryParseFloat(state.LegendaryChance) > 0)
        {
            cardTypes.Add("\"Legendary\"");
        }

        if (cardTypes.Count > 0)
        {
            return $"Random {string.Join(", ", cardTypes)} farm.";
        }

        return string.Empty;
    }
}
