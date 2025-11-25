using UnityEngine;

public enum ItemType
{
    Weapon,
    Armor,
    Default,
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string itemName;
    public Sprite itemImg;
    public ItemType type;
    public int value;
}
