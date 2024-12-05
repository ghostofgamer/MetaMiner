using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomToggle : MonoBehaviour, IPointerClickHandler
{
    private CustomToggleGroup toggleGroup;

    [Header("References")]
    [SerializeField]
    private List<Graphic> targetGraphics = new List<Graphic>();

    [field: Header("States Settings")]
    [field: SerializeField]
    public bool IsInteractable { get; private set; } = false;
    [field: SerializeField]
    public bool IsOn { get; private set; } = false;
    [field: SerializeField]
    public Color ActiveColor { get; set; } = Color.white;

    [field: SerializeField]
    public List<GameObject> ActiveGameObjects { get; set; } = new List<GameObject>();

    [field: SerializeField]
    public Color InactiveColor { get; set; } = Color.white;

    [field: SerializeField]
    public List<GameObject> InactiveGameObjects { get; set; } = new List<GameObject>();

    [field: Header("Events")]
    [field: SerializeField]
    public UnityEvent OnActivate { get; private set; } = new UnityEvent();

    [field: SerializeField]
    public UnityEvent OnDeactivate { get; private set; } = new UnityEvent();

    private void Awake()
    {
        RefreshState();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!IsInteractable) return;

        SetActiveState(true);

        if (toggleGroup != null)
            toggleGroup.RefreshStates(this);
    }

    public void SetToggleGroup(CustomToggleGroup toggleGroup)
    {
        this.toggleGroup = toggleGroup;
    }

    public void SetActiveState(bool isOn)
    {
        if (isOn) OnActivate?.Invoke();
        if (!isOn) OnDeactivate?.Invoke();

        IsOn = isOn;
        RefreshState();
    }

    private void RefreshState()
    {
        foreach (Graphic graphic in targetGraphics)
        {
            if (IsOn)
                graphic.color = ActiveColor;
            else
                graphic.color = InactiveColor;
        }

        foreach (GameObject go in ActiveGameObjects)
        {
            go.SetActive(IsOn);
        }

        foreach (GameObject go in InactiveGameObjects)
        {
            go.SetActive(!IsOn);
        }
    }
}
