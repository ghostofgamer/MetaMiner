using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DAOScreenView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI nameText, peopleCountText, summaryBalanceText, percentSummaryBalanceText;

    [SerializeField]
    private GameObject topObject, daoObject;

    [SerializeField]
    private RectTransform topParent;

    [SerializeField]
    private GameObject topDAOItemPrefab;

    [SerializeField]
    private Image daoPictureImage;

    [SerializeField]
    private AspectRatioFitter daoPictureFitter;

    public void ShowTopDAO(List<DAOState> daoTop)
    {
        for (int i = topParent.childCount - 1; i >= 0; i--)
        {
            ObjectPool.Instance.ReturnObject(topParent.GetChild(i).gameObject);
        }

        foreach (var dao in daoTop)
        {
            GameObject go = ObjectPool.Instance.GetObject(topDAOItemPrefab);
            go.transform.SetParent(topParent, false);
            go.transform.localScale = Vector3.one;
            var itemPresenter = go.GetComponent<DAOItemPresenter>();
            itemPresenter.SetDAO(dao);
        }
    }

    public void ShowCurrentDAO(DAOState state)
    {
        nameText.text = state.Name;
        peopleCountText.text = state.PeopleCount;
        summaryBalanceText.text = state.SummaryBalance;
        percentSummaryBalanceText.text = state.PercentSummaryBalance;
    }

    public void ShowDAOPicture(Sprite daoPicture)
    {
        if (daoPicture == null) return;

        daoPictureImage.sprite = daoPicture;
        daoPictureFitter.aspectMode = AspectRatioFitter.AspectMode.EnvelopeParent;
        daoPictureFitter.aspectRatio = daoPicture.rect.width / daoPicture.rect.height;
    }

    public void ChangeState(bool hasDAO)
    {
        daoObject.SetActive(hasDAO);
        topObject.SetActive(!hasDAO);
    }
}
