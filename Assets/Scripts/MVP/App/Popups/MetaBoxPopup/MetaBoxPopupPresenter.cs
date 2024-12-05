using MetaMiners.Network;
using UnityEngine;

public class MetaBoxPopupPresenter : MonoBehaviour
{
    [SerializeField]
    private MetaBoxPopupModel model;

    [SerializeField]
    private MetaBoxPopupView view;

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
}
