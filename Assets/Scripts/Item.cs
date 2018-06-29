using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public bool equipable;
    public int slotTaken;

    public int health, attack, defense; 

    void Use()
    {
        if(equipable)
        {
            //Equipment/Armour
            if(slotTaken == 0) 
            {
                //Weapon
            }
            else if(slotTaken == 2)
            {
                //Armour
            }
        }
        else
        {
            //Consumable

        }
    }

    public void Interact(Stats s)
    {
        s.RecoverDamage(10);
        Destroy(gameObject);
        Debug.Log("Destroying self.");
    }
}