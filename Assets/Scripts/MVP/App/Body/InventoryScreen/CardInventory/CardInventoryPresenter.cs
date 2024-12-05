using UnityEngine;

public class CardInventoryPresenter : MonoBehaviour
{
    [SerializeField]
    private CardInventoryModel model;

    [SerializeField]
    private CardInventoryView view;

    private void Awake()
    {
        model.Card.Subscribe(card => view.ShowCard(card));
    }

    public void SetCard(CardState cardState)
    {
        model.Card.Value = cardState;
    }

    public void OpenPopup()
    {
        AppPresenter.Instance.PopupsPresenter.FarmPopupPresenter.SetCard(model.Card);
        AppPresenter.Instance.PopupsPresenter.ShowPopup(PopupsModel.Popups.Farm);
    }

    public void SelectItem()
    {
        AppPresenter.Instance.BodyPresenter.MergeScreenPresenter.SelectItem(model.Card);
    }
}
