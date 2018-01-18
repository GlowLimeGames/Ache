using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItem : MonoBehaviour {

    public int itemID;

    public bool interactable;

    private void OnMouseUpAsButton()
    {
        if (!interactable)
        {
            return;
        }

        AddToInventory();
    }

    public void AddToInventory()
    {
        if (Inventory.Instance.IsFull)
        {
            return;
        }

        Inventory.Instance.AddItem(itemID);
        Destroy(this.gameObject);
    }
}
