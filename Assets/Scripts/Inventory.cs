using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public GameObject[] slots = new GameObject[20];
    public GameObject[] equipped = new GameObject[20];

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void addItem(GameObject g)
    {
        Item i = g.GetComponent<Item>();
        //print(i.name);
        //print(i.slotTaken);

        if (i.equipable)
        {
            if (equipped[i.slotTaken] == null)
            {
                equipped[i.slotTaken] = g;

                if (i.slotTaken == 0)
                {
                    //i.equipWeapon();
                    //PlayerControl p = GetComponent<PlayerControl>();
                    //p.refreshWeapon(g);
                    //CapsuleCollider c = i.GetComponent<CapsuleCollider>();
                    //c.enabled = false;
                }
                else
                {
                    //i.equip();
                    //g.SetActive(false);
                }
                //Equipment e = g.GetComponent<Equipment>();
                //e.equip();
                //i.transform.parent = transform;
                //updateAllStatisitics();
                return;
            }
            else
            {
                //add to next free slot
                place(g);
                return;
            }
        }
        else
        {
            //find in inv
            place(g);
            return;
        }
    }

    void place(GameObject g)
    {
        //int freeslot = getNextFreeSlot();
        //slots[freeslot] = g;
        //g.transform.parent = transform;
        //g.SetActive(false);
        //updateGUITextures();
    }
}
