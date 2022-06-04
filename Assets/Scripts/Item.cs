using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    [HideInInspector] public int itemID = 0;
    [Header("--- General ---")]
    public InventoryManager.ItemType itemType = InventoryManager.ItemType.Weapon;
    public string itemName = "Placeholder";
    public float itemWeight = 0;

    [Header("--- UI ---")]
    public Text itemNameText;
    public Text weightText;
    public Text typeText;

    [Header("--- Weapons --- ")]
    [Range(0, 40)]     public float damage = 1;         //this is the damage dealt per hit. Is already formatted for disp value.
    [Range(10, 1000)] public float rateOfFire = 100;   //this is the number of ms between attacks. Convert to disp value with Mathf.Abs((rateOfFire / 100) - 10).
                                                        //This will make lower values here, be higher values to display
    [Range(0, 10)]     public float accuracy = 1;       //(this * 2) is the max number of degrees the bullets shoot angle can be offset by. Its * 2 since the angle can be above or below the "0-line".
                                                        //Convert to disp value with Mathf.Abs(accuracy - 10)
    [Range(0, 40)]     public float noise = 1;          //Not sure yet how this will be implemented. Perhaps the distance away that it can alert enemies?

    [Header("--- Healing ---")]
    [Range(0, 1)] public float healAmtPercent = 0.01f; //this is the percentage of health that will be restored on use

    //Junk has no additional vars
    [Header("--- Quest ---")]
    public NPC giverNpc;// = new NPC();
    public NPC recieverNpc;// = new NPC();
    public string questName = "";
    [Multiline] public string questDesc = "";
    public QuestType questType = QuestType.GetItems;
    public List<Item> itemsToGet = new List<Item>();
    public Room roomToClear;// = new Room();
    public NPC npcToTalkTo;// = new NPC();



    private InventoryManager invManager;
    private UIInventoryManager uiInvManager;
    private Outline myOutline;
    [HideInInspector] public enum QuestType { GetItems, ClearEnemiesFromRoom, TalkToNPC};
    void Awake()
    {
        invManager = GameObject.Find("PlayerRoot").GetComponent<InventoryManager>();
        //Assign an unused id to new items
        itemID = invManager.allItemIds.Count;
        uiInvManager = GameObject.Find("GameController").GetComponent<UIInventoryManager>();
        myOutline = GetComponent<Outline>();
    }

    public void OnDropItemUIClk() //This can only be called from within the UI
    {
        print("Item Dropped");
        if(invManager == null)
        {
            invManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryManager>();
        }

        if(uiInvManager == null)
        {
            uiInvManager = GameObject.Find("GameController").GetComponent<UIInventoryManager>();
        }
        invManager.RemoveItemFromInv(this);
        uiInvManager.RefreshUI();
	}

    public void OnSelectUIItem() //This can only be called from within the UI
    {
        print("Item Selected");
        if(myOutline != null)
        {
            uiInvManager.SelectItem(this, myOutline);
		}
	}

	public void OnDrag()
	{
        print("Tap Dragged");
	}


}
