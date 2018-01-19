using System;
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
	private GameManager gameManager;

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
		gameManager = GameObject.FindObjectOfType<GameManager> ();
	}

	void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

    void Start()
    {
		
		if (gameManager.gameplayScene) {
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
		//if (gameManager.gameplayScene) {
		if (scene.name == "Preface") {
			Player = GameObject.FindGameObjectWithTag ("Player").GetComponent<playerMovement> ();
			// Yes, that is the default scale of the inventory canvas
			gameObject.transform.localScale = new Vector3 (0.25f, 0.25f, 0.25f);
		} else {
			Player = null;
			gameObject.transform.localScale = new Vector3 (0, 0, 0);
		}
	}
}
