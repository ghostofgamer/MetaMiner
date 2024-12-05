using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskItemPresenter : MonoBehaviour
{
    [SerializeField]
    private TaskItemModel model;

    [SerializeField]
    private TaskItemView view;

    private void Awake()
    {
        model.Caption.Subscribe(caption => view.ShowCaption(caption));
        model.Completed.Subscribe(isCompleted => view.ShowIsCompleted(isCompleted));
        model.Icon.Subscribe(icon => view.ShowIcon(icon));
        model.Reward.Subscribe(reward => view.ShowReward(reward));
    }

    public void SetCaption(string caption)
    {
        model.Caption.Value = caption;
    }

    public void SetCompleted(bool isCompleted)
    {
        model.Completed.Value = isCompleted;
    }

    public void SetIcon(string icon)
    {
        model.Icon.Value = icon;
    }

    public void SetReward(string reward)
    {
        model.Reward.Value = reward;
    }

    public void SetLink(string url)
    {
        model.Link.Value = url;
    }

    public void OpenPopup()
    {
        AppPresenter.Instance.PopupsPresenter.TaskPopupPresenter.SetCaption(model.Caption.Value);
        AppPresenter.Instance.PopupsPresenter.TaskPopupPresenter.SetIcon(model.Icon.Value);
        AppPresenter.Instance.PopupsPresenter.TaskPopupPresenter.SetLink(model.Link.Value);

        AppPresenter.Instance.PopupsPresenter.ShowPopup(PopupsModel.Popups.Task);
    }
}
