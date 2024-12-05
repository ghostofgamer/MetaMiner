using DG.Tweening;
using EasyButtons;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MetaBoxRendererView : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private RectTransform itemsParent;

    [SerializeField]
    private GameObject itemsPrefab;

    [SerializeField]
    private LayoutGroup layoutGroup;

    private List<Transform> items = new List<Transform>();
    private int lastItem = -1;

    public void ShowCards(int count)
    {
        Debug.Log($"Cards count: {count}");
        lastItem = -1;
        items.Clear();
        layoutGroup.enabled = true;

        for (int i = itemsParent.childCount - 1; i >= 0; i--)
        {
            ObjectPool.Instance.ReturnObject(itemsParent.GetChild(i).gameObject);
        }

        for (int i = 0; i < count; i++)
        {
            GameObject go = ObjectPool.Instance.GetObject(itemsPrefab);
            go.transform.SetParent(itemsParent, false);
            go.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            go.transform.localScale = Vector3.one;
            items.Add(go.transform);
        }
    }

    [Button]
    public void ShowNextCard(Action callback)
    {
        layoutGroup.enabled = false;

        lastItem++;

        if (lastItem < items.Count)
        {
            items[lastItem].transform.SetAsLastSibling();
            items[lastItem].transform.DOMove(target.position, 0.75f).SetEase(Ease.InOutQuad);
            items[lastItem].transform.DORotateQuaternion(target.rotation, 1f).SetEase(Ease.OutBounce);
            items[lastItem].GetComponent<RectTransform>().DOSizeDelta(target.GetComponent<RectTransform>().sizeDelta, 0.75f).SetEase(Ease.Linear);
            StartCoroutine(HideRotationAnimation(items[lastItem].transform, callback));
        }

        //if (lastItem - 1 >= 0)
        //{
        //    Vector3 pos = items[lastItem - 1].transform.position;
        //    pos.y -= 1;
        //    items[lastItem - 1].transform.SetAsLastSibling();
        //    items[lastItem - 1].transform.DOMove(pos, 0.5f).SetEase(Ease.InOutQuad);
        //    items[lastItem - 1].transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InOutQuad);
        //}
    }

    public IEnumerator HideRotationAnimation(Transform transform, Action callback)
    {
        yield return new WaitForSeconds(1.2f);

        Vector3 rotationTarget = new Vector3();
        rotationTarget.x = 0;
        rotationTarget.y = 90f;
        rotationTarget.z = -40f;
        transform.DOLocalRotate(rotationTarget, 0.5f).SetEase(Ease.InCubic).OnComplete(() =>
        {
            ObjectPool.Instance.ReturnObject(transform.gameObject);
            callback?.Invoke();
        });
    }

    public void SetState(bool open)
    {
        animator.SetBool("Open", open);
    }
}
