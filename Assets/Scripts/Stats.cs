using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Stats : NetworkBehaviour {

    public Text healthText;

    [SyncVar(hook = "OnChangeHealth")]
    public int health = 0;

    public int maxHealth = 100;
    

    //[SyncVar]
    //private bool dead = false;

    // Use this for initialization
    void Start () {
        if(isLocalPlayer)
        healthText.text = "Health: " + health;
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void TakeDamage(int incoming)
    {
        
        health -= incoming;
        
    }

    public void OnChangeHealth(int health)
    {
        if(isLocalPlayer)
        healthText.text = "Health: " + health;
    }
}
