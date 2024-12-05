using MetaMiners.Network;
using UnityEngine;

public class TaskPopupPresenter : MonoBehaviour
{
    [SerializeField]
    private TaskPopupModel model;

    [SerializeField]
    private TaskPopupView view;

    private void Awake()
    {
        model.Caption.Subscribe(caption => view.ShowCaption(caption));
        model.IconCode.Subscribe(icon => view.ShowIcon(icon));
        model.IconCode.Subscribe(icon => view.ShowHeader(icon));
    }

    public void SetCaption(string caption)
    {
        model.Caption.Value = caption;
    }

    public void SetIcon(string icon)
    {
        model.IconCode.Value = icon;
    }

    public void SetLink(string url)
    {
        model.Link.Value = url;
    }

    public void OpenLink()
    {
        if (!string.IsNullOrWhiteSpace(model.Link.Value))
        {
            Application.OpenURL(model.Link);
        }
        else
        {
            AppPresenter.Instance.PopupsPresenter.ShowPopup(PopupsModel.Popups.InviteFriend);
        }
    }

    public void CheckTask()
    {
        NetworkManager.Instance.PostGetProfile();

        // Network
    }
}
