using System.Collections;
using UnityEngine;
using DG.Tweening;
using System;

public class BodyView : MonoBehaviour
{
    [SerializeField]
    private MineScreenView mineScreenView;

    [SerializeField]
    private UpgradeScreenView upgradeScreenView;

    [SerializeField]
    private InventoryScreenView inventoryScreenView;

    [SerializeField]
    private MergeScreenView mergeScreenView;

    [SerializeField]
    private DAOScreenView daoScreenView;

    [SerializeField]
    private EarnScreenView earnScreenView;

    [SerializeField]
    private MetaBoxOpenScreenView metaBoxOpenScreenView;

    [SerializeField]
    private RectTransform getMetaBoxTransform;

    private DateTime lastAdsShownTime = DateTime.MinValue;

    private CanvasGroup prevScreen;

    private CanvasGroup currentScreen;

    private void OnEnable()
    {
        StartCoroutine(ShowAdsOverlay());
    }

    public void ShowScreen(BodyModel.Screens screen)
    {
        // Отключаем текущий экран с анимацией исчезновения
        if (currentScreen != null)
        {
            prevScreen = currentScreen;
            prevScreen.DOFade(0, 0.2f).OnComplete(() => prevScreen.gameObject.SetActive(false));
        }


        // Определяем новый экран
        CanvasGroup nextScreen = GetScreenCanvasGroup(screen);

        if (nextScreen != null)
        {
            nextScreen.gameObject.SetActive(true);
            nextScreen.alpha = 0;
            nextScreen.DOFade(1, 0.3f);
            currentScreen = nextScreen;
        }

        if (screen != BodyModel.Screens.Mine)
        {
            CubeRendererPresenter.Instance.SetToDefaultRotation();
        }
    }

    private CanvasGroup GetScreenCanvasGroup(BodyModel.Screens screen)
    {
        switch (screen)
        {
            case BodyModel.Screens.Mine:
                return mineScreenView.GetComponent<CanvasGroup>();
            case BodyModel.Screens.Upgrade:
                return upgradeScreenView.GetComponent<CanvasGroup>();
            case BodyModel.Screens.Inventory:
                return inventoryScreenView.GetComponent<CanvasGroup>();
            case BodyModel.Screens.DAO:
                return daoScreenView.GetComponent<CanvasGroup>();
            case BodyModel.Screens.Earn:
                return earnScreenView.GetComponent<CanvasGroup>();
            case BodyModel.Screens.Merge:
                return mergeScreenView.GetComponent<CanvasGroup>();
            case BodyModel.Screens.OpenMetaBox:
                return metaBoxOpenScreenView.GetComponent<CanvasGroup>();
            default:
                return null;
        }
    }

    private IEnumerator ShowAdsOverlay()
    {
        while (true)
        {
            if (AppPresenter.Instance.BodyPresenter.MineScreenPresenter.GetAdsAvailable() > 0)
            {
                if ((DateTime.Now - lastAdsShownTime).TotalSeconds > UnityEngine.Random.Range(40, 60))
                {
                    getMetaBoxTransform.DOAnchorPosX(0, 2f);
                    lastAdsShownTime = DateTime.Now;
                }
            }
            yield return new WaitForSeconds(5f);
        }
    }

    public void HideAdsOverlay()
    {
        getMetaBoxTransform.DOAnchorPosX(400, 0.5f);
    }
}
