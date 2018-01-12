using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;


[Serializable]
public class Item
{
    public int iD;
    public Sprite image;
    public string type;
    public int damage;

    public Item(Sprite image, string type, int iD, int damage)
    {
        this.image = image;
        this.type = type;
        this.iD = iD;
        this.damage = damage;
    }
}

public class Inventory : MonoBehaviour {

    public List<Item> ItemData = new List<Item>();

    public List<GameObject> InventorySlots;
    List<Item> CurrentItems = new List<Item>();
    public int maxItems = 5;

    public Item selected;
    public int selectedSlot;

    public Transform inventoryBar;

    private void Start()
    {
        AddItem(ItemData[0]);
        AddItem(ItemData[1]);
    }

    public void AddItem(Item item)
    {
        if (CurrentItems.Count > maxItems)
        {
            return;
        }
        GameObject currentSlot = InventorySlots[CurrentItems.Count];

        currentSlot.transform.GetChild(0).GetComponent<Image>().sprite = item.image;
        currentSlot.GetComponent<Button>().onClick.AddListener(delegate () { Select(item, currentSlot); });
        CurrentItems.Add(item);
    }

    void SetHighlight(GameObject button, bool active)
    {
        button.GetComponent<Outline>().enabled = active;
    }

    void Select(Item item, GameObject slot)
    {
        if (selected != null)
        {
            if (selected == item)
            {
                UnSelect(selected);
                selected = null;
            }
            else
            {
                UnSelect(selected);
                selected = item;

                for (int i = 0; i < InventorySlots.Count; i++)
                {
                    if (InventorySlots[i]== slot)
                    {
                        selectedSlot = i;
                        break;
                    }
                }
            }
        }
        else
        {
            selected = item;

            for (int i = 0; i < InventorySlots.Count; i++)
            {
                if (InventorySlots[i] == slot)
                {
                    selectedSlot = i;
                    break;
                }
            }
        }

        if (selected != null)
        {
            SetHighlight(slot, true);

            if (item.type == "Usable")
            {
                //Player.Use(item);
            }
            else if (item.type == "Holdable")
            {
                //Player.Hold(item);
            }
            else if (item.type == "Weapon")
            {
                //Player.Equip(item);
            }
        }
    }

    void UnSelect(Item item)
    {
        //Player.Unequip ();

        SetHighlight(InventorySlots[selectedSlot], false);
    }
}
