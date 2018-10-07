using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public bool equipable;
    public int slotTaken;

    //public int health, power, attack, defense; not needed here C: 

    void Use()
    {

    }

    public void Interact(GameObject go)
    {
        Stats s = go.GetComponent<Stats>();
        s.RecoverDamage(10);
        Destroy(gameObject);
        Debug.Log("Destroying self.");
    }

    
}