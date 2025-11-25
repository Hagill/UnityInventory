using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    [Header("인벤토리 슬롯")]
    [SerializeField] private ItemSlot slotPrefab;
    [SerializeField] private Transform slotParent;

    public List<ItemSlot> itemSlots = new List<ItemSlot>();

    [SerializeField] private GameObject equipPopUp;
    [SerializeField] private Button yesBtn;
    [SerializeField] private Button noBtn;
    [SerializeField] private TextMeshProUGUI equipPopupText;

    private InventorySlot selectedSlot;

    private void Awake()
    {
        yesBtn.onClick.AddListener(OnClickEquipYes);
        noBtn.onClick.AddListener(OnClickEquipNo);

        equipPopUp.SetActive(false);
    }
    private void Start()
    {
        InitInventoryUI(20);
    }

    private void InitInventoryUI(int slotCount)
    {
        foreach (ItemSlot slot in itemSlots)
        {
            Destroy(slot.gameObject);
        }
        itemSlots.Clear();

        for (int i = 0; i < slotCount; i++)
        {
            ItemSlot newSlot = Instantiate(slotPrefab, slotParent);
            itemSlots.Add(newSlot);
        }
    }

    public void UpdateInventoryUI(List<InventorySlot> inventorySlots)
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if(i < inventorySlots.Count)
            {
                itemSlots[i].SetSlot(inventorySlots[i]);
            }
            else
            {
                itemSlots[i].SetSlot(new InventorySlot(null));
            }
        }
    }

    public void ShowEquipPopup(InventorySlot slot)
    {
        selectedSlot = slot;

        if(slot.isEquipped) 
        {
            equipPopupText.text = $"{slot.ItemData.itemName}을/를 해제 하시겠습니까?";
        }
        else
        {
            equipPopupText.text = $"{slot.ItemData.itemName}을/를 장착 하시겠습니까?";
        }
    }

    void OnClickEquipYes()
    {
        if(selectedSlot != null)
        {
            selectedSlot.isEquipped = !selectedSlot.isEquipped;

        }
    }

    void OnClickEquipNo()
    {
        equipPopUp.SetActive(false);
    }
}
