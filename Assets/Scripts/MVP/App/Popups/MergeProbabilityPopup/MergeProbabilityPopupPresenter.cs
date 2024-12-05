using System.Collections.Generic;
using UnityEngine;

public class MergeProbabilityPopupPresenter : MonoBehaviour
{
    [SerializeField]
    private MergeProbabilityPopupModel model;

    [SerializeField]
    private MergeProbabilityPopupView view;

    private void Awake()
    {
        model.CardsToMerge.Subscribe(value => view.ShowCards(value));
        model.NewCard.Subscribe(value => view.ShowNewCard(value));
        model.Probability.Subscribe(value => view.ShowProbability(value));
    }

    public void SetCardsToMerge(List<CardState> cardStates)
    {
        model.CardsToMerge.Value = cardStates;
    }

    public void SetNewCard(CardState cardState)
    {
        model.NewCard.Value = cardState;
    }

    public void SetProbability(int value)
    {
        model.Probability.Value = value;
    }
}
