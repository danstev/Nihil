using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item {

    public int health, power, attack, defense;
    public Animator anim;

    public void OnStart()
    {
        anim.PlayInFixedTime("spin", -1, 0);
    }

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
        transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        transform.localRotation = Quaternion.Euler(0,0,0);

        //Update stats from newly equipped item.
        Stats s = person.GetComponent<Stats>();
        s.UpdateStats(health, power, defense, attack);

        //Animset
        anim.PlayInFixedTime("idle", -1, 0);
        //transform.localRotation = Quaternion.Euler(20, 90, -20);
    }

    public void Unequip(GameObject person)
    {
        //Remake capsule collider
        CapsuleCollider cap = GetComponent<CapsuleCollider>();
        cap.isTrigger = true;
        cap.radius = 1f;
        cap.height = 1f;

        //Remove in inventory
        Inventory invent = person.GetComponent<Inventory>();
        invent.weapon = null;

        //Move to correct places
        transform.parent = null;

        //Update stats from newly equipped item.
        Stats s = person.GetComponent<Stats>();
        s.UpdateStats(health, power, defense, attack);

        //Animset
        anim.PlayInFixedTime("spin", -1, 0);
    }
}
