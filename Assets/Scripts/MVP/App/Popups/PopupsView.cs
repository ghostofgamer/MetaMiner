using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PopupsView : MonoBehaviour
{
    [SerializeField]
    private PopupsPresenter popupsPresenter;

    [SerializeField]
    private Image backgroundImage;

    [SerializeField]
    private float animationDuration = 0.5f;

    public void EnableBackground(bool enable)
    {
        if (enable)
        {
            backgroundImage.DOFade(0.5f, animationDuration).SetEase(Ease.OutQuad); // Плавное появление фона
        }
        else
        {
            backgroundImage.DOFade(0f, animationDuration).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                backgroundImage.enabled = false; // Выключить фон после анимации
            });
        }

        if (enable)
        {
            backgroundImage.enabled = true; // Включить фон перед анимацией
        }
    }

    public void ShowPopup(PopupsModel.Popups popup)
    {
        if (popup == PopupsModel.Popups.None)
        {
            HidePopupsImmediate();
            EnableBackground(false);
            return;
        }

        HidePopupsWithoutBackground(); // Убираем все попапы, но оставляем фон

        if (popup != PopupsModel.Popups.None) EnableBackground(true);

        RectTransform popupRect = GetPopup(popup);

        if (popupRect != null)
        {
            popupRect.gameObject.SetActive(true);
            popupRect.anchoredPosition = new Vector2(0, -Screen.height); // Стартовая позиция (ниже экрана)
            popupRect.DOAnchorPos(Vector2.zero, animationDuration).SetEase(Ease.OutBack); // Анимация выезда вверх

            // Перемещаем объект в самый конец списка, чтобы он был сверху
            popupRect.SetSiblingIndex(popupRect.parent.childCount - 1);
        }
    }

    public void HidePopups()
    {
        foreach (var popupPresenter in GetAllPopupPresenters())
        {
            var rectTransform = popupPresenter.GetComponent<RectTransform>();
            if (rectTransform.gameObject.activeSelf)
            {
                rectTransform.DOAnchorPos(new Vector2(0, -Screen.height), animationDuration).SetEase(Ease.InBack)
                    .OnComplete(() => rectTransform.gameObject.SetActive(false));
            }
        }

        EnableBackground(false);
    }

    private void HidePopupsImmediate()
    {
        foreach (var popupPresenter in GetAllPopupPresenters())
        {
            popupPresenter.gameObject.SetActive(false);
        }
    }

    private void HidePopupsWithoutBackground()
    {
        foreach (var popupPresenter in GetAllPopupPresenters())
        {
            var rectTransform = popupPresenter.GetComponent<RectTransform>();
            if (rectTransform.gameObject.activeSelf)
            {
                rectTransform.DOAnchorPos(new Vector2(0, -Screen.height), animationDuration).SetEase(Ease.InBack)
                    .OnComplete(() => rectTransform.gameObject.SetActive(false));
            }
        }
    }

    private IEnumerable<Transform> GetAllPopupPresenters()
    {
        yield return popupsPresenter.DAOInfoPopupPresenter.transform;
        yield return popupsPresenter.InviteFriendPopupPresenter.transform;
        yield return popupsPresenter.TaskPopupPresenter.transform;
        yield return popupsPresenter.FarmPopupPresenter.transform;
        yield return popupsPresenter.MergeProbabilityPopupPresenter.transform;
        yield return popupsPresenter.NewFarmPopupPresenter.transform;
        yield return popupsPresenter.MetaBoxPopupPresenter.transform;
        yield return popupsPresenter.AdsPopupPresenter.transform;
        yield return popupsPresenter.NewMetaBoxPopupPresenter.transform;
        yield return popupsPresenter.WalletPopupPresenter.transform;
    }

    private RectTransform GetPopup(PopupsModel.Popups popup)
    {
        RectTransform popupRect = null;

        switch (popup)
        {
            case PopupsModel.Popups.DAOInfo:
                popupRect = popupsPresenter.DAOInfoPopupPresenter.GetComponent<RectTransform>();
                break;
            case PopupsModel.Popups.InviteFriend:
                popupRect = popupsPresenter.InviteFriendPopupPresenter.GetComponent<RectTransform>();
                break;
            case PopupsModel.Popups.Task:
                popupRect = popupsPresenter.TaskPopupPresenter.GetComponent<RectTransform>();
                break;
            case PopupsModel.Popups.Farm:
                popupRect = popupsPresenter.FarmPopupPresenter.GetComponent<RectTransform>();
                break;
            case PopupsModel.Popups.MergeProbability:
                popupRect = popupsPresenter.MergeProbabilityPopupPresenter.GetComponent<RectTransform>();
                break;
            case PopupsModel.Popups.NewFarm:
                popupRect = popupsPresenter.NewFarmPopupPresenter.GetComponent<RectTransform>();
                break;
            case PopupsModel.Popups.MetaBox:
                popupRect = popupsPresenter.MetaBoxPopupPresenter.GetComponent<RectTransform>();
                break;
            case PopupsModel.Popups.Ads:
                popupRect = popupsPresenter.AdsPopupPresenter.GetComponent<RectTransform>();
                break;
            case PopupsModel.Popups.NewMetaBox:
                popupRect = popupsPresenter.NewMetaBoxPopupPresenter.GetComponent<RectTransform>();
                break;
            case PopupsModel.Popups.Wallet:
                popupRect = popupsPresenter.WalletPopupPresenter.GetComponent<RectTransform>();
                break;
        }

        return popupRect;
    }
}
