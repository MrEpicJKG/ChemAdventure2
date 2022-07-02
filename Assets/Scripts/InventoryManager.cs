using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<int> allItemIds = new List<int>();
    public List<Item> weaponItems = new List<Item>();
    public List<Item> healingItems = new List<Item>();
    public List<Item> junkItems = new List<Item>();
    public List<Item> questItems = new List<Item>();
    public List<Item> ammoItems = new List<Item>();
    public List<Item> ingredentItems = new List<Item>();
    public List<Item> chemItems = new List<Item>();

    [Header("DEBUG")]
    public bool printWeaps = false;
    public bool printHealing = false;
    public bool printJunk = false;
    public bool printQuest = false;
    public bool printAmmo = false;
    public bool printIngredents = false;
    public bool printChems = false;

    /*[HideInInspector]*/ public Item primEquippedWeap;
    /*[HideInInspector]*/ public Item secEquippedWeap;

    [HideInInspector] public enum ItemType { Weapon, Healing, Junk, Quest, Ammo, Ingredent, Chem};
    [HideInInspector] public enum WeapType { None, Melee_Blade, Melee_Blunt, Ranged_Gun};
    [HideInInspector] public enum AmmoType { None, _9mm, _556, _762};
    // Start is called before the first frame update
    void Start()
    {
        //Load default starting items here
    }

	private void Update()
	{
		if(printWeaps == true)
        {
            printWeaps = false;
            string printStr = "Weapons: ";
            foreach(Item item in weaponItems)
            {
                printStr += ", " + item.itemName;
			}
		}

        if (printHealing == true)
        {
            printHealing = false;
            string printStr = "Healing: ";
            foreach (Item item in healingItems)
            {
                printStr += ", " + item.itemName;
            }
        }

        if (printJunk == true)
        {
            printJunk = false;
            string printStr = "Junk: ";
            foreach (Item item in junkItems)
            {
                printStr += ", " + item.itemName;
            }
        }

        if (printQuest == true)
        {
            printQuest = false;
            string printStr = "Quest: ";
            foreach (Item item in questItems)
            {
                printStr += ", " + item.itemName;
            }
        }

        if (printAmmo == true)
        {
            printAmmo = false;
            string printStr = "Ammo: ";
            foreach (Item item in ammoItems)
            {
                printStr += ", " + item.itemName;
            }
        }

        if (printIngredents == true)
        {
            printIngredents = false;
            string printStr = "Ingredents: ";
            foreach (Item item in ingredentItems)
            {
                printStr += ", " + item.itemName;
            }
        }

        if (printChems == true)
        {
            printChems = false;
            string printStr = "Chems: ";
            foreach (Item item in chemItems)
            {
                printStr += ", " + item.itemName;
            }
        }
    }

	public void AddItemToInv(Item item)
    {
        switch(item.itemType)
        {
            case ItemType.Weapon:
            {
                weaponItems.Add(item);
                allItemIds.Add(item.itemID);
                break;
			}

            case ItemType.Healing:
            {
                healingItems.Add(item);
                allItemIds.Add(item.itemID);
                break;
			}

            case ItemType.Junk:
            {
                junkItems.Add(item);
                allItemIds.Add(item.itemID);
                break;
			}

            case ItemType.Quest:
            {
                questItems.Add(item);
                allItemIds.Add(item.itemID);
                break;
			}

            case ItemType.Ammo:
            {
                ammoItems.Add(item);
                allItemIds.Add(item.itemID);
                break;
			}

            case ItemType.Ingredent:
            {
                ingredentItems.Add(item);
                allItemIds.Add(item.itemID);
                break;
			}

            case ItemType.Chem:
            {
                chemItems.Add(item);
                allItemIds.Add(item.itemID);
                break;
			}

            default:
            {
                print("ERROR: Unknown Item Type for item named: " + item.itemName + " with the ID of: " + item.itemID + "!!");
                break;
			}
		}
	}

    public void RemoveItemFromInv(Item item)
    {
        switch (item.itemType)
        {
            case ItemType.Weapon:
            {
                weaponItems.Remove(item);
                allItemIds.Remove(item.itemID);
                break;
            }

            case ItemType.Healing:
            {
                healingItems.Remove(item);
                allItemIds.Remove(item.itemID);
                break;
            }

            case ItemType.Junk:
            {
                junkItems.Remove(item);
                allItemIds.Remove(item.itemID);
                break;
            }

            case ItemType.Quest:
            {
                questItems.Remove(item);
                allItemIds.Remove(item.itemID);
                break;
            }

            case ItemType.Ammo:
            {
                ammoItems.Remove(item);
                allItemIds.Remove(item.itemID);
                break;
            }

            case ItemType.Ingredent:
            {
                ingredentItems.Remove(item);
                allItemIds.Remove(item.itemID);
                break;
            }

            case ItemType.Chem:
            {
                chemItems.Remove(item);
                allItemIds.Remove(item.itemID);
                break;
            }

            default:
            {
                print("ERROR: Unknown Item Type for item named: " + item.itemName + " with the ID of: " + item.itemID + "!!");
                break;
            }
        }
    }
}
