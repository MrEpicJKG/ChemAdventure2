using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractController : MonoBehaviour
{
    public GameObject[] interactableObjects;
    [SerializeField] private float maxInteractRange = 1;


    void Start()
    {
        
    }

    void FixedUpdate()
    {
        
    }

    private void GetInteractableObj()
    {
        RaycastHit2D[] hitObj = Physics2D.CircleCastAll(transform.position, maxInteractRange, Vector2.zero);
        for(int i = 0; i < hitObj.Length; i++)
        {
            Transform obj = hitObj[i].transform;
            if(obj.CompareTag("Loot") || obj.CompareTag("LootContainer") || obj.CompareTag("Door") || obj.CompareTag("Stairs"))
            {
                
			}
		}
	}
}
