using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    ItemDatabase database;
    GameObject inventoryPanel;
    GameObject slotPanel;
    public GameObject inventorySlot;
    public GameObject inventoryItem;

    int slotAmount;
    public List<Item> items = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();

    void Start() {
        database = GetComponent<ItemDatabase>();

        slotAmount = 20;
        inventoryPanel = GameObject.Find("Inventory Panel");
        slotPanel = inventoryPanel.transform.FindChild("Slot Panel").gameObject;

        for(int i = 0; i < slotAmount; i++) {
            slots.Add(Instantiate(inventorySlot));
            slots[i].transform.SetParent(slotPanel.transform);
            items.Add(new Item());
            slots[i].GetComponent<InvSlot>().id = i;
        }

        AddItem(2);
        AddItem(0);
        
    }

    public void AddItem(int id) {

        Item itemToAdd = database.FetchItemByID(id);
        if(itemToAdd.Stackable && CheckItemInInv(itemToAdd)) {
            for(int i = 0; i < items.Count; i++) {
                if(items[i].ID == id) {
                    ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                    data.amount++;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                    break;
                }
            }
        } else {
            for(int i = 0; i < items.Count; i++) {
                if(items[i].ID == -1) {
                    items[i] = itemToAdd;
                    GameObject itemGO = Instantiate(inventoryItem);
                    ItemData itemData = itemGO.GetComponent<ItemData>();
                    itemData.item = itemToAdd; // Sets ItemData item to be the item to add.
                    itemData.slot = i; // Sets ItemData slot number to be the item slot number.
                    itemGO.transform.SetParent(slots[i].transform);
                    itemGO.transform.position = Vector2.zero;
                    // Sets the image of the item
                    itemGO.GetComponent<Image>().sprite = itemToAdd.Sprite;
                    
                    itemGO.name = itemToAdd.Title;
                    break;
                }
            }
        }
    }

    bool CheckItemInInv(Item item) {

        for(int i = 0; i < items.Count; i++) {
            if(items[i].ID == item.ID)
                return true;
        }
        return false;
    }
}
