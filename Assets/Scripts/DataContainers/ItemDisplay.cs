using UnityEngine;

public class ItemDisplay : MonoBehaviour
{
    public ItemData itemData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Item "+itemData.name + " Value: " + itemData.value);
    }

    
}
