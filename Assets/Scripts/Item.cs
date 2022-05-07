using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [HideInInspector] public int itemID = 0;
    [Header("--- General ---")]
    public InventoryManager.ItemType itemType = InventoryManager.ItemType.Weapon;
    public string itemName = "Placeholder";
    public float itemWeight = 0;

    [Header("--- Weapons --- ")]
    [Range(1, 40)]     public float damage = 1;         //this is the damage dealt per hit. Is already formatted for disp value.
    [Range(100, 1000)] public float rateOfFire = 100;   //this is the number of ms between attacks. Convert to disp value with Mathf.Abs((rateOfFire / 100) - 10).
                                                        //This will make lower values here, be higher values to display
    [Range(0, 10)]     public float accuracy = 1;       //(this * 2) is the max number of degrees the bullets shoot angle can be offset by. Its * 2 since the angle can be above or below the "0-line".
                                                        //Convert to disp value with Mathf.Abs(accuracy - 10)
    [Range(1, 40)]     public float noise = 1;          //Not sure yet how this will be implemented. Perhaps the distance away that it can alert enemies?

    [Header("--- Healing ---")]
    [Range(0.01f, 1)] public float healAmtPercent = 0.01f; //this is the percentage of health that will be restored on use

    //Junk has no additional vars
    [Header("--- Quest ---")]
    public NPC giverNpc = new NPC();
    public NPC recieverNpc = new NPC();
    public string questName = "";
    [Multiline] public string questDesp = "";
    public QuestType questType = QuestType.GetItems;
    public List<Item> itemsToGet = new List<Item>();
    public Room roomToClear = new Room();
    public NPC npcToTalkTo = new NPC();



    private InventoryManager invManager;
    [HideInInspector] public enum QuestType { GetItems, ClearEnemiesFromRoom, TalkToNPC};
    void Start()
    {
        invManager = GameObject.Find("Player").GetComponent<InventoryManager>();
        //Assign an unused id to new items
        itemID = invManager.allItemIds.Count;
        invManager.AddItemToInv(this);
    }
}
