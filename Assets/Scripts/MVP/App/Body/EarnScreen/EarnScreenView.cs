using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static PlayerInRatingItemModel;

public class EarnScreenView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI friends;

    [SerializeField]
    private TextMeshProUGUI earn;

    [SerializeField]
    private RectTransform tasksParent;

    [SerializeField]
    private RectTransform ratingParent;

    [SerializeField]
    private GameObject taskPrefab;

    [SerializeField]
    private GameObject ratingItemPrefab;

    [SerializeField]
    private GameObject tasksPanel;

    [SerializeField]
    private GameObject ratingPanel;

    public void ShowFriendsCount(int friendCount)
    {
        friends.text = $"{friendCount} Friends";
    }

    public void ShowEarnAmount(int earnAmount)
    {
        string formattedEarnAmount = string.Format("{0:N0}", earnAmount).Replace(",", " ");
        earn.text = $"+{formattedEarnAmount}";
    }

    public void ShowTasks(List<TaskItemModel.TaskState> tasks)
    {
        for (int i = tasksParent.childCount - 1; i >= 0; i--)
        {
            ObjectPool.Instance.ReturnObject(tasksParent.GetChild(i).gameObject);
        }

        for (int i = 0; i < tasks.Count; i++)
        {
            GameObject go = ObjectPool.Instance.GetObject(taskPrefab);
            go.transform.SetParent(tasksParent, false);
            go.transform.localScale = Vector3.one;
            var presenter = go.GetComponent<TaskItemPresenter>();
            presenter.SetCaption(tasks[i].Caption);
            presenter.SetCompleted(tasks[i].Completed);
            presenter.SetIcon(tasks[i].Icon);
            presenter.SetReward(tasks[i].Reward);
            presenter.SetLink(tasks[i].Link);
        }
    }

    public void ClickOnSwitchTaskItem()
    {
        tasksPanel.SetActive(true);
        ratingPanel.SetActive(false);
    }

    public void ClickOnSwitchRatingItem()
    {
        tasksPanel.SetActive(false);
        ratingPanel.SetActive(true);
    }

    public void ShowRating(List<PlayerInRatingState> players)
    {
        for (int i = ratingParent.childCount - 1; i >= 0; i--)
        {
            ObjectPool.Instance.ReturnObject(ratingParent.GetChild(i).gameObject);
        }

        for (int i = 0; i < players.Count; i++)
        {
            GameObject go = ObjectPool.Instance.GetObject(ratingItemPrefab);
            go.transform.SetParent(ratingParent, false);
            go.transform.localScale = Vector3.one;
            var presenter = go.GetComponent<PlayerInRatingItemPresenter>();
            presenter.SetNickname(players[i].Nickname);
            presenter.SetFriendInvited(players[i].FriendsInvited);
            presenter.SetRating(i + 1);
        }
    }
}
