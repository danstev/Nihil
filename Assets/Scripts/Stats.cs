using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Stats : NetworkBehaviour {

    public Text healthText;
    public Text powerText;

    //Knockback vars
    public Rigidbody r;
    public CharacterController player;
    private Vector3 imp;

    [SyncVar(hook = "OnChangeHealth")]
    public int health = 0;

    [SyncVar(hook = "OnChangePower")]
    public int power = 0;

    public int maxHealth = 100;
    public int maxPower = 50;

    //[SyncVar]
    //private bool dead = false;

    // Use this for initialization
    void Start () {
        imp = Vector3.zero;

        if (isLocalPlayer)
        {
            healthText.text = "Health: " + health;
            powerText.text = "Power: " + power;
            //hover.enabled = false;
        }
        else
        {
            //hover.enabled = true;
        }
    }
	
	// Update is called once per frame
	void Update () {

        if(imp.magnitude > 0.2f)
        {
            player.Move(imp * Time.deltaTime);
        }
        else
        {
            imp = Vector3.zero;
        }
        imp = Vector3.Lerp(imp, Vector3.zero, 5 * Time.deltaTime);
    }

    [Command]
    public void CmdTakeDamage(float[] incoming)
    {
        health -= (int)incoming[0];

        Vector3 p = new Vector3(incoming[1], incoming[2], incoming[3]);
        p = transform.position - p;
        p = p.normalized;

        if (r != null)
        {
            r.AddForce(p * 12500);
        }
        else if(player != null)
        {
            if (!isServer)
            {
                AddImpact(p);
            }
            else
            {
                RpcAddImpact(p);
            }
        }

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

    public void OnChangePower(int power)
    {
        if (isLocalPlayer)
            powerText.text = "Power: " + power;
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

    private void AddImpact(Vector3 dir)
    {
        //dir.Normalize();
        //if (dir.y < 0) dir.y = -dir.y;

        imp += dir.normalized * 10;
    }

    [ClientRpc]
    private void RpcAddImpact(Vector3 dir)
    {
        //dir.Normalize();
        //if (dir.y < 0) dir.y = -dir.y;

        imp += dir.normalized * 10;
    }
}
