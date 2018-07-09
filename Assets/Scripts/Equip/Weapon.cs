using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item {

    public new void Interact(GameObject person)
    {
        //Eliminiate capsule collider
        CapsuleCollider cap = GetComponent<CapsuleCollider>();
        cap.isTrigger = false;
        cap.radius = 0.0001f;
        cap.height = .0001f;

        //Place in inventory
        Inventory invent = person.GetComponent<Inventory>();
        invent.weapon = this.gameObject;

        //Move to correct places
        transform.parent = person.transform;
        transform.localPosition = new Vector3(0.43f, 0.25f, 0.53f);
        transform.localRotation = Quaternion.Euler(20,90,-20);

        //Update stats from newly equipped item.
        Stats s = person.GetComponent<Stats>();
        s.UpdateStats(health, power, defense, attack);
    }
}
