﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


[Serializable]
public class Item
{
    public int iD;
    public Sprite image;
    public string type;
    public int damage;
    public GameObject obj;

    public Item(Sprite image, string type, int iD, int damage, GameObject obj)
    {
        this.image = image;
        this.type = type;
        this.iD = iD;
        this.damage = damage;
        this.obj = obj;
    }
}

public class Inventory : MonoBehaviour {

    public List<Item> ItemData = new List<Item>();

    public List<GameObject> InventorySlots;
    public List<Item> CurrentItems = new List<Item>();
    public int maxItems = 5;

    public bool IsFull
    {
        get
        {
            if (CurrentItems.Count == maxItems)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public Item selected;
    public int selectedSlot;

    public Transform inventoryBar;

    playerMovement Player;

    public static Inventory Instance
    {
        get
        {
            return instance;
        }
    }
    private static Inventory instance = null;

    void Awake()
    {
        if (instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        //DontDestroyOnLoad(gameObject);
    }

	void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

    void Start()
    {
		
		if (GameManager.Instance.gameplayScene) {
			Player = GameObject.FindGameObjectWithTag ("Player").GetComponent<playerMovement> ();
		} else {
			Player = null;
		}
    }

    public void AddItem(int itemID)
    {
        if (CurrentItems.Count > maxItems)
        {
            return;
        }

        Item item = ItemData[0];
        foreach(Item eachItem in ItemData)
        {
            if (eachItem.iD == itemID)
            {
                item = eachItem;
                break;
            }
        }

        GameObject currentSlot = InventorySlots[CurrentItems.Count];

        currentSlot.transform.GetChild(0).GetComponent<Image>().sprite = item.image;
        if (itemID != 3)
        {
            currentSlot.transform.GetChild(0).localScale = Vector3.one * 3f;
        }
        currentSlot.GetComponent<Button>().onClick.AddListener(delegate () { Select(item, currentSlot); });
        CurrentItems.Add(item);
    }

    public bool HasItem(int iD)
    {
        foreach (Item eachItem in CurrentItems)
        {
            if (eachItem.iD == iD)
            {
                return true;
            }
        }
        

        return false;
    }

    public void RemoveItem(int itemID)
    {
        Item item = ItemData[0];
        int itemNumber = 0;
        foreach (Item eachItem in CurrentItems)
        {
            if (eachItem.iD == itemID)
            {
                item = eachItem;
                break;
            }
            itemNumber++;
        }

        GameObject currentSlot = InventorySlots[itemNumber];

        currentSlot.transform.GetChild(0).GetComponent<Image>().sprite = null;
        currentSlot.GetComponent<Button>().onClick.RemoveListener(delegate () { Select(item, currentSlot); });
        CurrentItems.Remove(item);
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
                Player.Use(item);
            }
            else if (item.type == "Holdable")
            {
                Player.Hold(item);
            }
            else if (item.type == "Weapon")
            {
                Player.Equip(true, item);
            }
        }
    }

    void UnSelect(Item item)
    {
        Player.Equip (false, item);

        SetHighlight(InventorySlots[selectedSlot], false);
    }

	void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		// Hide inventory if it's not in a gameplay scene
		// GameManager's OnSceneLoaded seems to trigger after this, so it's not useful
		GameManager.Instance.SetGameplayScene(scene, mode);
		if (GameManager.Instance.gameplayScene) {
            //if (scene.name == "Preface") {
            Player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>();
			// Yes, that is the default scale of the inventory canvas
			gameObject.transform.localScale = new Vector3 (0.25f, 0.25f, 0.25f);
		} else {
			Player = null;
			gameObject.transform.localScale = new Vector3 (0, 0, 0);
		}
	}
}