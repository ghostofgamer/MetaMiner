using UnityEngine;

public class PlayerInRatingItemPresenter : MonoBehaviour
{
    [SerializeField]
    private PlayerInRatingItemModel model;

    [SerializeField]
    private PlayerInRatingItemView view;

    private void Awake()
    {
        model.Nickname.Subscribe(nickname => view.ShowNickname(nickname));
        model.Rating.Subscribe(rating => view.ShowTopRating(rating));
        model.ProfilePicture.Subscribe(picture => view.ShowProfilePicture(picture));
        model.FriendsInvited.Subscribe(friends => view.ShowFriendsInvitedCount(friends));
    }

    public void SetNickname(string nickname)
    {
        model.Nickname.Value = nickname;
    }

    public void SetRating(int rating)
    {
        model.Rating.Value = rating;
    }

    public void SetProfilePicture(Sprite picture)
    {
        model.ProfilePicture.Value = picture;
    }

    public void SetFriendInvited(int friends)
    {
        model.FriendsInvited.Value = friends;
    }
}
