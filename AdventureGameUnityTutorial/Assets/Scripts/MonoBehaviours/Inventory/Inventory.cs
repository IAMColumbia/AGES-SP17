using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Inventory : MonoBehaviour {

    public Image[] ItemImages = new Image[NumItemSlots];
    public Item[] Items = new Item[NumItemSlots];

    public const int NumItemSlots = 4;

    public void AddItem(Item itemToAdd)
    {
        for (int i = 0; i < Items.Length; i++)
        {
            if(Items[i] == null)
            {
                Items[i] = itemToAdd;
                ItemImages[i].sprite = itemToAdd.sprite;
                ItemImages[i].enabled = true;
                return;
            }
        }
    }

    public void RemoveItem (Item itemToRemove)
    {
        for (int i = 0; i < Items.Length; i++)
        {
            if(Items[i] == itemToRemove)
            {
                Items[i] = null;
                ItemImages[i].sprite = null;
                ItemImages[i].enabled = false;
                return;
            }
        }
    }
}
