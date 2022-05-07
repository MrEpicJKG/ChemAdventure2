using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectInteractController : MonoBehaviour
{
    
    [SerializeField] private float maxPlayerDetectDist = 2;
    [SerializeField] private ObjectType objType = ObjectType.Loot;

    [Header("Only used for Doors and Stairs")]
    [SerializeField] private string sceneToLoad;

    private GameObject player;
    private GameObject myIconObj;
    private bool canBeTapped = false;
    private Item myItem;
    private InventoryManager invManager;

    private enum ObjectType { Door, Stairs, Loot, LootContainer}
    // Start is called before the first frame update
    void Start()
    {
        myIconObj = transform.GetChild(0).gameObject;
        myIconObj.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        myItem = GetComponent<Item>();
        invManager = player.GetComponent<InventoryManager>();
    }

	private void Update()
	{
        float dist = Vector2.Distance(transform.position, player.transform.position);
        if(dist <= maxPlayerDetectDist)
        {
            myIconObj.SetActive(true);
            canBeTapped = true;
		}
        else
        {
            myIconObj.SetActive(false);
            canBeTapped = false;
		}
	}

	public void OnTapped()
    {
        if (canBeTapped == true)
        {
            if (objType == ObjectType.Loot)
            {
                if(myItem != null)
                {
                    invManager.AddItemToInv(myItem);
				}
                //Add to player inventory
            }
            else if (objType == ObjectType.LootContainer)
            {
                //Open Container inventory
            }
            else if (objType == ObjectType.Door)
            {
                //load new scene for other side of door
            }
            else if (objType == ObjectType.Stairs)
            {
                //Load new Scene for next floor
            }
        }
	}
}
