using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 20;
    //add more here

    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    [HideInInspector] public void RecieveDamage(float damage)
    {
        health = Mathf.Clamp(health - damage, 0, Mathf.Infinity);
        if(health == 0)
        {
            print("Enemy Killed");
            gameObject.SetActive(false); //replace this with death anim
		}
	}
}
