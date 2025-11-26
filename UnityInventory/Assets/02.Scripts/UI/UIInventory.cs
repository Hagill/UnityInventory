using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI curInventoryText;
    [SerializeField] private TextMeshProUGUI maxInventoryText;
    [Header("인벤토리 슬롯")]
    [SerializeField] private ItemSlot slotPrefab;
    [SerializeField] private Transform slotParent;

    public List<ItemSlot> itemSlots = new List<ItemSlot>();

    [SerializeField] private GameObject equipPopUp;
    [SerializeField] private Button yesBtn;
    [SerializeField] private Button noBtn;
    [SerializeField] private TextMeshProUGUI equipPopupText;

    private InventorySlot selectedSlot;

    private void OnEnable()
    {  
        if(GameManager.Instance != null && GameManager.Instance.player != null)
        {
            UpdateInventoryUI(GameManager.Instance.player.Inventory);
        }
        
    }

    private void Awake()
    {
        yesBtn.onClick.AddListener(OnClickEquipYes);
        noBtn.onClick.AddListener(OnClickEquipNo);

        equipPopUp.SetActive(false);
    }
    private void Start()
    {
        InitInventoryUI(120);
        UpdateInventoryUI(GameManager.Instance.player.Inventory);
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
            newSlot.SetSlot(new InventorySlot(null));
            itemSlots.Add(newSlot);
        }
    }

    public void UpdateInventoryUI(List<InventorySlot> inventorySlots)
    {
        if (curInventoryText != null)
        {
            curInventoryText.text = $"{inventorySlots.Count}";
        }
        if(maxInventoryText != null)
        {
            maxInventoryText.text = $"/ {itemSlots.Count}";
        }

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
        equipPopUp.SetActive(true);
    }

    void OnClickEquipYes()
    {
        if(selectedSlot != null)
        {
            Character player = GameManager.Instance.player;
            if (player != null)
            {
                if (selectedSlot.isEquipped)
                {
                    player.UnequipItem(selectedSlot);
                    equipPopUp.SetActive(false);
                }
                else
                {
                    player.EquipItem(selectedSlot);
                    equipPopUp.SetActive(false);
                }
            }
            else
            {
                Debug.LogError("GameManager의 Player 인스턴스를 찾을 수 없음");
            }

        }
    }

    void OnClickEquipNo()
    {
        equipPopUp.SetActive(false);
    }
}
