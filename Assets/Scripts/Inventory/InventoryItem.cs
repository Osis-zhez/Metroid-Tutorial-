using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    public ItemData itemData;
    public int stackSize = 0;
    public int maxStackSize;

    public InventoryItem(ItemData newItemData)
    {
        itemData = newItemData;
        AddStack(1);
    }

    public bool AddStack(int maxSlotSize)
    {
        if (stackSize < maxSlotSize)
        {
            stackSize ++;
            return true;
        }
        else
        {
            return false;
        }

    }
        
    public void RemoveStack() => stackSize --;
}
