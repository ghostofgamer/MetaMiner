using System.Collections.Generic;
using UnityEngine;

public class MergeScreenView : MonoBehaviour
{
    [SerializeField]
    private GameObject inventoryScreenObject;

    [SerializeField]
    private GameObject mergeScreenObject;

    private void OnEnable()
    {
        OpenMerge();
    }

    public void OpenInventory()
    {
        inventoryScreenObject.SetActive(true);
        mergeScreenObject.SetActive(false);
    }

    public void OpenMerge()
    {
        inventoryScreenObject.SetActive(false);
        mergeScreenObject.SetActive(true);
    }
}
