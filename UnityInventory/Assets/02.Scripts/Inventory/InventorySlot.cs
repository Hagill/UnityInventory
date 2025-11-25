using System;
using UnityEngine;

[Serializable]
public class InventorySlot
{
    public ItemData ItemData;
    public bool isEquipped;

    public InventorySlot(ItemData itemData)
    {
        ItemData = itemData;
        isEquipped = false;
    }
}
