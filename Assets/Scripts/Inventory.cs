using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Inventory : NetworkBehaviour {

    public GameObject[] slots = new GameObject[20];

    [SyncVar]
    public GameObject weapon, spell, armour;

    public void WeaponUnequip()
    {

        Weapon w = weapon.GetComponent<Weapon>();
        w.CmdUnequip(gameObject);
    }

}
