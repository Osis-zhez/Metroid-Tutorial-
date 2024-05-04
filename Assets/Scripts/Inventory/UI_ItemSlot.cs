using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemSlot : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private Color emptyColor;
    [SerializeField] private TextMeshProUGUI itemText;
    [SerializeField] private int maxSlotSize;

    public InventoryItem item;
   
    private void Start()
    {
        Inventory.Instance.OnClearSlot += Inventory_OnClearSlot;
    }

    private void Inventory_OnClearSlot(InventoryItem inventoryItem)
    {
        if (item == inventoryItem)
        {
            Debug.Log("11");
            if (item.stackSize < 1)
            {   
                Debug.Log("22");
                itemText.text = "";
                itemImage.color = emptyColor;
                item = null;
            }
        }
    }

    public void UpdateSlot(InventoryItem newItem)
    {
        item = newItem;

        itemImage.color = Color.white;
         
        if (item != null)
        {
            itemImage.sprite = item.itemData.icon;

            if (item.stackSize > 1)
            {
                itemText.text = item.stackSize.ToString();
            }
            else if (item.stackSize == 1)
            {
                itemText.text = "";
            }
        }
    }
}
