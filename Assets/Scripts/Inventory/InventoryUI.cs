using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private UI_ItemSlot[] uI_ItemSlots;

    private void Awake()
    {
        uI_ItemSlots = GetComponentsInChildren<UI_ItemSlot>();
    }

    private void Start()
    {

    }

    public void UpdateSlots()
    {
        foreach (UI_ItemSlot itemSlot in uI_ItemSlots)
        {
            // itemSlot.UpdateSlot
        }
    }
}
