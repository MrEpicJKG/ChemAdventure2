using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UIInventoryManager : MonoBehaviour
{
    public Item selectedItem;

    [SerializeField] private GameObject invPanel;
    [SerializeField] private InventoryManager invManager;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private float itemPanelStartingYPos = -21.5f;
    [SerializeField] private Transform scrollViewContent;
    [SerializeField] private Color itemPanelWhiteColor;
    [SerializeField] private Color itemPanelGrayColor;

    [SerializeField] private Image primEquippedWeapImage;
    [SerializeField] private Image secEquippedWeapImage;
    [SerializeField] private Slider weightSlider;
    [SerializeField] private Text weightSliderText;
    [SerializeField] private Text itemDetailsNameText;
    [SerializeField] private Text itemDetailsDescText;
    [SerializeField] private Image itemDetailsImage;
    [SerializeField] private Slider itemDetailsDamSlider;
    [SerializeField] private Text itemDetailsDamText;
    [SerializeField] private Slider itemDetailsAccSlider;
    [SerializeField] private Text itemDetailsAccText;
    [SerializeField] private Slider itemDetailsFireRateSlider;
    [SerializeField] private Text itemDetailsFireRateText;
    [SerializeField] private Slider itemDetailsNoiseSlider;
    [SerializeField] private Text itemDetailsNoiseText;

    private float currTotWeight = 0;
    private Outline selectedOutline;
    private Tab currTab = Tab.All;

    private List<Item> weaponItems = new List<Item>();
    private List<Item> healingItems = new List<Item>();
    private List<Item> junkItems = new List<Item>();
    private List<Item> questItems = new List<Item>();
    private List<Item> ammoItems = new List<Item>();
    private List<Item> ingredentItems = new List<Item>();
    private List<Item> chemItems = new List<Item>();
    
    private enum Tab { All, Weaps, Health, Ammo, Chems, Ingredents, Quest, Junk};

    private GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameController").GetComponent<GameManager>();
        RefreshUI();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I) == true)
        {
            if (invPanel.activeSelf == true)
            {
                invPanel.SetActive(false);
            }
            else
            {
                invPanel.SetActive(true);
                currTab = Tab.All;
                RefreshUI();
			}
		}
    }

    public void RefreshUI()
    {
        weaponItems = invManager.weaponItems;
        healingItems = invManager.healingItems;
        junkItems = invManager.junkItems;
        questItems = invManager.questItems;
        ammoItems = invManager.ammoItems;
        ingredentItems = invManager.ingredentItems;
        chemItems = invManager.chemItems;

        currTotWeight = CalcTotalWeight();
        weightSlider.value = currTotWeight;
        if(currTotWeight > weightSlider.maxValue)
        {
            weightSliderText.color = Color.red;
		}
        else
        {
            weightSliderText.color = Color.black;
		}
        weightSliderText.text = currTotWeight.ToString() + " lbs. / " + weightSlider.maxValue.ToString() + " lbs.";

        RefreshInvList();
	}

    private float CalcTotalWeight()
    {
        float totWeight = 0;
        
        foreach(Item item in weaponItems)
        {
            totWeight += item.itemWeight;
		}

        foreach (Item item in healingItems)
        {
            totWeight += item.itemWeight;
        }

        foreach (Item item in junkItems)
        {
            totWeight += item.itemWeight;
        }

        foreach (Item item in questItems)
        {
            totWeight += item.itemWeight;
        }

        foreach (Item item in ammoItems)
        {
            totWeight += item.itemWeight;
        }

        foreach (Item item in ingredentItems)
        {
            totWeight += item.itemWeight;
        }

        foreach (Item item in chemItems)
        {
            totWeight += item.itemWeight;
        }

        return totWeight;
    }

    private void RefreshInvList()
    {
        List<Item> currList = new List<Item>();
        if(currTab == Tab.Weaps)
        {
            currList = weaponItems;
		}
        else if (currTab == Tab.Health)
        {
            currList = healingItems;
        }
        else if(currTab == Tab.Junk)
        {
            currList = junkItems;
        }
        else if (currTab == Tab.Quest)
        {
            currList = questItems;
        }
        else if (currTab == Tab.Ammo)
        {
            currList = ammoItems;
        }
        else if (currTab == Tab.Ingredents)
        {
            currList = ingredentItems;
        }
        else if (currTab == Tab.Chems)
        {
            currList = chemItems;
        }
        else //(currTab == Tab.All)
        {
            currList = new List<Item>();
            currList = weaponItems;
            currList.AddRange(ammoItems);
            currList.AddRange(healingItems);
            currList.AddRange(junkItems);
            currList.AddRange(questItems);
            currList.AddRange(ingredentItems);
            currList.AddRange(chemItems);
        }

        currList.Sort((s1, s2) => s1.itemName.CompareTo(s2.itemName));

        for(int i = 0; i < currList.Count; i++)
        {
            GameObject panel = Instantiate(itemPrefab, new Vector3(0, itemPanelStartingYPos - (35 * i)), Quaternion.identity, scrollViewContent);
            panel.transform.GetChild(0).GetComponent<Text>().text = currList[i].itemName; //Name
            panel.transform.GetChild(1).GetComponent<Text>().text = currList[i].itemWeight.ToString() + " lbs."; //Weight
            panel.transform.GetChild(2).GetComponent<Text>().text = currList[i].itemType.ToString(); //Type
            if(i % 2 == 0)
            {
                panel.GetComponent<Image>().color = itemPanelWhiteColor; 
            }
            else
            {
                panel.GetComponent<Image>().color = itemPanelGrayColor;
			}

            Item panelItem = panel.GetComponent<Item>();
            panelItem = currList[i];
        }

    }

    [HideInInspector] public void SelectItem(Item item, Outline outline)
    {
        if (selectedOutline != null)
        { 
            selectedOutline.enabled = false;
        }
        
        selectedItem = item;
        selectedOutline = outline;
        selectedOutline.enabled = true;

        //update displayed stats here
    }

    //Button Events
    public void OnPrimaryEquipClk()
    {
        manager.clickCapturedByUI = true;
	}

    public void OnSecondaryEquipClk()
    {
        manager.clickCapturedByUI = true;
    }

    public void OnAllTabClk()
    {
        currTab = Tab.All;
        RefreshUI();
        manager.clickCapturedByUI = true;
    }

    public void OnWeapTabClk()
    {
        currTab = Tab.Weaps;
        RefreshUI();
        manager.clickCapturedByUI = true;
    }

    public void OnHealthTabClk()
    {
        currTab = Tab.Health;
        RefreshUI();
        manager.clickCapturedByUI = true;
    }

    public void OnAmmoTabClk()
    {
        currTab = Tab.Ammo;
        RefreshUI();
        manager.clickCapturedByUI = true;
    }

    public void OnChemTabClk()
    {
        currTab = Tab.Chems;
        RefreshUI();
        manager.clickCapturedByUI = true;
    }

    public void OnIngTabClk()
    {
        currTab = Tab.Ingredents;
        RefreshUI();
        manager.clickCapturedByUI = true;
    }

    public void OnQuestTabClk()
    {
        currTab = Tab.Quest;
        RefreshUI();
        manager.clickCapturedByUI = true;
    }

    public void OnJunkTabClk()
    {
        currTab = Tab.Junk;
        RefreshUI();
        manager.clickCapturedByUI = true;
    }
}
