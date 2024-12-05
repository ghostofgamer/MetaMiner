using MetaMiners.Network;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EarnScreenPresenter : MonoBehaviour
{
    [SerializeField]
    private EarnScreenModel model = new EarnScreenModel();

    [SerializeField]
    private EarnScreenView view;

    private void Awake()
    {
        model.FriendsCount.Subscribe(friend => view.ShowFriendsCount(friend));
        model.EarnAmount.Subscribe(amount => view.ShowEarnAmount(amount));
    }

    public void SetFriendCount(int friendCount)
    {
        model.FriendsCount.Value = friendCount;
    }

    public void SetEarnAmount(int amount)
    {
        model.EarnAmount.Value = amount;
    }

    public void ShowTasks(List<Responses.MissionItem> tasks)
    {
        var taskList = tasks.Select(task => new TaskItemModel.TaskState(task.Icon, task.Caption, task.Reward, task.Link, task.Completed)).ToList();

        view.ShowTasks(taskList);
    }

    public void ShowRating(List<Responses.TopPlayer> players)
    {
        var taskList = players.Select(player => new PlayerInRatingItemModel.PlayerInRatingState(player.Username, player.ReferralCount)).ToList();

        view.ShowRating(taskList);
    }

    public void OpenInvitePopup()
    {
        AppPresenter.Instance.PopupsPresenter.ShowPopup(PopupsModel.Popups.InviteFriend);
    }
}
