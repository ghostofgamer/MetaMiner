using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfilePresenter : MonoBehaviour
{
    [SerializeField]
    private ProfileModel model;

    [SerializeField]
    private ProfileView view;

    private void Awake()
    {
        model.Nickname.Subscribe(nickname => view.ShowNickname(nickname));
        model.Status.Subscribe(status => view.ShowStatus(status));
        model.ProfilePicture.Subscribe(image => view.ShowProfilePicture(image));
    }

    public void SetNickname(string nickname)
    {
        model.Nickname.Value = nickname;
    }

    public void SetStatus(int status)
    {
        model.Status.Value = status;
    }

    public void SetProfilePicture(Sprite image)
    {
        model.ProfilePicture.Value = image;
    }
}
