using MetaMiners.Network;
using UnityEngine;

public class FarmPopupPresenter : MonoBehaviour
{
    [SerializeField]
    private FarmPopupModel model;

    [SerializeField]
    private FarmPopupView view;

    private void OnEnable()
    {
        CubeRendererPresenter.Instance.SetToDefaultRotation();
        CubeRendererPresenter.Instance.SetLevel(model.Card.Value.Level);
        CubeRendererPresenter.Instance.SetRarity(model.Card.Value.Type);
    }

    private void OnDisable()
    {
        CubeRendererPresenter.Instance.SetLevel(AppPresenter.Instance.BodyPresenter.MineScreenPresenter.model.FarmLevel);
        CubeRendererPresenter.Instance.SetRarity(AppPresenter.Instance.BodyPresenter.MineScreenPresenter.model.FarmRarity);
    }

    private void Awake()
    {
        model.Card.Subscribe(card => view.ShowCard(card));
    }

    public void SetCard(CardState cardState)
    {
        model.Card.Value = cardState;
    }

    public void SelectCard()
    {
        NetworkManager.Instance.PostSetActiveCard(int.Parse(model.Card.Value.Id));
    }
}
