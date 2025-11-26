using System;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("기초 능력치")]
    [SerializeField] private int BaseHp = 100;
    [SerializeField] private int BaseAttackPoint = 10;
    [SerializeField] private int BaseDefencePoint = 5;

    public int Level { get; private set; }
    public string Name { get; private set; }
    public int Gold { get; private set; }
    public int Exp { get; private set; }
    public int MaxExp { get; private set; }
    public int curHp {  get; private set; }
    public int Critical { get; private set; }

    public List<InventorySlot> Inventory = new List<InventorySlot>();

    public event Action OnCharacterStatusChanged;

    private void NotifyStatusChanged()
    {
        OnCharacterStatusChanged?.Invoke();
    }

    public int curAttackPoint
    {
        get
        {
            int sum = BaseAttackPoint;
            for (int i =0; i < Inventory.Count; i++)
            {
                if (Inventory[i].isEquipped && Inventory[i].ItemData != null && Inventory[i].ItemData.type == ItemType.Weapon)
                {
                    sum += Inventory[i].ItemData.value;
                }
            }
            return sum;
        }
    }

    public int curDefencePoint
    {
        get
        {
            int sum = BaseDefencePoint;
            for (int i = 0; i < Inventory.Count; i++)
            {
                if (Inventory[i].isEquipped && Inventory[i].ItemData != null && Inventory[i].ItemData.type == ItemType.Armor)
                {
                    sum += Inventory[i].ItemData.value;
                }
            }
            return sum;
        }
    }

    public void AddExp(int exp)
    {
        Exp += exp;
        CheckLevelUp();

        NotifyStatusChanged();
    }

    private bool CheckLevelUp()
    {
        bool levelUp = false;
        while(Exp >= MaxExp)
        {
            Exp -= MaxExp;
            LevelUp();
            levelUp = true;
        }
        return levelUp;
    }

    public void LevelUp()
    {
        Level++;
        BaseHp += 10;
        curHp = BaseHp;
        BaseAttackPoint += 2;
        BaseDefencePoint += 1;
        MaxExp = Level * 10;

        // 레벨 업 버튼을 눌렀을 때 필요
        NotifyStatusChanged();
    }

    public void SetData(string characterName)
    {
        Name = characterName;
        Level = 1;
        Gold = 25000;
        Exp = 6;
        MaxExp = Level * 10;
        Critical = 25;
        curHp = BaseHp;

        NotifyStatusChanged();
    }

    public void EquipItem(InventorySlot slotToEquip)
    {
        if (slotToEquip == null || slotToEquip.ItemData == null) return;

        foreach (var slot in Inventory)
        {
            if(slot.isEquipped && slot.ItemData != null && slot.ItemData.type == slotToEquip.ItemData.type)
            {
                slot.isEquipped = false;
                Debug.Log($"{slot.ItemData.itemName} 장착해제");
            }
        }
        slotToEquip.isEquipped = true;

        NotifyStatusChanged();
    }

    public void UnequipItem(InventorySlot slotToUnequip)
    {
        if (slotToUnequip == null || slotToUnequip.ItemData == null) return;
        slotToUnequip.isEquipped = false;
        Debug.Log($"{slotToUnequip.ItemData.itemName} 장착해제");

        NotifyStatusChanged();
    }
}
