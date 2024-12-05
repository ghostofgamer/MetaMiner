using MetaMiners.Network;
using UnityEngine;

public class NewMetaBoxPopupPresenter : MonoBehaviour
{
    [SerializeField]
    private NewMetaBoxPopupModel model;

    [SerializeField]
    private NewMetaBoxPopupView view;

    private void Awake()
    {
        model.Box.Subscribe(box => view.ShowBox(box));
    }

    public void SetBox(BoxState boxState)
    {
        model.Box.Value = boxState;
    }

    public void OpenMetaBox()
    {
        NetworkManager.Instance.PostOpenBox(int.Parse(model.Box.Value.Id));
    }

    public void AddToInventory()
    {
        NetworkManager.Instance.PostGetProfile();
    }
}
