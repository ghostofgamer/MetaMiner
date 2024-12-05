using System;
using UnityEngine;

public class MetaBoxRendererPresenter : MonoBehaviour
{
    [SerializeField]
    private MetaBoxRendererModel model;

    [SerializeField]
    private MetaBoxRendererView view;

    private void Awake()
    {
        model.Open.Subscribe(open => view.SetState(open));
        model.CardsCount.Subscribe(count => view.ShowCards(count));
    }

    private void OnDisable()
    {
        view.SetState(false);
    }

    public void SetCardsCount(int count)
    {
        Debug.Log("SetCardsCount(int count)");
        model.CardsCount.SetValueWithoutNotify(count);
        model.CardsCount.Notify();
        view.SetState(false);
    }

    public void ShowOpenAnimation()
    {
        view.SetState(true);
    }

    public void ShowIdleAnimation()
    {
        view.SetState(false);
    }

    public void ShowNextCard(Action callback)
    {
        view.ShowNextCard(callback);
    }
}
