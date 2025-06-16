using UnityEngine;

[CreateAssetMenu(fileName ="New Item",menuName ="Inventory/Item")]
public class ItemData:ScriptableObject
{
    public string itemName;
    public Sprite icon;
    [Range(0,100)]
    public int value;

  
}
