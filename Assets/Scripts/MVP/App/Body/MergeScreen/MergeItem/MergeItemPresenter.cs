using UnityEngine;

public class MergeItemPresenter : MonoBehaviour
{
    [SerializeField]
    private MergeItemModel model;

    [SerializeField]
    private MergeItemView view;

    private void Awake()
    {
        model.Card.Subscribe(card => view.ShowCard(card));
        model.Card.Value = null;
        HideError();
    }

    public void SetCard(CardState cardState)
    {
        model.Card.Value = cardState;
    }

    public CardState GetCard()
    {
        return model.Card.Value;
    }

    public void ShowError(bool showText, string text)
    {
        view.ShowError(showText, text);
    }

    public void HideError()
    {
        view.HideError();
    }

    public void OnClick()
    {
        if (model.Card.Value != null)
        {
            model.Card.Value = null;
        }
        else
        {
            AppPresenter.Instance.BodyPresenter.MergeScreenPresenter.OpenInventory();
        }
    }
}
