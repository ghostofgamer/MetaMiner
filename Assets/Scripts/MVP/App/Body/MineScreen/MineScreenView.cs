using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using System.Collections;

public class MineScreenView : MonoBehaviour
{
    [SerializeField]
    private MineScreenPresenter presenter;

    [SerializeField]
    private TextMeshProUGUI farmLevelText;

    [SerializeField]
    private TextMeshProUGUI upgradesText;

    [SerializeField]
    private TextMeshProUGUI mmcBalanceText;

    [SerializeField]
    private TextMeshProUGUI earningsText;

    [SerializeField]
    private TextMeshProUGUI energyText;

    [SerializeField]
    private PerRectangleProgressBar upgradesProgressBar;

    [SerializeField]
    private Slider energySlider;

    [SerializeField]
    private GameObject clickTextPrefab;

    [SerializeField]
    private RectTransform clicksParent;

    [SerializeField]
    private Image glowBackground;

    [SerializeField]
    private Sprite[] glowByLevel = new Sprite[5];

    public void ShowFarmLevel(int farmLevel, int maxFarmLevel)
    {
        farmLevelText.text = $"{farmLevel}/{maxFarmLevel}";

        if (farmLevel > 0 & farmLevel <= 5)
            glowBackground.sprite = glowByLevel[farmLevel - 1];
    }

    public void ShowUpgrades(int upgrades, int farmLevel)
    {
        int maxRating = 0;
        int rating = upgrades;

        // Изменить
        if (farmLevel == 1)
        {
            maxRating = 14;
        }
        else if (farmLevel == 2)
        {
            maxRating = 29 - 14;
            rating = rating - 14;
        }
        else if (farmLevel == 3)
        {
            maxRating = 44 - 29;
            rating = rating - 29;
        }
        else if (farmLevel == 4)
        {
            maxRating = 59 - 44;
            rating = rating - 44;
        }

        upgradesText.text = $"{rating}/{maxRating}";

        upgradesProgressBar.SetMaxItemsCount(maxRating);
        upgradesProgressBar.SetValue(rating);
    }

    public void ShowMMCBalance(int mmcBalance)
    {
        string formattedMMCBalance = string.Format("{0:N0}", mmcBalance).Replace(",", " ");
        mmcBalanceText.text = $"{formattedMMCBalance}";
    }

    public void ShowEarnings(int earnings)
    {
        earningsText.text = $"Earnings: {earnings}/h";
    }

    public void ShowEnergy(int energy, int maxEnergy)
    {
        energySlider.minValue = 0;
        energySlider.maxValue = maxEnergy;

        energySlider.DOValue(energy, 0.3f);

        energyText.text = $"{energy}/{maxEnergy}";
    }

    public void CubeClicked()
    {
        presenter.CubeClicked();
    }

    public void SpawnClick(int power)
    {
        // Для ПК
        if (Input.GetMouseButtonUp(0))
        {
            CreateClickEffect(Input.mousePosition, power);
        }

        // Для мобильных устройств
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Began)
            {
                CreateClickEffect(touch.position, power);
            }
        }
    }

    private void CreateClickEffect(Vector2 screenPosition, int power)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            clicksParent as RectTransform,
            screenPosition,
            Camera.main,
            out Vector2 localPoint
        );

        GameObject go = ObjectPool.Instance.GetObject(clickTextPrefab);
        go.transform.SetParent(clicksParent, false);
        go.transform.localScale = Vector3.one;

        go.GetComponent<TextMeshProUGUI>().text = $"+{power}";
        go.GetComponent<RectTransform>().localPosition = localPoint;
        go.GetComponent<ClickEffectAnimator>().StartAnimation();
    }
}
