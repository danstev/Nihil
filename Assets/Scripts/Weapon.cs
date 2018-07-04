using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item {

    public new void Interact(GameObject person)
    {
        Inventory invent = person.GetComponent<Inventory>();
        invent.slots[0] = this.gameObject;
        transform.parent = person.transform;
        transform.position = new Vector3(0.43f, 0f, 0.53f);
    }
}
