using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeScreenView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI balanceText;

    [SerializeField]
    private TextMeshProUGUI farmLevelText;

    [SerializeField]
    private TextMeshProUGUI currentPowerTapText;

    [SerializeField]
    private TextMeshProUGUI currentBatteryText;

    [SerializeField]
    private TextMeshProUGUI currentRestoreEnergySpeedText;

    [SerializeField]
    private TextMeshProUGUI powerTapCostText;

    [SerializeField]
    private TextMeshProUGUI batteryCostText;

    [SerializeField]
    private TextMeshProUGUI restoreEnergySpeedCostText;

    [SerializeField]
    private TextMeshProUGUI updatedPowerTapText;

    [SerializeField]
    private TextMeshProUGUI updatedBatteryText;

    [SerializeField]
    private TextMeshProUGUI updatedRestoreEnergySpeedText;

    [SerializeField]
    private TextMeshProUGUI autoMiningCostText;

    [SerializeField]
    private TextMeshProUGUI updatedAutoMiningText;

    [SerializeField]
    private Button powerTapButton;

    [SerializeField]
    private Button batteryButton;

    [SerializeField]
    private Button restoreEnergySpeedButton;

    [SerializeField]
    private Button autoMiningButton;

    [SerializeField]
    private GameObject autoMiningAvailableObject;

    [SerializeField]
    private Image glowBackground;

    [SerializeField]
    private Sprite[] glowByLevel = new Sprite[5];

    public void ShowBalance(int balance) => balanceText.text = $"{balance:n0}";
    public void ShowFarmLevel(int level)
    {
        farmLevelText.text = $"Farm level {level}";

        if (level > 0 & level <= 5)
            glowBackground.sprite = glowByLevel[level - 1];
    }
    public void ShowCurrentPowerTap(int powerTap) => currentPowerTapText.text = $"{powerTap}/click";
    public void ShowCurrentBattery(int battery) => currentBatteryText.text = $"{battery}";
    public void ShowCurrentRestoreEnergySpeed(int speed) => currentRestoreEnergySpeedText.text = $"{speed}/s";
    public void ShowPowerTapCost(int cost)
    {
        powerTapButton.interactable = true;

        powerTapCostText.text = $"{cost:n0}";
        if (cost == 0)
        {
            powerTapButton.interactable = false;
            powerTapCostText.text = "-";
        }
    }

    public void ShowBatteryCost(int cost)
    {
        batteryButton.interactable = true;

        batteryCostText.text = $"{cost:n0}";
        if (cost == 0)
        {
            batteryButton.interactable = false;
            batteryCostText.text = "-";
        }
    }

    public void ShowRestoreEnergySpeedCost(int cost)
    {
        restoreEnergySpeedButton.interactable = true;

        restoreEnergySpeedCostText.text = $"{cost:n0}";
        if (cost == 0)
        {
            restoreEnergySpeedButton.interactable = false;
            restoreEnergySpeedCostText.text = "-";
        }
    }

    public void ShowUpdatedPowerTap(int powerTap)
    {
        powerTapButton.interactable = true;

        updatedPowerTapText.text = $"{powerTap}/click";
        if (powerTap == 0)
        {
            powerTapButton.interactable = false;
            updatedPowerTapText.text = "Max";
        }
    }

    public void ShowUpdatedBattery(int battery)
    {
        batteryButton.interactable = true;

        updatedBatteryText.text = $"{battery}";
        if (battery == 0)
        {
            batteryButton.interactable = false;
            updatedBatteryText.text = "Max";
        }
    }

    public void ShowUpdatedRestoreEnergySpeed(int speed)
    {
        restoreEnergySpeedButton.interactable = true;
        updatedRestoreEnergySpeedText.text = $"{speed}/s";
        if (speed == 0)
        {
            restoreEnergySpeedButton.interactable = false;
            updatedRestoreEnergySpeedText.text = "Max";
        }
    }
    public void ShowAutoMiningCost(int cost)
    {
        autoMiningCostText.text = $"{cost:n0}";
        if (cost == 0)
        {
            autoMiningCostText.text = $"-";
        }
    }
    public void ShowAutoMiningState(bool isAvailable, bool isActive)
    {
        if (!isAvailable)
        {
            autoMiningButton.interactable = false;
            updatedAutoMiningText.text = $"Need 2 lvl";
        }

        if (isActive)
        {
            autoMiningButton.interactable = false;
            updatedAutoMiningText.text = $"Active";
            ShowAutoMiningCost(0);
        }
        else
        {
            autoMiningButton.interactable = true;
            updatedAutoMiningText.text = $"Buy";
        }

        autoMiningAvailableObject.SetActive(!isAvailable);
    }
}
