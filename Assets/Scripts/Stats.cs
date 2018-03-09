using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour {

    public int health = 0;
    public int maxHealth = 100;
    private bool dead = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (dead)
        {
            return;
        }
    }

    public void TakeDamage(int incoming)
    {
        health -= incoming;
        Debug.Log("Damage: " + incoming + " has been taken by: " + gameObject.name + ".");
    }
}
