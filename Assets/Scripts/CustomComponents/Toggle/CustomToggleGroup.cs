using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomToggleGroup : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private List<CustomToggle> toggles = new List<CustomToggle>();

    [field: Header("Events")]
    [field: SerializeField]
    public UnityEvent<CustomToggle> OnChange { get; private set; } = new UnityEvent<CustomToggle>();

    private void Awake()
    {
        foreach (var toggle in toggles)
        {
            toggle.SetToggleGroup(this);
        }
    }

    public void AddButtonToggle(CustomToggle buttonToggle)
    {
        toggles.Add(buttonToggle);
    }

    public void RefreshStates(CustomToggle buttonToggle)
    {
        bool isOn = buttonToggle.IsOn;

        foreach (var toggle in toggles)
        {
            toggle.SetActiveState(toggle == buttonToggle && isOn);
        }

        if (!isOn)
        {
            toggles[0].SetActiveState(true);
        }

        if (isOn) OnChange?.Invoke(buttonToggle);
    }
}
