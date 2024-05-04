using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private ItemData itemData;

    private void OnValidate() 
    {
        GetComponent<SpriteRenderer>().sprite = itemData.icon;
        gameObject.name = "Item object" + itemData.itemName;
    }


    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.GetComponent<PlayerHealthController>() != null)
        {
            Inventory.Instance.AddItem(itemData);
            Destroy(gameObject);
        }
    }
}