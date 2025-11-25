using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Image itemImg;
    [SerializeField] private GameObject equip;
    [SerializeField] private Button slotBtn;

    private InventorySlot inventorySlot;

    private UIInventory uiInventory;

    private void Awake()
    {
        if (slotBtn != null)
        {
            slotBtn.onClick.AddListener(OnClickSlot);
        }
    }

    private void Start()
    {
        uiInventory = GetComponentInParent<UIInventory>();
    }

    public void SetSlot(InventorySlot slot)
    {
        inventorySlot = slot;

        if (slot.ItemData != null)
        {
            itemImg.sprite = slot.ItemData.itemImg;
            itemImg.color = Color.white;
        }
        else
        {
            itemImg.sprite = null;
            itemImg.color = Color.clear;
        }

        equip.SetActive(slot.isEquipped);
    }

    public void OnClickSlot()
    {
        uiInventory.ShowEquipPopup(inventorySlot);

    }
}
