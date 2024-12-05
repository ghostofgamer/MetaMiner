using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MergeProbabilityPopupView : MonoBehaviour
{
    [SerializeField]
    private Image[] cardPictures = new Image[5];

    [SerializeField]
    private TextMeshProUGUI[] rarityTexts = new TextMeshProUGUI[5];

    [SerializeField]
    private TextMeshProUGUI[] levelTexts = new TextMeshProUGUI[5];

    [SerializeField]
    private Image[] rarityIcons = new Image[5];

    [SerializeField]
    private Image newCardPicture;

    [SerializeField]
    private TextMeshProUGUI newRarityText;

    [SerializeField]
    private TextMeshProUGUI newLevelText;

    [SerializeField]
    private Image newRarityIcon;

    [SerializeField]
    private TextMeshProUGUI probabilityText;

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

    public void ShowCards(List<CardState> cardsToMerge)
    {
        for (int i = 0; i < cardsToMerge.Count && i < 5; i++)
        {
            var card = cardsToMerge[i];

            rarityTexts[i].text = card.Type.ToUpper();
            levelTexts[i].text = $"Farm Level {card.Level}";
            cardPictures[i].sprite = GetSprite(card.Type, card.Level);

            Color rarityColor = card.Type switch
            {
                "common" => commonColor,
                "rare" => rareColor,
                "epic" => epicColor,
                "legendary" => legendaryColor,
                _ => Color.white
            };

            rarityTexts[i].color = rarityColor;
            rarityIcons[i].color = rarityColor;
        }
    }

    public void ShowNewCard(CardState newCard)
    {
        newRarityText.text = newCard.Type.ToUpper();
        newLevelText.text = $"Farm Level {newCard.Level}";
        newCardPicture.sprite = GetSprite(newCard.Type, newCard.Level);

        Color rarityColor = newCard.Type switch
        {
            "common" => commonColor,
            "rare" => rareColor,
            "epic" => epicColor,
            "legendary" => legendaryColor,
            _ => Color.white
        };

        newRarityText.color = rarityColor;
        newRarityIcon.color = rarityColor;
    }

    public void ShowProbability(int value)
    {
        probabilityText.text = $"{value}%";
    }

}
