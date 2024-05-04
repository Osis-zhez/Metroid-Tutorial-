using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public event Action<InventoryItem> OnClearSlot;

    public List<InventoryItem> inventoryItemsList;
    public Dictionary<ItemData, InventoryItem> inventoryDictionary;

    [Header("Inventory UI")]
    [SerializeField] private Transform inventorySlosParent;

    private UI_ItemSlot[] itemSlot;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        inventoryItemsList = new List<InventoryItem>();
        inventoryDictionary = new Dictionary<ItemData, InventoryItem>();

        itemSlot = inventorySlosParent.GetComponentsInChildren<UI_ItemSlot>(); // Получаем в массив, все слоты из UI
    }

    private void UpdateSlotUI()
    {
        for (int i = 0; i < inventoryItemsList.Count; i++)
        {
            itemSlot[i].UpdateSlot(inventoryItemsList[i]); //Бежим по всем слотам и передаем туда InventoryItem
        }
    }

    public void AddItem(ItemData item)
    {
        if (inventoryItemsList.Count < 1)
        {
            InventoryItem newItem = new InventoryItem(item);
            inventoryItemsList.Add(newItem);
            UpdateSlotUI();
            Debug.Log("0");
        }
        else
        {
            for (int i = 0; i < inventoryItemsList.Count; i++)
            {
                if (inventoryItemsList[i].itemData == item)
                {
                    if (inventoryItemsList[i].AddStack(2))
                    {
                        Debug.Log("1");
                        UpdateSlotUI();
                        return;
                    }
                    else
                    {

                    }
                }
                else
                {
                    InventoryItem newItem1 = new InventoryItem(item);
                    inventoryItemsList.Add(newItem1);
                    Debug.Log("3");
                    UpdateSlotUI();
                    return;
                }
            }
            
            InventoryItem newItem = new InventoryItem(item);
            inventoryItemsList.Add(newItem);
            UpdateSlotUI();
            Debug.Log("2");
        }
        
        UpdateSlotUI();
    }

    public void RemoveItem(ItemData item)
    {
        for (int i = 0; i < inventoryItemsList.Count; i++)
        {
            if (inventoryItemsList[i].itemData == item)
            {
                if (inventoryItemsList[i].stackSize <= 1)
                {
                    // inventoryItemsList[i].RemoveStack();
                    inventoryItemsList[i].RemoveStack();
                    OnClearSlot?.Invoke(inventoryItemsList[i]);
                    inventoryItemsList.Remove(inventoryItemsList[i]);
                    break;
                }
                else if (inventoryItemsList[i].stackSize > 1)
                {
                    inventoryItemsList[i].RemoveStack();
                    
                    UpdateSlotUI();
                    break;
                }
            }
        }
        // if (inventoryDictionary.TryGetValue(item, out InventoryItem value))
        // {
        //     if (value.stackSize <= 1)
        //     {
        //         inventoryItemsList.Remove(value);
        //         inventoryDictionary.Remove(item);
        //     }
        //     else
        //     {
        //         value.RemoveStack();
        //     }
        // }

    }

    public List<InventoryItem> InventoryItemList()
    {
        return inventoryItemsList;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            ItemData newItem = inventoryItemsList[inventoryItemsList.Count - 1].itemData;

            RemoveItem(newItem);
        }
    }
}
