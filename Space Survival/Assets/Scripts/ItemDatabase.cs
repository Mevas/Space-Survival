using UnityEngine;
using System.Collections;
using LitJson;
using System.Collections.Generic;
using System.IO;

public class ItemDatabase : MonoBehaviour {
    private List<Item> database = new List<Item>();
    JsonData itemData;

    void Start() {
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"));

        ConstructItemDatabase();
        Debug.Log(FetchItemBySlug("energy_crystal").Description);
    }

    public Item FetchItemByID(int id) {
        for(int i = 0; i < database.Count; i++)
            if(database[i].ID == id)
                return database[i];
        return null;
    }

    public Item FetchItemBySlug(string slug) {
        for(int i = 0; i < database.Count; i++)
            if(database[i].Slug == slug)
                return database[i];
        return null;
    }

    void ConstructItemDatabase() {
        for(int i = 0; i < itemData.Count; i++) {
            database.Add(new Item((int)itemData[i]["id"], itemData[i]["title"].ToString(), (int)itemData[i]["weight"], (bool)itemData[i]["stackable"], itemData[i]["description"].ToString(), itemData[i]["slug"].ToString()));
        }
    }
}

public class Item {
    public int ID { get; set; }
    public string Title { get; set; }
    public int Weight { get; set; }
    public bool Stackable { get; set; }
    public string Description { get; set; }
    public string Slug { get; set; }
    public Sprite Sprite { get; set; }

    public Item(int id, string title, int weight, bool stackable, string desc, string slug) {
        ID = id;
        Title = title;
        Weight = weight;
        Stackable = stackable;
        Description = desc;
        Slug = slug;
        Sprite = Resources.Load<Sprite>("Sprites/" + slug);
    }

    public Item() {
        ID = -1;
    }
}
