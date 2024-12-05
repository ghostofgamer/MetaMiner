using UnityEngine;

public class BoxInventoryPresenter : MonoBehaviour
{
    [SerializeField]
    private BoxInventoryModel model;

    [SerializeField]
    private BoxInventoryView view;

    private void Awake()
    {
        model.Box.Subscribe(box => view.ShowBox(box));
    }

    public void SetBox(BoxState boxState)
    {
        model.Box.Value = boxState;
    }

    public void OpenPopup()
    {
        AppPresenter.Instance.PopupsPresenter.MetaBoxPopupPresenter.SetBox(model.Box);
        AppPresenter.Instance.PopupsPresenter.ShowPopup(PopupsModel.Popups.MetaBox);
    }
}
