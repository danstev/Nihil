using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        if (isLocalPlayer)
        {
            healthText.text = "Health: " + health;
            //hover.enabled = false;
        }
        else
        {
            //hover.enabled = true;
        }
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void TakeDamage(int incoming)
    {
        
        health -= incoming;

        if(health <= 0)
        {
            Destroy();
        }
        
    }

    public void RecoverDamage(int incoming)
    {
        health += incoming;
    }

    public void OnChangeHealth(int health)
    {
        if(isLocalPlayer)
        healthText.text = "Health: " + health;
    }

    public void Destroy()
    {
        if(tag == "Enemy")
        {
            GetComponent<EnemyAI>().SetDead();
            StartCoroutine(Fade(GetComponentInChildren<Renderer>()));
            Destroy(gameObject, 2f);
        }
    }

    IEnumerator Fade(Renderer r)
    {
        float start = Time.time;
        while (Time.time <= start + 2f)
        {
            Color c = r.material.color;
            c.a = 1f - Mathf.Clamp01((Time.time - start) / 2f);
            r.material.color = c;
            yield return new WaitForEndOfFrame();
        }
    }
}
